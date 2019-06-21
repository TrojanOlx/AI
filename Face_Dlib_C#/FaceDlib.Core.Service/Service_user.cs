using FaceDlib.Core.Common.DBHelper;
using FaceDlib.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FaceDlib.Core.Service
{
    public class Service_user
    {
        static string Select = @"SELECT
                                    public.user.id,
                                    public.user.mobile,
                                    public.user.use_amount,
                                    public.user.user_name,
                                    public.user.email,
                                    public.user.inter_pre
                                FROM
                                    public.user
                                WHERE
                                    1 = 1 ";


        static string Update = @"UPDATE PUBLIC.USER 
                                    SET {0} 
                                    WHERE
	                                    1 = 1 ";
        /// <summary>
        /// 根据User_id 查询 User
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static async Task<user> GetUser_By_User_id_Async(long user_id) {
            string where = " AND id = @user_id";
            return await SqlDapperHelper.ExecuteReaderRetTAsync<user>(Select+where,new { user_id });
        }

        public static user GetUser_By_User_id(long user_id)
        {
            string where = " AND id = @user_id";
            return SqlDapperHelper.ExecuteReaderReturnT<user>(Select + where, new { user_id });
        }



        /// <summary>
        /// 修改用户金额
        /// </summary>
        /// <param name="user_Amount">日志实体类</param>
        /// <param name="tra">事务</param>
        /// <returns></returns>
        public static async Task<long> MinusUserAmount_By_User_id(user_amount_detail user_Amount, IDbTransaction tra=null) {
            string set = "use_amount = use_amount+(@change_amount) , updated_at = @updated_at";
            string where = " AND id = @user_id";
            return await SqlDapperHelper.ExecuteSqlIntAsync(string.Format(Update + where,set),new {user_Amount.user_id,user_Amount.updated_at, user_Amount.change_amount},tra);
        }



    }
}
