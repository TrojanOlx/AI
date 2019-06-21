using FaceDlib.Core.Common.DBHelper;
using FaceDlib.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FaceDlib.Core.Service
{
    public class Service_user_face_set
    {

        private static string SelectSet = @"SELECT
                                            public.user_face_set.id,
	                                        public.user_face_set.user_id,
	                                        public.user_face_set.secret_id,
	                                        public.user_face_set.secret_key,
	                                        public.user_face_set.created_at,
	                                        public.user_face_set.updated_at
                                        FROM
                                            public.user_face_set
                                        WHERE
                                            1 = 1 ";


        /// <summary>
        /// 根据Key获取配置信息
        /// </summary>
        /// <param name="secret_key">密钥</param>
        /// <returns></returns>
        public static async Task<user_face_set> GetUser_Face_Set_By_Secret_key_Async(string secret_id)
        {
            string where = " AND public.user_face_set.secret_id = @secret_id";
            return await SqlDapperHelper.ExecuteReaderRetTAsync<user_face_set>(SelectSet + where, new { secret_id });
        }
        /// <summary>
        /// 根据Key获取配置信息
        /// </summary>
        /// <param name="secret_key">密钥</param>
        /// <returns></returns>
        public static  user_face_set GetUser_Face_Set_By_Secret_key(string secret_id)
        {
            string where = " AND public.user_face_set.secret_id = @secret_id";
            return  SqlDapperHelper.ExecuteReaderReturnT<user_face_set>(SelectSet + where, new { secret_id });
        }



    }
}
