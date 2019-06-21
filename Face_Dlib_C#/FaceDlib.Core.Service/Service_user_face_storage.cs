using FaceDlib.Core.Common.DBHelper;
using FaceDlib.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FaceDlib.Core.Service
{
    public class Service_user_face_storage
    {
        static string Select = @"SELECT 
	                                PUBLIC.user_face_storage.ID,
	                                PUBLIC.user_face_storage.user_id,
	                                PUBLIC.user_face_storage.secret_id,
	                                PUBLIC.user_face_storage.face_token,
	                                PUBLIC.user_face_storage.image,
	                                PUBLIC.user_face_storage.image_type,
	                                PUBLIC.user_face_storage.api_group_id,
	                                PUBLIC.user_face_storage.api_user_id,
	                                PUBLIC.user_face_storage.api_user_info,
	                                PUBLIC.user_face_storage.quality_control,
	                                PUBLIC.user_face_storage.liveness_control,
	                                PUBLIC.user_face_storage.sign,
	                                PUBLIC.user_face_storage.TIMESTAMP,
	                                PUBLIC.user_face_storage.is_delete,
	                                PUBLIC.user_face_storage.api_respone,
	                                PUBLIC.user_face_storage.created_at,
	                                PUBLIC.user_face_storage.updated_at 
                                FROM
	                                PUBLIC.user_face_storage 
                                WHERE
	                                1 = 1 ";


        static string Update = @"UPDATE PUBLIC.user_face_storage 
                                 SET 
                                    {0} 
                                 WHERE 
                                    1=1 ";




        /// <summary>
        /// 根据应用，分组，用户查询
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="group_id"></param>
        /// <param name="secret_id"></param>
        /// <returns></returns>
        public static async Task<List<T>> GetStorage_By_UGS<T>(string user_id, long group_id, string secret_id)
        {

            string sql = "SELECT * FROM PUBLIC.user_face_storage S left join PUBLIC.user_face_storage_group G on S.api_group_id=G.id WHERE 1 = 1";

            string where = " AND S.api_user_id = @user_id AND S.api_group_id = @group_id AND S.secret_id = @secret_id AND S.is_delete=@is_delete";
            return await SqlDapperHelper.ExecuteReaderRetListAsync<T>(sql + where,
                new { user_id, group_id, secret_id, is_delete = false });
        }


        /// <summary>
        /// 根据应用，分组，用户查询
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="secret_id"></param>
        /// <returns></returns>
        public static async Task<List<T>> GetStorage_By_UGS_ALL<T>(string user_id, string secret_id)
        {

            string sql = "SELECT * FROM PUBLIC.user_face_storage S left join PUBLIC.user_face_storage_group G on S.api_group_id=G.id WHERE 1 = 1";

            string where = " AND S.api_user_id = @user_id AND S.secret_id = @secret_id AND S.is_delete=@is_delete";
            return await SqlDapperHelper.ExecuteReaderRetListAsync<T>(sql + where,
                new { user_id, secret_id, is_delete = false });
        }






        /// <summary>
        /// 根据 应用ID 和 分组ID 查询用户
        /// </summary>
        /// <param name="secret_id"></param>
        /// <param name="group_id"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static async Task<List<user_face_storage>> GetUser_Face_Storage_By_UGS_Asunc(string secret_id, long group_id, int start, int length)
        {
            string sql = "SELECT PUBLIC.user_face_storage.api_user_id FROM PUBLIC.user_face_storage WHERE 1 = 1 ";

            string where = " AND secret_id = @secret_id  AND api_group_id = @group_id AND is_delete = @is_delete";
            string limit = " limit @length offset @start ";
            return await SqlDapperHelper.ExecuteReaderRetListAsync<user_face_storage>(sql + where + limit, new { secret_id, group_id, length, start, is_delete = false });
        }


        /// <summary>
        /// 根据 应用ID 和 分组ID 查询用户
        /// </summary>
        /// <param name="secret_id"></param>
        /// <param name="group_id"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static async Task<List<user_face_storage>> GetUser_Face_Storage_By_UGS_Asunc_All(string secret_id, long group_id)
        {
            string sql = "SELECT PUBLIC.user_face_storage.api_user_id FROM PUBLIC.user_face_storage WHERE 1 = 1 ";

            string where = " AND secret_id = @secret_id  AND api_group_id = @group_id AND is_delete = @is_delete";
            return await SqlDapperHelper.ExecuteReaderRetListAsync<user_face_storage>(sql + where, new { secret_id, group_id, is_delete = false });
        }

        /// <summary>
        /// 根据 应用ID 和 分组ID 查询用户
        /// </summary>
        /// <param name="secret_id"></param>
        /// <param name="group_id"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static async Task<List<user_face_storage>> GetUser_Face_Storage_By_UGS_Asunc_All(string secret_id, List<long> group_ids)
        {
            string sql = "SELECT PUBLIC.user_face_storage.api_user_id FROM PUBLIC.user_face_storage WHERE 1 = 1 ";
            string where = " AND secret_id = @secret_id  AND api_group_id = ANY(@group_ids) AND is_delete = @is_delete";
            return await SqlDapperHelper.ExecuteReaderRetListAsync<user_face_storage>(sql + where, new { secret_id, group_ids, is_delete = false });
        }






        /// <summary>
        /// 根据应用，分组，用户查询
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="group_id"></param>
        /// <param name="secret_id"></param>
        /// <returns></returns>
        public static async Task<user_face_storage> GetUser_By_UGS(string user_id, long group_id, string secret_id)
        {
            string where = @" AND api_user_id = @user_id AND api_group_id = @group_id AND secret_id = @secret_id AND is_delete=@is_delete";
            return await SqlDapperHelper.ExecuteReaderRetTAsync<user_face_storage>(Select + where,
                new { user_id, group_id, secret_id, is_delete = false });
        }


        /// <summary>
        /// 根据应用，分组，用户查询
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="group_id"></param>
        /// <param name="secret_id"></param>
        /// <returns></returns>
        public static async Task<List<user_face_storage>> GetUserList_By_UGS(string user_id, long group_id, string secret_id)
        {
            string where = @" AND api_user_id = @user_id AND api_group_id = @group_id AND secret_id = @secret_id AND is_delete=@is_delete";
            return await SqlDapperHelper.ExecuteReaderRetListAsync<user_face_storage>(Select + where,
                new { user_id, group_id, secret_id, is_delete = false });
        }



        /// <summary>
        /// 根据应用，分组，用户查询
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="group_id"></param>
        /// <param name="secret_id"></param>
        /// <returns></returns>
        public static async Task<List<user_face_storage>> GetUserList_By_UGS(string user_id, List<long> group_ids, string secret_id)
        {
            string where = @" AND api_user_id = @user_id AND api_group_id = ANY(@group_ids) AND secret_id = @secret_id AND is_delete=@is_delete";

            return await SqlDapperHelper.ExecuteReaderRetListAsync<user_face_storage>(Select + where,
            new { user_id, group_ids, secret_id, is_delete = false });

        }






        /// <summary>
        /// 根据应用，分组，用户查询
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="group_id"></param>
        /// <param name="secret_id"></param>
        /// <returns></returns>
        public static async Task<user_face_storage> GetUser_By_UGS(string user_id, long group_id, string face_token, string secret_id)
        {
            string where = @" AND api_user_id = @user_id 
                              AND api_group_id = @group_id 
                              AND face_token = @face_token
                              AND secret_id = @secret_id 
                              AND is_delete = @is_delete";
            return await SqlDapperHelper.ExecuteReaderRetTAsync<user_face_storage>(Select + where,
                new { user_id, group_id, secret_id, face_token, is_delete = false });
        }



        /// <summary>
        /// 根据应用，分组，用户查询
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="group_id"></param>
        /// <param name="secret_id"></param>
        /// <returns></returns>
        public static async Task<user_face_storage> GetUser_By_UGS_ALL(string user_id, string secret_id)
        {
            string where = " AND api_user_id = @user_id AND secret_id = @secret_id AND is_delete=@is_delete";
            return await SqlDapperHelper.ExecuteReaderRetTAsync<user_face_storage>(Select + where,
                new { user_id, secret_id, is_delete = false });
        }





        /// <summary>
        /// 根据用户组将用户的状态变为删除
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tra"></param>
        /// <returns></returns>   Remove_Storage_By_Group
        public static async Task<long> Remove_Storage_By_Group(user_face_storage_group group, IDbTransaction tra = null)
        {
            string set = "is_delete = @is_delete , updated_at = @updated_at";
            string where = " AND api_group_id = @api_group_id AND secret_id = @secret_id";
            return await SqlDapperHelper.ExecuteSqlIntAsync(string.Format(Update + where, set),
                new
                {
                    is_delete = group.is_delete,
                    updated_at = group.updated_at,
                    api_group_id = group.id,
                    secret_id = group.secret_id
                }, tra);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tra"></param>
        /// <returns></returns>
        public static async Task<long> Remove_Storage_By_ApiUserID(user_face_storage user, IDbTransaction tra = null)
        {
            string set = "is_delete = @is_delete , updated_at = @updated_at";
            string where = " AND api_group_id = @api_group_id AND secret_id = @secret_id";
            return await SqlDapperHelper.ExecuteSqlIntAsync(string.Format(Update + where, set),
                new
                {
                    is_delete = user.is_delete,
                    updated_at = user.updated_at,
                    api_group_id = user.api_group_id,
                    secret_id = user.secret_id
                }, tra);
        }



        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tra"></param>
        /// <returns></returns>
        public static async Task<long> Remove_Storage_By_ApiUserIDFaceToken(user_face_storage user, IDbTransaction tra = null)
        {
            string set = "is_delete = @is_delete , updated_at = @updated_at";
            string where = " AND api_group_id = @api_group_id AND secret_id = @secret_id AND face_token = @face_token";
            return await SqlDapperHelper.ExecuteSqlIntAsync(string.Format(Update + where, set),
                new
                {
                    is_delete = user.is_delete,
                    updated_at = user.updated_at,
                    api_group_id = user.api_group_id,
                    secret_id = user.secret_id,
                    face_token = user.face_token
                }, tra);
        }



        /// <summary>
        /// 从所有组中删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tra"></param>
        /// <returns></returns>
        public static async Task<long> Remove_Storage_By_ApiUserID_All(user_face_storage user, IDbTransaction tra = null)
        {
            string set = "is_delete = @is_delete , updated_at = @updated_at";
            string where = " AND secret_id = @secret_id";
            return await SqlDapperHelper.ExecuteSqlIntAsync(string.Format(Update + where, set),
                new
                {
                    is_delete = user.is_delete,
                    updated_at = user.updated_at,
                    secret_id = user.secret_id
                }, tra);
        }






    }
}
