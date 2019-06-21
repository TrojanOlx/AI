using DlibCore.Computer;
using FaceDlib.Core.Common;
using FaceDlib.Core.Common.DBHelper;
using FaceDlib.Core.Common.Encrypt;
using FaceDlib.Core.Models.Common;
using FaceDlib.Core.Models.Models;
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
    public class FaceDatabaseController : Controller
    {
        /// <summary>
        /// 用户信息查询
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ReadUserInfo(FaceDatabaseModel.GroupUserModel model)
        {
            // 一系列检查
            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null,
                FaceList = null
            };

            var userFaceSet = Verify(ref request, model);
            // 基本验证
            if (userFaceSet == null)
            {
                return Ok(request);
            }




            List<FaceDatabaseModel.UserList> userLists = new List<FaceDatabaseModel.UserList>();
            if (model.group_id == "@ALL")
            {
                userLists = await Service_user_face_storage.GetStorage_By_UGS_ALL<FaceDatabaseModel.UserList>(model.user_id, model.secret_id);
            }
            else
            {
                var group = await Service_user_face_storage_group.Get_storage_group_BySecretGroupAsync(model.secret_id, model.group_id);
                if (group == null)
                {
                    request.Enum = RequestEnum.数据不存在;
                    return Ok(request);
                }
                userLists = await Service_user_face_storage.GetStorage_By_UGS<FaceDatabaseModel.UserList>(model.user_id, group.id, model.secret_id);
            }


            request.UserList = userLists.Select(x => new
            {
                user_info = x.api_user_info,
                group_id = x.group_name
            });

            request.Enum = RequestEnum.Succeed;
            return Ok(request);

        }




        /// <summary>
        /// 查找分组
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ReadtGroupList(FaceDatabaseModel.GroupListModel model)
        {

            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null,
                FaceList = null
            };

            // 基本验证
            var userFaceSet = Verify(ref request, model);
            if (userFaceSet == null)
            {
                return Ok(request);
            }

            try
            {
                List<string> groupIDList = new List<string>();


                var groupList = await Service_user_face_storage_group.GetUser_Face_Storage_Groups_BySecretIdAsunc(model.secret_id, model.start, model.length);

                groupList.ForEach(x =>
                {
                    groupIDList.Add(x.group_name);
                });

                request.GroupIdList = groupIDList;

                request.Enum = RequestEnum.Succeed;
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex, "查找数据出错");
                request.Enum = RequestEnum.服务端请求失败;
            }

            return Ok(request);

        }


        /// <summary>
        /// 创建用户组
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> CreatGroup(FaceDatabaseModel.GroupModel model)
        {
            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null,
                FaceList = null
            };

            //FaceDatabaseModel.CreatGroupModel creatGroup = new FaceDatabaseModel.CreatGroupModel();

            // 基本验证
            var userFaceSet = Verify(ref request, model);
            if (userFaceSet == null)
            {
                return Ok(request);
            }

            // 是否重复验证
            var group = await Service_user_face_storage_group.Get_storage_group_BySecretGroupAsync(model.secret_id, model.group_id);
            if (group != null)
            {
                request.Enum = RequestEnum.数据重复;
                return Ok(request);
            }

            // 扣费等验证


            // 保存到数据库
            request.Enum = RequestEnum.Succeed;
            DateTime dateTime_Now = DateTime.Now;
            try
            {
                using (var tra = SqlDapperHelper.GetOpenConnection().BeginTransaction())
                {

                    long num = 0;

                    user_face_storage_group storage_Group = new user_face_storage_group()
                    {
                        user_id = userFaceSet.user_id,
                        group_name = model.group_id,
                        remake = "",
                        created_at = dateTime_Now,
                        secret_id = model.secret_id,
                        updated_at = dateTime_Now,
                        api_respone = JsonConvert.SerializeObject(request, UnderlineSplitContractResolver.GetSettings()),
                        is_delete = false
                    };

                    num += await SqlDapperHelper.ExecuteInsertAsync(storage_Group);
                    if (num >= 0)
                    {
                        tra.Commit();
                    }
                    else
                    {
                        request.Enum = RequestEnum.数据存储处理失败;
                    }
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


        /// <summary>
        /// 修改分组
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatetGroup()
        {
            return Ok("无需求，暂未开发");
        }

        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> RemoveGroup(FaceDatabaseModel.GroupModel model)
        {
            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null,
                FaceList = null
            };

            // 基本验证
            var userFaceSet = Verify(ref request, model);
            if (userFaceSet == null)
            {
                return Ok(request);
            }

            // 是否重复验证
            var group = await Service_user_face_storage_group.Get_storage_group_BySecretGroupAsync(model.secret_id, model.group_id);
            if (group == null)
            {
                request.Enum = RequestEnum.数据不存在;
                return Ok(request);
            }

            group.is_delete = true;//是否移除
            group.updated_at = DateTime.Now;
            try
            {
                using (var tra = SqlDapperHelper.GetOpenConnection().BeginTransaction())
                {
                    // 修改用户的删除状态
                    await Service_user_face_storage.Remove_Storage_By_Group(group, tra);
                    // 修改用户组的删除状态
                    await Service_user_face_storage_group.Remove_storage_group(group, tra);
                    tra.Commit();
                    request.Enum = RequestEnum.Succeed;
                }
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex, "操作日志添加失败");
                request.Enum = RequestEnum.操作日志添加失败;
            }



            return Ok(request);

        }


        /// <summary>
        /// 获取用户人脸列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ActionResult> ReadtFaceList(FaceDatabaseModel.GroupUserModel model)
        {
            // 一系列检查
            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null,
                FaceList = null
            };

            var userFaceSet = Verify(ref request, model);
            // 基本验证
            if (userFaceSet == null)
            {
                return Ok(request);
            }

            var group = await Service_user_face_storage_group.Get_storage_group_BySecretGroupAsync(model.secret_id, model.group_id);
            if (group == null)
            {
                request.Enum = RequestEnum.数据不存在;
                return Ok(request);
            }

            try
            {
                var userLists = await Service_user_face_storage.GetUserList_By_UGS(model.user_id, group.id, model.secret_id);

                request.Enum = RequestEnum.Succeed;
                request.FaceList = userLists.Select(x => new
                {
                    x.face_token,
                    ctime = x.created_at
                });
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex);
                request.Enum = RequestEnum.服务端请求失败;
            }

            return Ok(request);

        }

        /// <summary>
        /// 人脸注册
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> CreatFace(FaceDatabaseModel.UserFaceModel model)
        {
            // 一系列检查
            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null,
                FaceList = null
            };

            var userFaceSet = Verify(ref request, model);
            // 基本验证
            if (userFaceSet == null)
            {
                return Ok(request);
            }


            #region 余额逻辑判断?


            // 判断是否有同样的人，判断是否有此分组
            var group = await Service_user_face_storage_group.Get_storage_group_BySecretGroupAsync(model.secret_id, model.group_id);
            if (group == null)
            {
                request.Enum = RequestEnum.数据不存在;
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



            // 判断此人相同数据是否已经存在
            var group_user = await Service_user_face_storage.GetUser_By_UGS(model.user_id, group.id, face_token, model.secret_id);

            if (group_user != null)
            {
                request.Enum = RequestEnum.数据重复;
                return Ok(request);
            }





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

            // 获取人脸数据
            //var faceDate = FaceDetectionCompute.GetRectangle(bitmap.Item1);
            DlibDotNet.Rectangle[] faceDate = new DlibDotNet.Rectangle[0];
            try
            {
                // 截取人像，面积排序
                faceDate = FaceDetectionCompute.GetRectangle(bitmap.Item1).OrderByDescending(d => d.Area).Take(1).ToArray();
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex, "FaceDetectionCompute.GetRectangles:人脸获取坐标出错");
                request.Enum = RequestEnum.无法解析人脸;
                return Ok(request);
            }

            #endregion

            request.Enum = RequestEnum.Succeed;
            request.Location = faceDate;



            #region 日志&&扣费

            try
            {

                DateTime dateTime = DateTime.Now;

                // 图片保存到数据库
                user_images = new user_face_image_data()
                {
                    token = face_token,
                    url = bitmap.Item2,
                    created_at = dateTime,
                    updated_at = dateTime
                };


                //用户信息
                user_face_storage face_Storage = new user_face_storage()
                {
                    user_id = userFaceSet.user_id,
                    secret_id = model.secret_id,
                    face_token = face_token,
                    image = bitmap.Item2,
                    image_type = model.image_type,
                    api_group_id = group.id,
                    api_user_id = model.user_id,
                    api_user_info = model.user_info,
                    quality_control = model.quality_control,
                    liveness_control = model.quality_control,
                    sign = model.sign,
                    timestamp = model.timestamp,
                    is_delete = false,
                    api_respone = JsonConvert.SerializeObject(request, UnderlineSplitContractResolver.GetSettings()),
                    created_at = dateTime,
                    updated_at = dateTime
                };

                using (var tra = SqlDapperHelper.GetOpenConnection().BeginTransaction())
                {
                    if (user_images == null)
                    {
                        await Service_user_face_image_data.Creatuser_face_image_data(user_images, tra);
                    }
                    await SqlDapperHelper.ExecuteInsertAsync(face_Storage, tra);
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

        /// <summary>
        /// 人脸更新
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> UpdateFace(FaceDatabaseModel.UserFaceModel model)
        {


            // 一系列检查
            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null,
                FaceList = null
            };

            var userFaceSet = Verify(ref request, model);
            // 基本验证
            if (userFaceSet == null)
            {
                return Ok(request);
            }

            // 判断是否存在此用户，是否存在此分组
            // 判断是否有同样的人，判断是否有此分组
            var group = await Service_user_face_storage_group.Get_storage_group_BySecretGroupAsync(model.secret_id, model.group_id);
            if (group == null)
            {
                request.Enum = RequestEnum.数据不存在;
                return Ok(request);
            }

            user_face_storage group_user = null;
            if (model.group_id == "@ALL")
            {
                group_user = await Service_user_face_storage.GetUser_By_UGS_ALL(model.user_id, model.secret_id);
            }
            else
            {
                group_user = await Service_user_face_storage.GetUser_By_UGS(model.user_id, group.id, model.secret_id);
            }
            if (group_user == null)
            {
                request.Enum = RequestEnum.数据不存在;
                return Ok(request);
            }



            // 将以前的人脸状态变为删除
            user_face_storage user = new user_face_storage()
            {
                is_delete = true,
                updated_at = DateTime.Now,
                secret_id = model.secret_id
            };

            // 增加一个人脸
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

            // 查询 图片库
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

            // 获取人脸数据
            //var faceDate = FaceDetectionCompute.GetRectangle(bitmap.Item1);
            DlibDotNet.Rectangle[] faceDate = new DlibDotNet.Rectangle[0];
            try
            {
                // 截取人像，面积排序
                faceDate = FaceDetectionCompute.GetRectangle(bitmap.Item1).OrderByDescending(d => d.Area).Take(1).ToArray();
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex, "FaceDetectionCompute.GetRectangles:人脸获取坐标出错");
                request.Enum = RequestEnum.无法解析人脸;
                return Ok(request);
            }

            #endregion

            request.Enum = RequestEnum.Succeed;
            request.Location = faceDate;

            #region 日志&&扣费

            try
            {

                DateTime dateTime = DateTime.Now;

                // 图片保存到数据库
                user_images = new user_face_image_data()
                {
                    token = face_token,
                    url = bitmap.Item2,
                    created_at = dateTime,
                    updated_at = dateTime
                };


                //查找分组信息

                user.api_group_id = group.id;


                //用户信息
                user_face_storage face_Storage = new user_face_storage()
                {
                    user_id = userFaceSet.user_id,
                    secret_id = model.secret_id,
                    face_token = face_token,
                    image = bitmap.Item2,
                    image_type = model.image_type,
                    api_group_id = group.id,
                    api_user_id = model.user_id,
                    api_user_info = model.user_info,
                    quality_control = model.quality_control,
                    liveness_control = model.quality_control,
                    sign = model.sign,
                    timestamp = model.timestamp,
                    is_delete = false,
                    api_respone = JsonConvert.SerializeObject(request, UnderlineSplitContractResolver.GetSettings()),
                    created_at = dateTime,
                    updated_at = dateTime
                };

                using (var tra = SqlDapperHelper.GetOpenConnection().BeginTransaction())
                {
                    await Service_user_face_storage.Remove_Storage_By_ApiUserID(user, tra);
                    if (user_images == null)
                    {
                        await Service_user_face_image_data.Creatuser_face_image_data(user_images, tra);
                    }
                    await SqlDapperHelper.ExecuteInsertAsync(face_Storage, tra);
                    tra.Commit();
                }
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex, "操作日志添加失败");
                request.Enum = RequestEnum.操作日志添加失败;
            }

            #endregion

            return Ok(request);

        }

        /// <summary>
        /// 人脸删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ActionResult> RemoveFace(FaceDatabaseModel.RemoveFaceModel model)
        {
            // 一系列检查
            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null,
                FaceList = null
            };

            var userFaceSet = Verify(ref request, model);
            // 基本验证
            if (userFaceSet == null)
            {
                return Ok(request);
            }


            // 判断是否有同样的人，判断是否有此分组
            var group = await Service_user_face_storage_group.Get_storage_group_BySecretGroupAsync(model.secret_id, model.group_id);
            if (group == null)
            {
                request.Enum = RequestEnum.数据不存在;
                return Ok(request);
            }


            // 判断此人相同数据是否已经存在
            var group_user = await Service_user_face_storage.GetUser_By_UGS(model.user_id, group.id, model.face_token, model.secret_id);

            if (group_user == null)
            {
                request.Enum = RequestEnum.数据不存在;
                return Ok(request);
            }

            group_user.is_delete = true;
            group.updated_at = DateTime.Now;

            try
            {
                // 数据库删除，增加日志
                await Service_user_face_storage.Remove_Storage_By_ApiUserIDFaceToken(group_user);
                request.Enum = RequestEnum.Succeed;
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex, "操作日志添加失败");
                request.Enum = RequestEnum.操作日志添加失败;
            }

            return Ok(request);


        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> RemoveUser(FaceDatabaseModel.GroupUserModel model)
        {
            // 一系列检查
            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null,
                FaceList = null
            };

            var userFaceSet = Verify(ref request, model);
            // 基本验证
            if (userFaceSet == null)
            {
                return Ok(request);
            }

            // 判断是否存在此用户，是否存在此分组
            // 判断是否有同样的人，判断是否有此分组
            var group = await Service_user_face_storage_group.Get_storage_group_BySecretGroupAsync(model.secret_id, model.group_id);
            if (group == null)
            {
                request.Enum = RequestEnum.数据不存在;
                return Ok(request);
            }

            user_face_storage group_user = null;
            if (model.group_id == "@ALL")
            {
                group_user = await Service_user_face_storage.GetUser_By_UGS_ALL(model.user_id, model.secret_id);
            }
            else
            {
                group_user = await Service_user_face_storage.GetUser_By_UGS(model.user_id, group.id, model.secret_id);
            }
            if (group_user == null)
            {
                request.Enum = RequestEnum.数据不存在;
                return Ok(request);
            }



            user_face_storage user = new user_face_storage()
            {
                is_delete = true,
                updated_at = DateTime.Now,
                secret_id = model.secret_id
            };

            try
            {
                // 数据库删除，增加日志
                if (model.group_id == "@ALL")
                {
                    await Service_user_face_storage.Remove_Storage_By_ApiUserID_All(user);
                }
                else
                {
                    user.api_group_id = group.id;
                    await Service_user_face_storage.Remove_Storage_By_ApiUserID(user);
                }
                request.Enum = RequestEnum.Succeed;
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex, "操作日志添加失败");
                request.Enum = RequestEnum.操作日志添加失败;
            }

            return Ok(request);

        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        public async Task<ActionResult> ReadtGroupUserList(FaceDatabaseModel.GroupUserListModel model)
        {
            // 一系列检查
            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null,
                FaceList = null
            };

            var userFaceSet = Verify(ref request, model);
            // 基本验证
            if (userFaceSet == null)
            {
                return Ok(request);
            }

            var group = await Service_user_face_storage_group.Get_storage_group_BySecretGroupAsync(model.secret_id, model.group_id);
            if (group == null)
            {
                request.Enum = RequestEnum.数据不存在;
                return Ok(request);
            }

            try
            {


                var userLists = await Service_user_face_storage.GetUser_Face_Storage_By_UGS_Asunc(model.secret_id, group.id, model.start, model.length);


                List<string> userIDList = new List<string>();
                userLists.ForEach(x =>
                {
                    userIDList.Add(x.api_user_id);
                });

                request.UserIdList = userIDList;

                request.Enum = RequestEnum.Succeed;
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex);
                request.Enum = RequestEnum.服务端请求失败;
            }
            return Ok(request);

        }

        /// <summary>
        /// 复制用户
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> CopyGroupUser(FaceDatabaseModel.CopyUserListModel model)
        {
            // 一系列检查
            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null,
                FaceList = null
            };

            var userFaceSet = Verify(ref request, model);
            // 基本验证
            if (userFaceSet == null)
            {
                return Ok(request);
            }

            // 判断是否存在此用户，是否存在此分组
            // 判断是否有同样的人，判断是否有此分组
            var group = await Service_user_face_storage_group.Get_storage_group_BySecretGroupAsync(model.secret_id, model.src_group_id);
            if (group == null)
            {
                request.Enum = RequestEnum.数据不存在;
                return Ok(request);
            }

            var group_user = await Service_user_face_storage.GetUser_By_UGS(model.user_id, group.id, model.secret_id);
            if (group_user == null)
            {
                request.Enum = RequestEnum.数据不存在;
                return Ok(request);
            }

            var newgroup = await Service_user_face_storage_group.Get_storage_group_BySecretGroupAsync(model.secret_id, model.dst_group_id);
            if (newgroup == null)
            {
                request.Enum = RequestEnum.数据不存在;
                return Ok(request);
            }

            var newgroup_user = await Service_user_face_storage.GetUser_By_UGS(model.user_id, newgroup.id, model.secret_id);
            if (newgroup_user != null)
            {
                request.Enum = RequestEnum.数据重复;
                return Ok(request);
            }



            group_user.api_group_id = newgroup.id;

            try
            {
                await SqlDapperHelper.ExecuteInsertAsync(group_user);
                request.Enum = RequestEnum.Succeed;
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex, "操作日志添加失败");
                request.Enum = RequestEnum.操作日志添加失败;
            }

            return Ok(request);

        }

        /// <summary>
        /// 基本验证
        /// </summary>
        /// <param name="request"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public user_face_set Verify<T>(ref RequestFaceModel request, T model)
        {
            #region 基本验证
            if (!ModelState.IsValid)
            {
                request.Message = ModelState.Where(e => e.Value.Errors.Count > 0).FirstOrDefault().Value.Errors[0].ErrorMessage;
                request.Status = (int)RequestEnum.必要参数未传入;
                return null;
            }

            // 根据 secretkey 获取set
            var userFaceSet = Service_user_face_set.GetUser_Face_Set_By_Secret_key((model as VerifyBase).secret_id);
            if (userFaceSet == null)
            {
                // 您未开通此项目
                request.Enum = RequestEnum.没有接口权限;
                return null;
            }


            // 签名判断
            var dic = ModleToDictionary.GetProperties(model);
            var key = userFaceSet.secret_key;
            dic.Remove("sign");

            var sign = Signature.GetSign(dic, key);
            if (sign != (model as VerifyBase).sign)
            {
                // 签名错误
                request.Enum = RequestEnum.签名错误;
                return null;
            }
            #endregion

            return userFaceSet;

        }


    }
}
