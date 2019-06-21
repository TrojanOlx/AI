/******************************************
* 模块名称：实体 user_info
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
	/// 实体 user_info
	/// </summary>
	[Description("Primary:id")]
    [Serializable]
	public partial class user_info
	{
        #region 构造函数
        /// <summary>
        /// 实体 user_info
        /// </summary>
        public user_info(){}
        #endregion

        #region 私有变量
        private long _id = long.MinValue;
        private long _user_id = long.MinValue;
        private string _company_name = null;
        private string _contact_name = null;
        private string _contact_mobile = null;
        private string _index = null;
        private string _created_at = null;
        private string _updated_at = null;
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
        /// 公司名称(NOT NULL)
        /// </summary>
        public string company_name
        {
            set{ _company_name=value;}
            get{return _company_name;}
        }
        /// <summary>
        /// 联系人(NOT NULL)
        /// </summary>
        public string contact_name
        {
            set{ _contact_name=value;}
            get{return _contact_name;}
        }
        /// <summary>
        /// 密保手机(NOT NULL)
        /// </summary>
        public string contact_mobile
        {
            set{ _contact_mobile=value;}
            get{return _contact_mobile;}
        }
        /// <summary>
        /// 存储用户id，唯一键(NOT NULL)
        /// </summary>
        public string index
        {
            set{ _index=value;}
            get{return _index;}
        }
        /// <summary>
        /// created_at
        /// </summary>
        public string created_at
        {
            set{ _created_at=value;}
            get{return _created_at;}
        }
        /// <summary>
        /// updated_at
        /// </summary>
        public string updated_at
        {
            set{ _updated_at=value;}
            get{return _updated_at;}
        }
        #endregion

        #region 公共静态只读属性
        /// <summary>
        /// 表名 表原信息描述: user_info
        /// </summary>
        public static readonly string s_TableName =  "user_info";
        /// <summary>
        /// 信息描述: id(NOT NULL)
        /// </summary>
        public static readonly string s_id =  "user_info┋id┋System.Int64";
        /// <summary>
        /// 信息描述: user_id(NOT NULL)
        /// </summary>
        public static readonly string s_user_id =  "user_info┋user_id┋System.Int64";
        /// <summary>
        /// 信息描述: 公司名称(NOT NULL)
        /// </summary>
        public static readonly string s_company_name =  "user_info┋company_name┋System.String";
        /// <summary>
        /// 信息描述: 联系人(NOT NULL)
        /// </summary>
        public static readonly string s_contact_name =  "user_info┋contact_name┋System.String";
        /// <summary>
        /// 信息描述: 密保手机(NOT NULL)
        /// </summary>
        public static readonly string s_contact_mobile =  "user_info┋contact_mobile┋System.String";
        /// <summary>
        /// 信息描述: 存储用户id，唯一键(NOT NULL)
        /// </summary>
        public static readonly string s_index =  "user_info┋index┋System.String";
        /// <summary>
        /// 信息描述: created_at
        /// </summary>
        public static readonly string s_created_at =  "user_info┋created_at┋System.String";
        /// <summary>
        /// 信息描述: updated_at
        /// </summary>
        public static readonly string s_updated_at =  "user_info┋updated_at┋System.String";
        #endregion
	}

    /// <summary>
    /// user_info实体集
    /// </summary>
    [Serializable]
    public class user_infoS : CollectionBase
    {
        #region 构造函数
        /// <summary>
        /// user_info实体集
        /// </summary>
        public user_infoS(){}
        #endregion

        #region 属性方法
        /// <summary>
        /// user_info集合 增加方法
        /// </summary>
        public void Add(user_info entity)
        {
            this.List.Add(entity);
        }
        /// <summary>
        /// user_info集合 索引
        /// </summary>
        public user_info this[int index]
        {
            get { return (user_info)this.List[index]; }
            set { this.List[index] = value; }
        }
        #endregion
    }
}
