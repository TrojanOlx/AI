using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using FaceDlib.Core.Common;
using DlibCore.Computer;
using FaceDlib.Core.Common.Encrypt;
using FaceDlib.Core.Models.Common;
using FaceDlib.Core.Service;
using FaceDlib.Core.Models.Models;
using Newtonsoft.Json;
using FaceDlib.Core.Common.DBHelper;
using FaceDlib.Core.Models.ViewModel;
using System.Drawing;
using System.Collections.Generic;

namespace FaceDlib.Core.Api.Controllers
{


    public class FaceDetectionController : Controller
    {
        /// <summary>
        /// 获取人脸坐标(单个文件)
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetFaceLocation(FaceLocationsViewModel model)
        {

            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null,
                FaceList = null
            };

            #region 基本验证
            // 模型验证
            if (!ModelState.IsValid)
            {
                request.Message = ModelState.Where(e => e.Value.Errors.Count > 0).FirstOrDefault().Value.Errors[0].ErrorMessage;
                request.Status = (int)RequestEnum.必要参数未传入;
                return Ok(request);
            }

            // 根据 secretkey 获取set
            var userFaceSet = await Service_user_face_set.GetUser_Face_Set_By_Secret_key_Async(model.secret_id);
            if (userFaceSet == null)
            {
                // 您未开通此项目
                request.Enum = RequestEnum.没有接口权限;
                return Ok(request);
            }


            // 签名判断
            var dic = ModleToDictionary.GetProperties(model);
            var key = userFaceSet.secret_key;
            dic.Remove("sign");


            var sign = Signature.GetSign(dic, key);
            if (sign != model.sign)
            {
                // 签名错误
                request.Enum = RequestEnum.签名错误;
                return Ok(request);
            }
            #endregion



            #region 余额逻辑判断

            // 根据 userFaceSet.user_id 获取用户信息
            var user = await Service_user.GetUser_By_User_id_Async(userFaceSet.user_id);

            // 根据user_id 先查预付款，未查询到则扣费使用
            var prepayment = await Service_user_face_detect_prepayment.GetUser_Face_Detect_Prepayment_By_User_id_Asynv(user.id);
            user_face_detect_price derectPrice = null;
            if (prepayment == null || prepayment.number <= 0)
            {
                prepayment = null;
                derectPrice = await Service_user_face_detect_price.GetUser_Face_Detect_Price_By_User_idAsync(user.id);
                if (derectPrice == null)
                {
                    // 用户没有预付费次数
                    request.Enum = RequestEnum.没有预付费次数未开通余额付款;
                }
                else
                {
                    // 查询用户余额  derectPrice.price>余额
                    if (user.use_amount < derectPrice.price)
                    {
                        // 预付费次数为0，余额不足
                        request.Enum = RequestEnum.预付费次数为0且余额不足;
                    }
                }
            }


            // 用户余额验证
            if (!string.IsNullOrEmpty(request.Message))
            {
                return Ok(request);
            }
            #endregion





            #region 获取图片
            // 变量初始化
            var face_token = string.Empty;
            (Bitmap, string) bitmap = (null, null);

            if (model.image_type.ToUpper() == "FACE_TOKEN")
            {
                if (model.image.Length > 50)
                {
                    request.Enum = RequestEnum.face_token格式错误;
                    return Ok(request);
                }
                var detect = await Service_user_face_detect_log.GetUser_Face_Detect_Log_By_Face_token(model.image);

                if (detect == null)
                {
                    request.Enum = RequestEnum.face_token不存在;
                    return Ok(request);
                }
                bitmap.Item2 = detect.image;

                var obj = JsonConvert.DeserializeObject<dynamic>(detect.api_respone);
                request.FaceList = obj.face_list;
                request.FaceNum = obj.face_num;
                face_token = model.image;
            }
            else
            {
                // 获取图片文件
                bitmap = model.image_type.ToUpper() == "BASE64" ? FileCommon.Base64ToBitmap(model.image) : FileCommon.UrlToBitmap(model.image);
                if (bitmap.Item1 == null && !string.IsNullOrEmpty(bitmap.Item2))
                {
                    if (model.image_type.ToUpper() == "BASE64")
                    {
                        request.Enum = RequestEnum.Base64图片格式错误;
                    }
                    else
                    {
                        request.Enum = RequestEnum.从图片的url下载图片失败;
                    }
                    return Ok(request);

                }
            }
            #endregion



