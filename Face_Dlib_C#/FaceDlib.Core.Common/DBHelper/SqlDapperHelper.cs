using Dapper;
using Dapper.Contrib.Extensions;
using FaceDlib.Core.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceDlib.Core.Common.DBHelper
{
    public class SqlDapperHelper
    {
        //static string connStrRead = ConfigurationView.connStrRead;// ConfigurationManager.ConnectionStrings["HJDataModelDapperRead"].ConnectionString;
        //static string connStrWrite = ConfigurationView.connStrWrite;// ConfigurationManager.ConnectionStrings["HJDataModelDapperWrite"].ConnectionString;
        //NpgsqlConnection("Host=localhost;Username=<user name>;Password=<password>;Database=TimeManagement")
        static int commandTimeout = 100;
        public static IDbConnection GetConnection(bool useWriteConn)
        {
            if (useWriteConn)
                return new NpgsqlConnection(CoinAppSettings.Instance.ConnectionStrings.DapperWrite);
            return new NpgsqlConnection(CoinAppSettings.Instance.ConnectionStrings.DapperRead);
        }
        public static IDbConnection GetOpenConnection()
        {
            var conn = new NpgsqlConnection(CoinAppSettings.Instance.ConnectionStrings.DapperWrite);
            conn.Open();
            return conn;
        }
        /// <summary>
        ///  执行sql返回一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="useWriteConn"></param>
        /// <returns></returns>
        public static T ExecuteReaderReturnT<T>(string sql, object param = null, bool useWriteConn = false, IDbTransaction transaction = null)
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(useWriteConn))
                {
                    conn.Open();
                    return conn.QueryFirstOrDefault<T>(sql, param, commandTimeout: commandTimeout);
                }
            }
            else
            {
                var conn = transaction.Connection;
                return conn.QueryFirstOrDefault<T>(sql, param, commandTimeout: commandTimeout);
            }

        }
        /// <summary>
        /// 执行sql返回多个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="useWriteConn"></param>
        /// <returns></returns>
        public static List<T> ExecuteReaderReturnList<T>(string sql, object param = null, bool useWriteConn = false)
        {
            using (IDbConnection conn = GetConnection(useWriteConn))
            {
                conn.Open();

                return conn.Query<T>(sql, param, commandTimeout: commandTimeout).ToList();
            }
        }
        /// <summary>
        /// 执行sql返回一个对象--异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="useWriteConn"></param>
        /// <returns></returns>
        public static async Task<T> ExecuteReaderRetTAsync<T>(string sql, object param = null, bool useWriteConn = false)
        {
            using (IDbConnection conn = GetConnection(useWriteConn))
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<T>(sql, param, commandTimeout: commandTimeout).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 执行sql返回多个对象--异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="useWriteConn"></param>
        /// <returns></returns>
        public static async Task<List<T>> ExecuteReaderRetListAsync<T>(string sql, object param = null, bool useWriteConn = false)
        {
            using (IDbConnection conn = GetConnection(useWriteConn))
            {
                conn.Open();
                var list = await conn.QueryAsync<T>(sql, param, commandTimeout: commandTimeout).ConfigureAwait(false);
                return list.ToList();
            }
        }
        /// <summary>
        /// 执行sql，返回影响行数 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static int ExecuteSqlInt(string sql, object param = null, IDbTransaction transaction = null)
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    conn.Open();
                    return conn.Execute(sql, param, commandTimeout: commandTimeout, commandType: CommandType.Text);
                }
            }
            else
            {
                var conn = transaction.Connection;
                return conn.Execute(sql, param, transaction: transaction, commandTimeout: commandTimeout, commandType: CommandType.Text);
            }
        }
        /// <summary>
        /// 执行sql，返回影响行数--异步
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteSqlIntAsync(string sql, object param = null, IDbTransaction transaction = null)
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    conn.Open();
                    return await conn.ExecuteAsync(sql, param, commandTimeout: commandTimeout, commandType: CommandType.Text).ConfigureAwait(false);
                }
            }
            else
            {
                var conn = transaction.Connection;
                return await conn.ExecuteAsync(sql, param, transaction: transaction, commandTimeout: commandTimeout, commandType: CommandType.Text).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="useWriteConn"></param>
        /// <returns></returns>
        public static T GetById<T>(int id, IDbTransaction transaction = null, bool useWriteConn = false) where T : class
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(useWriteConn))
                {
                    conn.Open();
                    return conn.Get<T>(id, commandTimeout: commandTimeout);
                }
            }
            else
            {
                var conn = transaction.Connection;
                return conn.Get<T>(id, transaction: transaction, commandTimeout: commandTimeout);
            }
        }
        /// <summary>
        /// 根据id获取实体--异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="useWriteConn"></param>
        /// <returns></returns>
        public static async Task<T> GetByIdAsync<T>(int id, IDbTransaction transaction = null, bool useWriteConn = false) where T : class
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(useWriteConn))
                {
                    conn.Open();
                    return await conn.GetAsync<T>(id, commandTimeout: commandTimeout);
                }
            }
            else
            {
                var conn = transaction.Connection;
                return await conn.GetAsync<T>(id, transaction: transaction, commandTimeout: commandTimeout);
            }
        }
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static long ExecuteInsert<T>(T item, IDbTransaction transaction = null) where T : class
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    conn.Open();
                    return conn.Insert<T>(item, commandTimeout: commandTimeout);
                }
            }
            else
            {
                var conn = transaction.Connection;
                return conn.Insert(item, transaction: transaction, commandTimeout: commandTimeout);
            }
        }
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static async Task<long> ExecuteInsertAsync<T>(T item, IDbTransaction transaction = null) where T : class
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    conn.Open();
                    return await conn.InsertAsync(item, commandTimeout: commandTimeout).ConfigureAwait(false);
                }
            }
            else
            {
                var conn = transaction.Connection;
                return await conn.InsertAsync(item, transaction: transaction, commandTimeout: commandTimeout).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="transaction"></param>
        public static void ExecuteInsertList<T>(IEnumerable<T> list, IDbTransaction transaction = null) where T : class
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    conn.Open();
                    conn.Insert(list, commandTimeout: commandTimeout);
                }
            }
            else
            {
                var conn = transaction.Connection;
                conn.Insert(list, transaction: transaction, commandTimeout: commandTimeout);
            }
        }

        public static async Task ExecuteInsertListAsync<T>(IEnumerable<T> list, IDbTransaction transaction = null) where T : class
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    conn.Open();
                    await conn.InsertAsync(list, commandTimeout: commandTimeout).ConfigureAwait(false);
                }
            }
            else
            {
                var conn = transaction.Connection;
                await conn.InsertAsync(list, transaction: transaction, commandTimeout: commandTimeout).ConfigureAwait(false);
            }
        }

    }
}
