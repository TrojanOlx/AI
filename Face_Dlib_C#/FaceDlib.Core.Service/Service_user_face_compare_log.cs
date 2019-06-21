using FaceDlib.Core.Common.DBHelper;
using FaceDlib.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FaceDlib.Core.Service
{
    public class Service_user_face_compare_log
    {
        static string Select = @"SELECT
                                    public.user_face_compare_log.id,
                                    public.user_face_compare_log.user_id,
                                    public.user_face_compare_log.secret_id,
                                    public.user_face_compare_log.image,
                                    public.user_face_compare_log.timestamp,
                                    public.user_face_compare_log.sign,
                                    public.user_face_compare_log.is_deductions_success,
                                    public.user_face_compare_log.api_respone,
                                    public.user_face_compare_log.created_at,
                                    public.user_face_compare_log.updated_at
                                FROM
                                    public.user_face_compare_log
                                WHERE
                                1 = 1 ";



        /// <summary>
        /// 根据Face_Token 查询图片是否存在
        /// </summary>
        /// <param name="face_token"></param>
        /// <returns></returns>
        public static async Task<user_face_compare_log> GetUser_Face_Compare_Log_By_Face_token(string face_token)
        {
            string where = " AND public.user_face_compare_log.face_token = @face_token";
            //string orderBy = "";
            return await SqlDapperHelper.ExecuteReaderRetTAsync<user_face_compare_log>(Select + where, new { face_token });
        }

    }
}