            #region 图片以及数据处理
            // 不是 FACE_TOKEN 的处理方式
            if (model.image_type.ToUpper() != "FACE_TOKEN")
            {
                // 查找是否有相同的图片
                face_token = model.image_type.ToUpper() == "BASE64" ? EncryptProvider.Md5(model.image) : EncryptProvider.Md5(FileCommon.BitmapToBase64(bitmap.Item1));
                var detect_Log = await Service_user_face_detect_log.GetUser_Face_Detect_Log_By_Face_token(face_token);
                if (detect_Log != null)
                {
                    bitmap.Item2 = detect_Log.image;
                }
                else
                {
                    try
                    {
                        // 存入本地
                        bitmap.Item2 = FileCommon.SaveBitmap(bitmap.Item1, bitmap.Item2);
                    }
                    catch (Exception ex)
                    {
                        LogHelperNLog.Error(ex);
                        request.Enum = RequestEnum.数据存储处理失败;
                        return Ok(request);
                    }

                }
                // 获取人脸数据
                //var faceDate = FaceDetectionCompute.GetRectangle(bitmap.Item1);
                DlibDotNet.Rectangle[] faceDate = new DlibDotNet.Rectangle[0];
                try
                {
                    // 截取人像，面积排序
                    faceDate = FaceDetectionCompute.GetRectangle(bitmap.Item1).OrderByDescending(d => d.Area).Take(model.max_face_num).ToArray();
                    //faceDate = FaceDetectionCompute.GetResult(bitmap.Item2);
                }
                catch (Exception ex)
                {
                    LogHelperNLog.Error(ex, "FaceDetectionCompute.GetRectangles:人脸获取坐标出错");
                    request.Enum = RequestEnum.无法解析人脸;
                    return Ok(request);
                }

                // 人脸关键点识别
                var landmark68 = FaceLandmarkDetectionCompute.DetsTolandmark68(bitmap.Item1, faceDate);

                if (model.face_field.ToUpper().Contains("LANDMARK"))
                {
                    request.LandmarkList = landmark68.Select(x => x.Item1);
                }

                // 人脸旋转角度
                var angles = FaceDetectionCompute.GetRotationAngle(landmark68.Select(x => x.Item2));

                // 人脸之态预测
                var gesture = FaceDetectionCompute.GetFaceGesture(landmark68.Select(x => x.Item2), bitmap.Item1.Width, bitmap.Item1.Height);
                if (faceDate != null)
                {
                    // 返回数据格式处理
                    var json = JsonConvert.SerializeObject(faceDate);
                    var data = JsonConvert.DeserializeObject<List<FaceLocationsOutMdole>>(json);
                    //data = data.OrderByDescending(d => d.Area).Take(model.max_face_num).ToList();
                    data.ForEach(x => { x.FaceToken = face_token; });
                    for (int i = 0; i < data.Count; i++)
                    {
                        data[i].FaceToken = face_token;
                        data[i].Angle = angles[i];
                        data[i].Gesture = new Gesture()
                        {
                            Pitch = gesture[i].Item1,
                            Yaw = gesture[i].Item2,
                            Roll = gesture[i].Item3
                        };
                    }
                    request.FaceList = data;
                    request.FaceNum = data.Count();
                }
            }
            #endregion

            request.Enum = RequestEnum.Succeed;

            #region 日志&&扣费
            try
            {
                // 预付费/扣费 log
                // 时间戳
                DateTime dateTime = DateTime.Now;
                user_face_detect_prepayment_detail prepayment_Detail = null;
                user_amount_detail amount_Detail = null;
                if (prepayment != null)
                {

                    prepayment_Detail = new user_face_detect_prepayment_detail()
                    {
                        user_id = user.id,
                        change_number = -1,
                        now_number = prepayment.number - 1,
                        info = string.Format("应用ID：{0},图片名称：{1}", model.secret_id, bitmap.Item2),
                        created_at = dateTime,
                        updated_at = dateTime
                    };
                }
                else
                {
                    amount_Detail = new user_amount_detail()
                    {
                        user_id = user.id,
                        change_amount = -derectPrice.price,
                        now_amount = user.use_amount - derectPrice.price,
                        info = string.Format("应用ID：{0},图片名称：{1},费用:{2}", model.secret_id, bitmap.Item2, derectPrice.price),
                        created_at = dateTime,
                        updated_at = dateTime,
                        type = 0
                    };
                }
                // 操作信息
                user_face_detect_log face_Detect_Log = new user_face_detect_log()
                {
                    user_id = user.id,
                    secret_id = model.secret_id,
                    face_token = face_token,
                    image = bitmap.Item2,
                    image_type = model.image_type,
                    face_field = model.face_field,
                    max_face_num = model.max_face_num,
                    face_type = model.face_type,
                    is_deductions_success = true,
                    api_respone = JsonConvert.SerializeObject(request, UnderlineSplitContractResolver.GetSettings()),
                    timestamp = Timelong.GetUnixDateTime(model.timestamp),
                    sign = sign,
                    created_at = dateTime,
                    updated_at = dateTime,
                    is_search = 1,
                };



                // 修改余额，存库，日志
                using (var tra = SqlDapperHelper.GetOpenConnection().BeginTransaction())
                {
                    if (prepayment != null)
                    {
                        await SqlDapperHelper.ExecuteInsertAsync(prepayment_Detail, tra);
                        await Service_user_face_detect_prepayment.MinusUsernumber_By_User_id(prepayment_Detail, tra);
                    }
                    else
                    {
                        await SqlDapperHelper.ExecuteInsertAsync(amount_Detail, tra);
                        await Service_user.MinusUserAmount_By_User_id(amount_Detail);
                    }
                    await SqlDapperHelper.ExecuteInsertAsync(face_Detect_Log, tra);
                    tra.Commit();
                }
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex, "操作日志添加失败");
                request.Enum = RequestEnum.操作日志添加失败;
                return Ok(request);
            }
            #endregion


            return Ok(request);
        }

    }
}
