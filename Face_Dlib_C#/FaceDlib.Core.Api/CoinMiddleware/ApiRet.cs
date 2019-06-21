using System;
using System.Collections.Generic;
using System.Text;

namespace FaceDlib.Core.Api
{
    public class ApiRet<T>
    {
        /// <summary>
        /// 返回状态码  -9999,2000
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 是否错误
        /// </summary>
        public bool IsError { set; get; }
        /// <summary>
        /// 构造器
        /// </summary>
        public ApiRet(int code, string msg, T Data)
        {
            this.Code = code;
            this.Msg = msg;
            if (Data == null)
            {
                this.Data = default(T);
            }
            else
            {
                this.Data = Data;
            }
        }


    }
}
