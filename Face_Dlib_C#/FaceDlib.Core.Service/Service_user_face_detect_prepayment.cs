using FaceDlib.Core.Common.DBHelper;
using FaceDlib.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FaceDlib.Core.Service
{
    public class Service_user_face_detect_prepayment
    {

        static string Select = @"SELECT
                                    public.user_face_detect_prepayment.id,
                                    public.user_face_detect_prepayment.user_id,
                                    public.user_face_detect_prepayment.number,
                                    public.user_face_detect_prepayment.created_at,
                                    public.user_face_detect_prepayment.updated_at
                                FROM
                                    public.user_face_detect_prepayment
                                WHERE
                                    1 = 1 ";

        static string Update = @"UPDATE public.user_face_detect_prepayment 
                                 SET 
                                    {0} 
                                 WHERE 
                                    1=1 ";

        /// <summary>
        /// 根据用户ID，查询用户的预存
        /// </summary>
        /// <param name="user_id">用户ID</param>
        /// <returns></returns>
        public static async Task<user_face_detect_prepayment> GetUser_Face_Detect_Prepayment_By_User_id_Asynv(long user_id)
        {
            string where = " AND public.user_face_detect_prepayment.user_id = @user_id";
            return await SqlDapperHelper.ExecuteReaderRetTAsync<user_face_detect_prepayment>(Select+where,new { user_id});
        }
        /// <summary>
        /// 根据用户ID，查询用户的预存
        /// </summary>
        /// <param name="user_id">用户ID</param>
        /// <returns></returns>
        public static  user_face_detect_prepayment GetUser_Face_Detect_Prepayment_By_User_id(long user_id)
        {
            string where = " AND public.user_face_detect_prepayment.user_id = @user_id";
            return  SqlDapperHelper.ExecuteReaderReturnT<user_face_detect_prepayment>(Select + where, new { user_id });
        }






        /// <summary>
        /// 修改预存数量
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="tra"></param>
        /// <returns></returns>
        public static async Task<long> MinusUsernumber_By_User_id(user_face_detect_prepayment_detail detail, IDbTransaction tra = null)
        {
            string set = " number = number+(@change_number),updated_at=@updated_at";
            string where = " AND user_id = @user_id";
            return await SqlDapperHelper.ExecuteSqlIntAsync(string.Format(Update + where, set), new { detail.change_number,detail.user_id,detail.updated_at }, tra);
        }
    }
}
