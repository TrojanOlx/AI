using FaceDlib.Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DlibDotNet;
using DlibDotNet.Extensions;
using Dlib = DlibDotNet.Dlib;
using DlibDotNet.Dnn;
using System.Drawing;
using System.Linq;
using Rectangle = DlibDotNet.Rectangle;
using FaceDlib.Core.Service;
using FaceDlib.Core.Models.Common;
using FaceDlib.Core.Models.ViewModel;
using Newtonsoft.Json;
using FaceDlib.Core.Common.Encrypt;
using DlibCore.Computer;
using FaceDlib.Core.Models.Models;
using FaceDlib.Core.Common.DBHelper;

namespace FaceDlib.Core.Api.Controllers
{
    public class DnnFaceRecognitionController : Controller
    {

        public async Task<ActionResult> FaceContrast([FromBody]FaceContrastViewModel model)
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
            dic.Remove("face_field");
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
            //Service_user_face_compare_prepayment
            // 根据user_id 先查预付款，未查询到则扣费使用
            var prepayment = await Service_user_face_compare_prepayment.GetUser_Face_Compare_Prepayment_By_User_id(user.id);
            user_face_compare_price comparePrice = null;
            if (prepayment == null || prepayment.number <= 0)
            {
                prepayment = null;
                comparePrice = await Service_user_face_compare_price.GetUser_Face_Compare_Price_By_User_idAsync(user.id);
                if (comparePrice == null)
                {
                    // 用户没有预付费次数
                    request.Enum = RequestEnum.没有预付费次数未开通余额付款;
                }
                else
                {
                    // 查询用户余额  derectPrice.price>余额
                    if (user.use_amount < comparePrice.price)
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

            List<(Bitmap, string)> bitmaps = new List<(Bitmap, string)>();

            List<ContrastData> face_tokens = new List<ContrastData>();

            for (int i = 0; i < model.images.Count; i++)
            {

                var image = model.images[i];

                var face_token = string.Empty;
                (Bitmap, string) bitmap = (null, null);
                // 获取图片文件
                bitmap = image.image_type.ToUpper() == "BASE64" ? FileCommon.Base64ToBitmap(image.image) : FileCommon.UrlToBitmap(image.image);
                if (bitmap.Item1 == null && !string.IsNullOrEmpty(bitmap.Item2))
                {
                    if (image.image_type.ToUpper() == "BASE64")
                    {
                        request.Enum = RequestEnum.Base64图片格式错误;
                    }
                    else
                    {
                        request.Enum = RequestEnum.从图片的url下载图片失败;
                    }
                    return Ok(request);
                }

                #endregion

                #region 图片以及数据处理
                // 查找是否有相同的图片
                face_token = image.image_type.ToUpper() == "BASE64" ? EncryptProvider.Md5(image.image) : EncryptProvider.Md5(FileCommon.BitmapToBase64(bitmap.Item1));


                // 查询 图片库 // 判断是否还需要进行图片存储
                var user_images = await Service_user_face_image_data.Getuser_face_image_data_By_Token_Async(face_token);

                if (user_images != null)
                {
                    bitmap.Item2 = user_images.url;
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

                bitmaps.Add(bitmap);
                face_tokens.Add(new ContrastData()
                {
                    face_token = face_token
                });
            }

            #region old Code

            //foreach (var image in model.images)
            //{
            //    // 变量初始化
            //    var face_token = string.Empty;
            //    (Bitmap, string) bitmap = (null, null);

            //    bitmap = image.image_type.ToUpper() == "BASE64" ? FileCommon.Base64ToBitmap(image.image) : FileCommon.UrlToBitmap(image.image);

            //    if (bitmap.Item1 == null && !string.IsNullOrEmpty(bitmap.Item2))
            //    {
            //        if (image.image_type.ToUpper() == "BASE64")
            //        {
            //            request.Enum = RequestEnum.Base64图片格式错误;
            //            request.Message = string.IsNullOrEmpty(request.Message) ? num + RequestEnum.Base64图片格式错误.ToString() : request.Message + num + RequestEnum.Base64图片格式错误.ToString();
            //        }
            //        else
            //        {
            //            request.Enum = RequestEnum.从图片的url下载图片失败;
            //            request.Message = string.IsNullOrEmpty(request.Message) ? num + RequestEnum.从图片的url下载图片失败.ToString() : request.Message + num + RequestEnum.从图片的url下载图片失败.ToString();
            //        }
            //        return Ok(request);
            //    }
            //    bitmaps.Add(bitmap);
            //}
            //#endregion

            //#region 图片以及数据处理

            //List<ContrastData> face_tokens = new List<ContrastData>();
            //for (int i = 0; i < bitmaps.Count; i++)
            //{
            //    var face_token = string.Empty;
            //    face_token = EncryptProvider.Md5(FileCommon.BitmapToBase64(bitmaps[i].Item1));

            //    //此处后期要更改为人脸库的图片
            //    var detect_Log = await Service_user_face_detect_log.GetUser_Face_Detect_Log_By_Face_token(face_token);
            //    if (detect_Log != null)
            //    {
            //        bitmaps[i] = (bitmaps[i].Item1, detect_Log.image);
            //        //bitmaps[i].Item2 = detect_Log.image;
            //    }
            //    else
            //    {
            //        try
            //        {
            //            // 存入本地
            //            bitmaps[i] = (bitmaps[i].Item1, FileCommon.SaveBitmap(bitmaps[i].Item1, bitmaps[i].Item2));
            //        }
            //        catch (Exception ex)
            //        {
            //            LogHelperNLog.Error(ex);
            //            request.Enum = RequestEnum.数据存储处理失败;
            //            return Ok(request);
            //        }

            //    }
            //    face_tokens.Add(new ContrastData() {
            //        face_token=face_token
            //    });
            //}
            #endregion


            var difference = DnnFaceRecognitionCompute.GetOutputLabels(bitmaps[0].Item1, bitmaps[1].Item1);

            // 是否需要人脸位置
            if (model.face_field.ToUpper().Contains("FACE_SHAPE"))
            {
                for (int i = 0; i < face_tokens.Count; i++)
                {
                    face_tokens[i].rectangles = difference.Item2[i];
                }
            }
            
            request.FaceList = face_tokens;
            request.Score = difference.Item1;
            #endregion

            request.Enum = RequestEnum.Succeed;


            //日志&&扣费
            try
            {
                // 预付费/扣费 log
                // 时间戳
                DateTime dateTime = DateTime.Now;
                user_face_compare_prepayment_detail prepayment_Detail = null;
                user_amount_detail amount_Detail = null;
                if (prepayment != null)
                {
                    prepayment_Detail = new user_face_compare_prepayment_detail()
                    {
                        user_id = user.id,
                        change_number = -1,
                        now_number = prepayment.number - 1,
                        info = string.Format("应用ID：{0}", model.secret_id),
                        created_at = dateTime,
                        updated_at = dateTime
                    };
                }
                else
                {
                    amount_Detail = new user_amount_detail()
                    {
                        user_id = user.id,
                        change_amount = -comparePrice.price,
                        now_amount = user.use_amount - comparePrice.price,
                        info = string.Format("应用ID：{0},费用:{1}", model.secret_id, comparePrice.price),
                        created_at = dateTime,
                        updated_at = dateTime,
                        type = 0
                    };
                }

                // 操作信息
                user_face_compare_log face_Compare_Log = new user_face_compare_log()
                {
                    user_id = user.id,
                    secret_id = model.secret_id,
                    image = JsonConvert.SerializeObject(bitmaps.Select(x=>x.Item2)),
                    is_deductions_success = true,
                    api_respone = JsonConvert.SerializeObject(request, UnderlineSplitContractResolver.GetSettings()),
                    timestamp = Timelong.GetUnixDateTime(model.timestamp),
                    sign = sign,
                    created_at = dateTime,
                    updated_at = dateTime,
                };

                // 修改余额，存库，日志
                using (var tra = SqlDapperHelper.GetOpenConnection().BeginTransaction())
                {
                    if (prepayment != null)
                    {
                        await SqlDapperHelper.ExecuteInsertAsync(prepayment_Detail, tra);
                        await Service_user_face_compare_prepayment.MinusUsernumber_By_User_id(prepayment_Detail, tra);
                    }
                    else
                    {
                        await SqlDapperHelper.ExecuteInsertAsync(amount_Detail, tra);
                        await Service_user.MinusUserAmount_By_User_id(amount_Detail);
                    }
                    await SqlDapperHelper.ExecuteInsertAsync(face_Compare_Log, tra);
                    tra.Commit();
                }


            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex, "操作日志添加失败");
                request.Enum = RequestEnum.操作日志添加失败;
                return Ok(request);
            }



            return Ok(request);
        }






















        //private ShapePredictor sp = null;
        //private LossMetric net = null;
        //public async Task<ActionResult> FaceContrast(List<IFormFile> files)
        //{
        //    // 将文件转化为图片
        //    var bitmaps = FileCommon.FileToBitmap(files);
        //    List<Rectangle[]> rectangles = new List<Rectangle[]>();

        //    var faceDescriptor1 = GetOutputLabels(bitmaps[0].Item1);
        //    var faceDescriptor2 = GetOutputLabels(bitmaps[1].Item1);

        //    if (faceDescriptor1.Item1 == null || faceDescriptor2.Item1 == null)
        //    {
        //        return Ok(new { start = false, difference = 99.99, rectangles });
        //    }
        //    var diff = faceDescriptor1.Item1[0] - faceDescriptor2.Item1[0];
        //    rectangles.Add(faceDescriptor1.Item2);
        //    rectangles.Add(faceDescriptor2.Item2);
        //    var difference = Dlib.Length(diff);
        //    return Ok(new { start = difference < 0.6, difference, rectangles });
        //}

        ///// <summary>
        ///// 计算
        ///// </summary>
        ///// <param name="bitmap"></param>
        ///// <returns>item1:128向量值，item2:坐标位置</returns>
        //public (OutputLabels<Matrix<float>>, Rectangle[]) GetOutputLabels(Bitmap bitmap)
        //{
        //    var dets = new Rectangle[0];
        //    try
        //    {
        //        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        //        //使用一个人脸标记模型来将人脸与标准姿势对齐
        //        if (sp == null)
        //        {
        //            sp = ShapePredictor.Deserialize(basePath + "ShapeModel/shape_predictor_5_face_landmarks.dat");
        //        }
        //        //加载负责人脸识别的DNN。
        //        if (net == null)
        //        {
        //            net = LossMetric.Deserialize(basePath + "ShapeModel/dlib_face_recognition_resnet_model_v1.dat");
        //        }
        //        var faces = new List<Matrix<RgbPixel>>();
        //        // 获取缓存目录
        //        //var filePath = Path.GetTempFileName();
        //        //在图像中寻找人脸我们需要一个人脸检测器:
        //        using (var detector = Dlib.GetFrontalFaceDetector())
        //        {
        //            //var fileUrl = string.Format(@"{0}{1}\{2}", basePath, DateTime.Today.ToString("yyyy-MM-dd"), bitmap);
        //            //Dlib.LoadImageAsMatrix<RgbPixel>(fileUrl)
        //            using (var img = bitmap.ToMatrix<RgbPixel>())
        //            {
        //                dets = detector.Operator(img);
        //                foreach (var face in dets)
        //                {
        //                    var shape = sp.Detect(img, face);
        //                    var faceChipDetail = Dlib.GetFaceChipDetails(shape, 150, 0.25);
        //                    var faceChip = Dlib.ExtractImageChip<RgbPixel>(img, faceChipDetail);
        //                    faces.Add(faceChip);
        //                }
        //                if (!faces.Any())
        //                {
        //                    //Console.WriteLine("No faces found in image!");
        //                    return (null, dets);
        //                }
        //            }
        //        }
        //        //此调用要求DNN将每个人脸图像转换为128D矢量。
        //        //在这个128D的矢量空间中，来自同一个人的图像会彼此接近
        //        //但是来自不同人的载体将会非常不同。所以我们可以用这些向量
        //        //辨别一对图片是来自同一个人还是不同的人。  
        //        return (net.Operator(faces), dets);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelperNLog.Error(ex, "FileCommon.OutputLabels 人脸对比出错");
        //        return (null, dets);
        //    }
        //}

    }
}
