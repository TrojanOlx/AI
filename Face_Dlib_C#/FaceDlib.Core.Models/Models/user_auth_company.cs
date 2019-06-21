/******************************************
* 模块名称：实体 user_auth_company
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
	/// 实体 user_auth_company
	/// </summary>
	[Description("Primary:id")]
    [Serializable]
	public partial class user_auth_company
	{
        #region 构造函数
        /// <summary>
        /// 实体 user_auth_company
        /// </summary>
        public user_auth_company(){}
        #endregion

        #region 私有变量
        private long _id = long.MinValue;
        private long _user_id = long.MinValue;
        private string _company_name = null;
        private string _industry = null;
        private string _id_card = null;
        private string _card_img = null;
        private string _qualifications_card_img = null;
        private short _status = short.MaxValue;
        private string _refuse_info = null;
        private string _check_time = null;
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
        /// 行业(NOT NULL)
        /// </summary>
        public string industry
        {
            set{ _industry=value;}
            get{return _industry;}
        }
        /// <summary>
        /// 证件号码(NOT NULL)
        /// </summary>
        public string id_card
        {
            set{ _id_card=value;}
            get{return _id_card;}
        }
        /// <summary>
        /// 证件图片(NOT NULL)
        /// </summary>
        public string card_img
        {
            set{ _card_img=value;}
            get{return _card_img;}
        }
        /// <summary>
        /// 资质认证的图片(NOT NULL)
        /// </summary>
        public string qualifications_card_img
        {
            set{ _qualifications_card_img=value;}
            get{return _qualifications_card_img;}
        }
        /// <summary>
        /// 签名状态，0未审核，1审核通过，-1审核拒绝
        /// </summary>
        public short status
        {
            set{ _status=value;}
            get{return _status;}
        }
        /// <summary>
        /// 审核拒绝的原因
        /// </summary>
        public string refuse_info
        {
            set{ _refuse_info=value;}
            get{return _refuse_info;}
        }
        /// <summary>
        /// 签名通过的时间
        /// </summary>
        public string check_time
        {
            set{ _check_time=value;}
            get{return _check_time;}
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
        /// 表名 表原信息描述: user_auth_company
        /// </summary>
        public static readonly string s_TableName =  "user_auth_company";
        /// <summary>
        /// 信息描述: id(NOT NULL)
        /// </summary>
        public static readonly string s_id =  "user_auth_company┋id┋System.Int64";
        /// <summary>
        /// 信息描述: user_id(NOT NULL)
        /// </summary>
        public static readonly string s_user_id =  "user_auth_company┋user_id┋System.Int64";
        /// <summary>
        /// 信息描述: 公司名称(NOT NULL)
        /// </summary>
        public static readonly string s_company_name =  "user_auth_company┋company_name┋System.String";
        /// <summary>
        /// 信息描述: 行业(NOT NULL)
        /// </summary>
        public static readonly string s_industry =  "user_auth_company┋industry┋System.String";
        /// <summary>
        /// 信息描述: 证件号码(NOT NULL)
        /// </summary>
        public static readonly string s_id_card =  "user_auth_company┋id_card┋System.String";
        /// <summary>
        /// 信息描述: 证件图片(NOT NULL)
        /// </summary>
        public static readonly string s_card_img =  "user_auth_company┋card_img┋System.String";
        /// <summary>
        /// 信息描述: 资质认证的图片(NOT NULL)
        /// </summary>
        public static readonly string s_qualifications_card_img =  "user_auth_company┋qualifications_card_img┋System.String";
        /// <summary>
        /// 信息描述: 签名状态，0未审核，1审核通过，-1审核拒绝
        /// </summary>
        public static readonly string s_status =  "user_auth_company┋status┋System.Int16";
        /// <summary>
        /// 信息描述: 审核拒绝的原因
        /// </summary>
        public static readonly string s_refuse_info =  "user_auth_company┋refuse_info┋System.String";
        /// <summary>
        /// 信息描述: 签名通过的时间
        /// </summary>
        public static readonly string s_check_time =  "user_auth_company┋check_time┋System.String";
        /// <summary>
        /// 信息描述: created_at
        /// </summary>
        public static readonly string s_created_at =  "user_auth_company┋created_at┋System.String";
        /// <summary>
        /// 信息描述: updated_at
        /// </summary>
        public static readonly string s_updated_at =  "user_auth_company┋updated_at┋System.String";
        #endregion
	}

    /// <summary>
    /// user_auth_company实体集
    /// </summary>
    [Serializable]
    public class user_auth_companyS : CollectionBase
    {
        #region 构造函数
        /// <summary>
        /// user_auth_company实体集
        /// </summary>
        public user_auth_companyS(){}
        #endregion

        #region 属性方法
        /// <summary>
        /// user_auth_company集合 增加方法
        /// </summary>
        public void Add(user_auth_company entity)
        {
            this.List.Add(entity);
        }
        /// <summary>
        /// user_auth_company集合 索引
        /// </summary>
        public user_auth_company this[int index]
        {
            get { return (user_auth_company)this.List[index]; }
            set { this.List[index] = value; }
        }
        #endregion
    }
}
