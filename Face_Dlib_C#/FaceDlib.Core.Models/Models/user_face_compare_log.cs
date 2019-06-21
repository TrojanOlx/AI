/******************************************
* 模块名称：实体 user_face_compare_log
* 当前版本：1.0
* 开发人员：Trojan
* 生成时间：2019/1/23
* 版本历史：此代码由 VB/C#.Net实体代码生成工具(EntitysCodeGenerate 4.6) 自动生成。
* 
******************************************/
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace FaceDlib.Core.Models.Models
{
	/// <summary>
	/// 实体 user_face_compare_log
	/// </summary>
	[Description("Primary:id")]
    [Serializable]
	public partial class user_face_compare_log
	{
        #region 构造函数
        /// <summary>
        /// 实体 user_face_compare_log
        /// </summary>
        public user_face_compare_log(){}
        #endregion

        #region 私有变量
        private long _id = long.MinValue;
        private long _user_id = long.MinValue;
        private string _secret_id = null;
        private string _image = null;
        private DateTime _timestamp = new DateTime();
        private string _sign = null;
        private Boolean _is_deductions_success = false;
        private string _api_respone = null;
        private DateTime _created_at = new DateTime();
        private DateTime _updated_at = new DateTime();
        #endregion

        #region 公共属性
        /// <summary>
        /// 主键 id(NOT NULL)
        /// </summary>
        public long id
        {
            set{ _id=value;}
            get{return _id;}
        }
        /// <summary>
        /// user_id(NOT NULL)
        /// </summary>
        public long user_id
        {
            set{ _user_id=value;}
            get{return _user_id;}
        }
        /// <summary>
        /// 应用的ID(NOT NULL)
        /// </summary>
        public string secret_id
        {
            set{ _secret_id=value;}
            get{return _secret_id;}
        }
        /// <summary>
        /// 图片路径或者BASE64位的图片
        /// </summary>
        public string image
        {
            set{ _image=value;}
            get{return _image;}
        }
        /// <summary>
        /// 当前的时间搓(NOT NULL)
        /// </summary>
        public DateTime timestamp
        {
            set{ _timestamp=value;}
            get{return _timestamp;}
        }
        /// <summary>
        /// 签名(NOT NULL)
        /// </summary>
        public string sign
        {
            set{ _sign=value;}
            get{return _sign;}
        }
        /// <summary>
        /// 是否还没有扣费
        /// </summary>
        public Boolean is_deductions_success
        {
            set{ _is_deductions_success=value;}
            get{return _is_deductions_success;}
        }
        /// <summary>
        /// API返回的信息
        /// </summary>
        public string api_respone
        {
            set{ _api_respone=value;}
            get{return _api_respone;}
        }
        /// <summary>
        /// created_at
        /// </summary>
        public DateTime created_at
        {
            set{ _created_at=value;}
            get{return _created_at;}
        }
        /// <summary>
        /// updated_at
        /// </summary>
        public DateTime updated_at
        {
            set{ _updated_at=value;}
            get{return _updated_at;}
        }
        #endregion

        #region 公共静态只读属性
        /// <summary>
        /// 表名 表原信息描述: user_face_compare_log
        /// </summary>
        public static readonly string s_TableName =  "user_face_compare_log";
        /// <summary>
        /// 信息描述: id(NOT NULL)
        /// </summary>
        public static readonly string s_id =  "user_face_compare_log┋id┋System.Int64";
        /// <summary>
        /// 信息描述: user_id(NOT NULL)
        /// </summary>
        public static readonly string s_user_id =  "user_face_compare_log┋user_id┋System.Int64";
        /// <summary>
        /// 信息描述: 应用的ID(NOT NULL)
        /// </summary>
        public static readonly string s_secret_id =  "user_face_compare_log┋secret_id┋System.String";
        /// <summary>
        /// 信息描述: 图片路径或者BASE64位的图片
        /// </summary>
        public static readonly string s_image =  "user_face_compare_log┋image┋System.String";
        /// <summary>
        /// 信息描述: 当前的时间搓(NOT NULL)
        /// </summary>
        public static readonly string s_timestamp =  "user_face_compare_log┋timestamp┋System.String";
        /// <summary>
        /// 信息描述: 签名(NOT NULL)
        /// </summary>
        public static readonly string s_sign =  "user_face_compare_log┋sign┋System.String";
        /// <summary>
        /// 信息描述: 是否还没有扣费
        /// </summary>
        public static readonly string s_is_deductions_success =  "user_face_compare_log┋is_deductions_success┋System.Boolean";
        /// <summary>
        /// 信息描述: API返回的信息
        /// </summary>
        public static readonly string s_api_respone =  "user_face_compare_log┋api_respone┋System.String";
        /// <summary>
        /// 信息描述: created_at
        /// </summary>
        public static readonly string s_created_at =  "user_face_compare_log┋created_at┋System.String";
        /// <summary>
        /// 信息描述: updated_at
        /// </summary>
        public static readonly string s_updated_at =  "user_face_compare_log┋updated_at┋System.String";
        #endregion
	}

    /// <summary>
    /// user_face_compare_log实体集
    /// </summary>
    [Serializable]
    public class user_face_compare_logS : CollectionBase
    {
        #region 构造函数
        /// <summary>
        /// user_face_compare_log实体集
        /// </summary>
        public user_face_compare_logS(){}
        #endregion

        #region 属性方法
        /// <summary>
        /// user_face_compare_log集合 增加方法
        /// </summary>
        public void Add(user_face_compare_log entity)
        {
            this.List.Add(entity);
        }
        /// <summary>
        /// user_face_compare_log集合 索引
        /// </summary>
        public user_face_compare_log this[int index]
        {
            get { return (user_face_compare_log)this.List[index]; }
            set { this.List[index] = value; }
        }
        #endregion
    }
}
