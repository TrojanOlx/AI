using FaceDlib.Core.Common.DBHelper;
using FaceDlib.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FaceDlib.Core.Service
{
    public class Service_user_face_detect_log
    {
        static string Select = @"SELECT
                                    public.user_face_detect_log.id,
                                    public.user_face_detect_log.user_id,
                                    public.user_face_detect_log.secret_id,
                                    public.user_face_detect_log.face_token,
                                    public.user_face_detect_log.image,
                                    public.user_face_detect_log.image_type,
                                    public.user_face_detect_log.face_field,
                                    public.user_face_detect_log.max_face_num,
                                    public.user_face_detect_log.face_type,
                                    public.user_face_detect_log.timestamp,
                                    public.user_face_detect_log.sign,
                                    public.user_face_detect_log.is_deductions_success,
                                    public.user_face_detect_log.api_respone,
                                    public.user_face_detect_log.created_at,
                                    public.user_face_detect_log.updated_at,
                                    public.user_face_detect_log.is_search
                                FROM
                                    public.user_face_detect_log
                                WHERE
                                    1 = 1 ";

        /// <summary>
        /// 根据Face_Token 查询图片是否存在
        /// </summary>
        /// <param name="face_token"></param>
        /// <returns></returns>
        public static async Task<user_face_detect_log> GetUser_Face_Detect_Log_By_Face_token(string face_token)
        {
            string where = " AND public.user_face_detect_log.face_token = @face_token";
            //string orderBy = "";
            return await SqlDapperHelper.ExecuteReaderRetTAsync<user_face_detect_log>(Select + where, new { face_token });
        }
    }
}
