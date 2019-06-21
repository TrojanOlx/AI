using FaceDlib.Core.Common.Encrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FaceDlib.Core.Common
{
    public class Signature
    {
        /// <summary>
        /// 获取Sign
        /// </summary>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static string GetSign(Dictionary<string,string> pairs,string key) {
            pairs = pairs.OrderBy(x => x.Key).ToDictionary(k=>k.Key,v=>v.Value);
            StringBuilder sb = new StringBuilder();
            foreach (var item in pairs)
            {
                sb.Append(item.Value+"&");
            }
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            var messageBytes = Encoding.UTF8.GetBytes(sb.ToString().TrimEnd('&'));
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
            
        }
    }
}
