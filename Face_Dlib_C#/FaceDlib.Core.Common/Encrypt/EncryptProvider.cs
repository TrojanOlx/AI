using FaceDlib.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FaceDlib.Core.Common.Encrypt
{
    public class EncryptProvider
    {
        #region Common

        /// <summary>
        /// 生成随机密钥
        /// </summary>
        /// <param name="n">长度，IV is 16，Key is 32</param>
        /// <returns>返回随机值</returns>
        private static string GetRandomStr(int length)
        {
            char[] arrChar = new char[]{
           'a','b','d','c','e','f','g','h','i','j','k','l','m','n','p','r','q','s','t','u','v','w','z','y','x',
           '0','1','2','3','4','5','6','7','8','9',
           'A','B','C','D','E','F','G','H','I','J','K','L','M','N','Q','P','R','T','S','V','U','W','X','Y','Z'
          };

            StringBuilder num = new StringBuilder();

            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < length; i++)
            {
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());
            }

            return num.ToString();
        }

        #endregion



        #region MD5
        
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="srcString">加密的字符串</param>
        /// <param name="length">长度，默认32位</param>
        /// <returns></returns>
        public static string Md5(string srcString, MD5Length length = MD5Length.L32)
        {
            string str_md5_out = string.Empty;
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes_md5_in = Encoding.UTF8.GetBytes(srcString);
                byte[] bytes_md5_out = md5.ComputeHash(bytes_md5_in);

                str_md5_out = length == MD5Length.L32
                    ? BitConverter.ToString(bytes_md5_out)
                    : BitConverter.ToString(bytes_md5_out, 4, 8);

                str_md5_out = str_md5_out.Replace("-", "");
                return str_md5_out;
            }
        }
        #endregion
    }
}
