using FaceDlib.Core.Common.DBHelper;
using FaceDlib.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FaceDlib.Core.Service
{
    public class Service_user_face_compare_price
    {
        static string Select = @"SELECT
                                    public.user_face_compare_price.id,
                                    public.user_face_compare_price.user_id,
                                    public.user_face_compare_price.price,
                                    public.user_face_compare_price.created_at,
                                    public.user_face_compare_price.updated_at
                                FROM
                                    public.user_face_compare_price
                                WHERE
                                    1 = 1 ";


        /// <summary>
        /// 根据用户ID获取用户每张图片设置的价格
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static async Task<user_face_compare_price> GetUser_Face_Compare_Price_By_User_idAsync(long user_id)
        {
            string where = " AND public.user_face_compare_price.user_id = @user_id";
            return await SqlDapperHelper.ExecuteReaderRetTAsync<user_face_compare_price>(Select + where, new { user_id });
        }
    }
}
