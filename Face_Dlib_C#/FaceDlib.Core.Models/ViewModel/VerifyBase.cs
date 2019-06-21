using System;
using System.Collections.Generic;
using System.Text;

namespace FaceDlib.Core.Models.ViewModel
{
    public class VerifyBase
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public string secret_id { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }


    }
}
