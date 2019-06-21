/******************************************
* 模块名称：实体 user
* 当前版本：1.0
* 开发人员：Trojan
* 生成时间：2019/1/7
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
	/// 实体 user
	/// </summary>
	[Description("Primary:id")]
    [Serializable]
	public partial class user
	{
        #region 构造函数
        /// <summary>
        /// 实体 user
        /// </summary>
        public user(){}
        #endregion

        #region 私有变量
        private long _id = long.MinValue;
        private string _mobile = null;
        private string _user_password = null;
        private decimal _use_amount = 0;
        private string _user_name = null;
        private Boolean _is_bind_email = false;
        private string _email = null;
        private Boolean _is_lock = false;
        private Int32 _login_count = Int32.MinValue;
        private string _google_secret = null;
        private DateTime _created_at = new DateTime();
        private DateTime _updated_at = new DateTime();
        private string _inter_pre = null;
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
        /// mobile(NOT NULL)
        /// </summary>
        public string mobile
        {
            set{ _mobile=value;}
            get{return _mobile;}
        }
        /// <summary>
        /// user_password(NOT NULL)
        /// </summary>
        public string user_password
        {
            set{ _user_password=value;}
            get{return _user_password;}
        }
        /// <summary>
        /// use_amount(NOT NULL)
        /// </summary>
        public decimal use_amount
        {
            set{ _use_amount=value;}
            get{return _use_amount;}
        }
        /// <summary>
        /// user_name
        /// </summary>
        public string user_name
        {
            set{ _user_name=value;}
            get{return _user_name;}
        }
        /// <summary>
        /// is_bind_email(NOT NULL)
        /// </summary>
        public Boolean is_bind_email
        {
            set{ _is_bind_email=value;}
            get{return _is_bind_email;}
        }
        /// <summary>
        /// email
        /// </summary>
        public string email
        {
            set{ _email=value;}
            get{return _email;}
        }
        /// <summary>
        /// is_lock(NOT NULL)
        /// </summary>
        public Boolean is_lock
        {
            set{ _is_lock=value;}
            get{return _is_lock;}
        }
        /// <summary>
        /// login_count(NOT NULL)
        /// </summary>
        public Int32 login_count
        {
            set{ _login_count=value;}
            get{return _login_count;}
        }
        /// <summary>
        /// google_secret
        /// </summary>
        public string google_secret
        {
            set{ _google_secret=value;}
            get{return _google_secret;}
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
        /// <summary>
        /// inter_pre
        /// </summary>
        public string inter_pre
        {
            set{ _inter_pre=value;}
            get{return _inter_pre;}
        }
        #endregion

        #region 公共静态只读属性
        /// <summary>
        /// 表名 表原信息描述: user
        /// </summary>
        public static readonly string s_TableName =  "user";
        /// <summary>
        /// 信息描述: id(NOT NULL)
        /// </summary>
        public static readonly string s_id =  "user┋id┋System.Int64";
        /// <summary>
        /// 信息描述: mobile(NOT NULL)
        /// </summary>
        public static readonly string s_mobile =  "user┋mobile┋System.String";
        /// <summary>
        /// 信息描述: user_password(NOT NULL)
        /// </summary>
        public static readonly string s_user_password =  "user┋user_password┋System.String";
        /// <summary>
        /// 信息描述: use_amount(NOT NULL)
        /// </summary>
        public static readonly string s_use_amount = "user┋use_amount┋decimal";
        /// <summary>
        /// 信息描述: user_name
        /// </summary>
        public static readonly string s_user_name =  "user┋user_name┋System.String";
        /// <summary>
        /// 信息描述: is_bind_email(NOT NULL)
        /// </summary>
        public static readonly string s_is_bind_email =  "user┋is_bind_email┋System.Boolean";
        /// <summary>
        /// 信息描述: email
        /// </summary>
        public static readonly string s_email =  "user┋email┋System.String";
        /// <summary>
        /// 信息描述: is_lock(NOT NULL)
        /// </summary>
        public static readonly string s_is_lock =  "user┋is_lock┋System.Boolean";
        /// <summary>
        /// 信息描述: login_count(NOT NULL)
        /// </summary>
        public static readonly string s_login_count =  "user┋login_count┋System.Int32";
        /// <summary>
        /// 信息描述: google_secret
        /// </summary>
        public static readonly string s_google_secret =  "user┋google_secret┋System.String";
        /// <summary>
        /// 信息描述: created_at
        /// </summary>
        public static readonly string s_created_at =  "user┋created_at┋System.String";
        /// <summary>
        /// 信息描述: updated_at
        /// </summary>
        public static readonly string s_updated_at =  "user┋updated_at┋System.String";
        /// <summary>
        /// 信息描述: inter_pre
        /// </summary>
        public static readonly string s_inter_pre =  "user┋inter_pre┋System.String";
        #endregion
	}

    /// <summary>
    /// user实体集
    /// </summary>
    [Serializable]
    public class userS : CollectionBase
    {
        #region 构造函数
        /// <summary>
        /// user实体集
        /// </summary>
        public userS(){}
        #endregion

        #region 属性方法
        /// <summary>
        /// user集合 增加方法
        /// </summary>
        public void Add(user entity)
        {
            this.List.Add(entity);
        }
        /// <summary>
        /// user集合 索引
        /// </summary>
        public user this[int index]
        {
            get { return (user)this.List[index]; }
            set { this.List[index] = value; }
        }
        #endregion
    }
}
