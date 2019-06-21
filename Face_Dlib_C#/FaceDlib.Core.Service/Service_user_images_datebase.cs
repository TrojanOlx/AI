using FaceDlib.Core.Common.DBHelper;
using FaceDlib.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FaceDlib.Core.Service
{
    public class Service_user_face_image_data
    {
        static string Select = @"SELECT
                                    public.user_face_image_data.id,
                                    public.user_face_image_data.token,
                                    public.user_face_image_data.url,
                                    public.user_face_image_data.created_at,
                                    public.user_face_image_data.updated_at
                                FROM
                                    public.user_face_image_data
                                WHERE 1 = 1 ";

        /// <summary>
        /// 根据Token 查询
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<user_face_image_data> Getuser_face_image_data_By_Token_Async(string token)
        {
            string where = " AND token = @token";
            return await SqlDapperHelper.ExecuteReaderRetTAsync<user_face_image_data>(Select + where, new { token });
        }


        public static async Task<long> Creatuser_face_image_data(user_face_image_data user_Images, IDbTransaction transaction = null)
        {
            return await SqlDapperHelper.ExecuteInsertAsync(user_Images, transaction);
        }



    }
}
