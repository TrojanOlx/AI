using DlibCore.Computer;
using FaceDlib.Core.Common;
using FaceDlib.Core.Common.Encrypt;
using FaceDlib.Core.Models.Common;
using FaceDlib.Core.Models.ViewModel;
using FaceDlib.Core.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceDlib.Core.Api.Controllers
{
    public class FaceFindController : Controller
    {
        public async Task<ActionResult> Search(FaceFindModel.Search model)
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

            #region 获取图片

            var face_token = string.Empty;
            (Bitmap, string) bitmap = (null, null);
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

            #endregion



            #region 图片以及数据处理

            // 查找是否有相同的图片
            face_token = model.image_type.ToUpper() == "BASE64" ? EncryptProvider.Md5(model.image) : EncryptProvider.Md5(FileCommon.BitmapToBase64(bitmap.Item1));

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

            #endregion



            //var bitmaps = "从数据库查询，然后加载Bitmap";

            // 查询分组
            var groupList = await Service_user_face_storage_group.Get_storage_group_BySecretGroupAsync(model.secret_id, model.group_id_list.Split(','));

            // 查询到用户信息
            var users = await Service_user_face_storage.GetUserList_By_UGS(model.user_id, groupList.Select(x => x.id).ToList(), model.secret_id);

            


            var bitmaps = FileCommon.SrcToBitmap(users.Select(x => x.image));

            var difference = DnnFaceRecognitionCompute.GetOutputLabels(bitmap.Item1, bitmaps);

            // 过滤相识度
            List<FaceFindOutModel.User> list = new List<FaceFindOutModel.User>();

            for (int i = 0; i < bitmaps.Count; i++)
            {
                var group = groupList.FirstOrDefault(x => x.id == users[i].api_group_id);
                list.Add(new FaceFindOutModel.User
                {
                    GroupId = group != null ? group.group_name : null,
                    UserId = users[i].api_user_id,
                    UserInfo = users[i].api_user_info,
                    Score = difference[i].Item1
                });
            }

            request.FaceToken = face_token;
            request.FaceList = list;


            // 数据处理，存库，日志

            return Ok(request);
        }


        public async Task<ActionResult> MultiSearch(FaceFindModel.MultiSearch model)
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

            #region 获取图片

            var face_token = string.Empty;
            (Bitmap, string) bitmap = (null, null);
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

            #endregion

            #region 图片以及数据处理

            // 查找是否有相同的图片
            face_token = model.image_type.ToUpper() == "BASE64" ? EncryptProvider.Md5(model.image) : EncryptProvider.Md5(FileCommon.BitmapToBase64(bitmap.Item1));

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

            #endregion

            // 查询分组
            var groupList = await Service_user_face_storage_group.Get_storage_group_BySecretGroupAsync(model.secret_id, model.group_id_list.Split(','));

            // 获取所有用户
            var users = await Service_user_face_storage.GetUser_Face_Storage_By_UGS_Asunc_All(model.secret_id,groupList.Select(x=>x.id).ToList());


            List<FaceFindOutModel.User> list = new List<FaceFindOutModel.User>();
            foreach (var item in users)
            {
                var bitmaps = FileCommon.SrcToBitmap(item.image);
                var difference = DnnFaceRecognitionCompute.GetOutputLabels(bitmap.Item1, bitmaps);
                var group = groupList.FirstOrDefault(x => x.id == item.api_group_id);
                list.Add(new FaceFindOutModel.User
                {
                    GroupId = group != null ? group.group_name : null,
                    UserId = item.api_user_id,
                    UserInfo = item.api_user_info,
                    Score = difference.Item1
                });
            }

            request.FaceToken = face_token;
            request.FaceList = list;


            // 数据处理，存库，日志

            return Ok(request);
        }
    }

    

}
