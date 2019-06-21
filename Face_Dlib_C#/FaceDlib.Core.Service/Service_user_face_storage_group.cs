using FaceDlib.Core.Common.DBHelper;
using FaceDlib.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FaceDlib.Core.Service
{
    public class Service_user_face_storage_group
    {

        static string SelectSql = @"SELECT 
	                                    PUBLIC.user_face_storage_group.ID,
	                                    PUBLIC.user_face_storage_group.user_id,
	                                    PUBLIC.user_face_storage_group.group_name,
	                                    PUBLIC.user_face_storage_group.remake,
	                                    PUBLIC.user_face_storage_group.created_at,
	                                    PUBLIC.user_face_storage_group.secret_id,
	                                    PUBLIC.user_face_storage_group.updated_at,
	                                    PUBLIC.user_face_storage_group.api_respone,
	                                    PUBLIC.user_face_storage_group.is_delete 
                                    FROM
	                                    PUBLIC.user_face_storage_group
                                    WHERE 1=1 ";



        static string UpdateSql = @"UPDATE PUBLIC.user_face_storage_group 
                                 SET 
                                    {0} 
                                 WHERE 
                                    1=1 ";


        /// <summary>
        /// 根据应用ID和分组 查找数据
        /// </summary>
        /// <returns></returns>
        public static async Task<user_face_storage_group> Get_storage_group_BySecretGroupAsync(string secret_id, string group_name)
        {
            string where = " AND is_delete=@is_delete AND secret_id = @secret_id AND group_name = @group_name";
            return await SqlDapperHelper.ExecuteReaderRetTAsync<user_face_storage_group>(SelectSql + where, new { secret_id, group_name, is_delete = false });
        }


        /// <summary>
        /// 根据应用ID和分组 查找数据
        /// </summary>
        /// <returns></returns>
        public static async Task<List<user_face_storage_group>> Get_storage_group_BySecretGroupAsync(string secret_id, IEnumerable<string> group_names)
        {
            string where = " AND is_delete=@is_delete AND secret_id = @secret_id AND group_name = ANY(@group_names)";

            return await SqlDapperHelper.ExecuteReaderRetListAsync<user_face_storage_group>(SelectSql + where, new { secret_id, group_names, is_delete = false });

        }




        public static user_face_storage_group Get_storage_group_ByID(long id)
        {
            string where = " AND id = @id";
            return SqlDapperHelper.ExecuteReaderReturnT<user_face_storage_group>(SelectSql + where, new { id });
        }


        /// <summary>
        /// 根据应用ID 查询分组
        /// </summary>
        /// <param name="secret_id"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static async Task<List<user_face_storage_group>> GetUser_Face_Storage_Groups_BySecretIdAsunc(string secret_id, int start, int length)
        {
            string where = " AND secret_id = @secret_id AND is_delete = @is_delete";
            string limit = " limit @length offset @start ";
            return await SqlDapperHelper.ExecuteReaderRetListAsync<user_face_storage_group>(SelectSql + where + limit, new { secret_id, length, start, is_delete = false });
        }


        /// <summary>
        /// 修改是否删除状态
        /// </summary>
        /// <param name="group"></param>
        /// <param name="tra"></param>
        /// <returns></returns>
        public static async Task<long> Remove_storage_group(user_face_storage_group group, IDbTransaction tra = null)
        {
            string set = "is_delete = @is_delete , updated_at = @updated_at";
            string where = "AND user_id = @user_id AND group_name = @group_name AND secret_id = @secret_id";
            return await SqlDapperHelper.ExecuteSqlIntAsync(string.Format(UpdateSql + where, set), group, tra);

        }


    }
}
