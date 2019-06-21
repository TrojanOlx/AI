/******************************************
* 模块名称：实体 user_amount_detail
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
	/// 实体 user_amount_detail
	/// </summary>
	[Description("Primary:id")]
    [Serializable]
	public partial class user_amount_detail
	{
        #region 构造函数
        /// <summary>
        /// 实体 user_amount_detail
        /// </summary>
        public user_amount_detail(){}
        #endregion

        #region 私有变量
        private long _id = long.MinValue;
        private long _user_id = long.MinValue;
        private decimal _change_amount = 0;
        private decimal _now_amount = 0;
        private string _info = null;
        private DateTime _created_at = new DateTime();
        private DateTime _updated_at = new DateTime();
        private Int32 _type = Int32.MinValue;
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
        /// 用户ID(NOT NULL)
        /// </summary>
        public long user_id
        {
            set{ _user_id=value;}
            get{return _user_id;}
        }
        /// <summary>
        /// 改变的金额，正数或者负数(NOT NULL)
        /// </summary>
        public decimal change_amount
        {
            set{ _change_amount=value;}
            get{return _change_amount;}
        }
        /// <summary>
        /// 改变后的金额(NOT NULL)
        /// </summary>
        public decimal now_amount
        {
            set{ _now_amount=value;}
            get{return _now_amount;}
        }
        /// <summary>
        /// 备注，要详细(NOT NULL)
        /// </summary>
        public string info
        {
            set{ _info=value;}
            get{return _info;}
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
        /// 支付类型,0其他，1短信，2充值
        /// </summary>
        public Int32 type
        {
            set{ _type=value;}
            get{return _type;}
        }
        #endregion

        #region 公共静态只读属性
        /// <summary>
        /// 表名 表原信息描述: user_amount_detail
        /// </summary>
        public static readonly string s_TableName =  "user_amount_detail";
        /// <summary>
        /// 信息描述: id(NOT NULL)
        /// </summary>
        public static readonly string s_id =  "user_amount_detail┋id┋System.Int64";
        /// <summary>
        /// 信息描述: 用户ID(NOT NULL)
        /// </summary>
        public static readonly string s_user_id =  "user_amount_detail┋user_id┋System.Int64";
        /// <summary>
        /// 信息描述: 改变的金额，正数或者负数(NOT NULL)
        /// </summary>
        public static readonly string s_change_amount = "user_amount_detail┋change_amount┋System.Decimal";
        /// <summary>
        /// 信息描述: 改变后的金额(NOT NULL)
        /// </summary>
        public static readonly string s_now_amount = "user_amount_detail┋now_amount┋System.Decimal";
        /// <summary>
        /// 信息描述: 备注，要详细(NOT NULL)
        /// </summary>
        public static readonly string s_info =  "user_amount_detail┋info┋System.String";
        /// <summary>
        /// 信息描述: created_at
        /// </summary>
        public static readonly string s_created_at =  "user_amount_detail┋created_at┋System.String";
        /// <summary>
        /// 信息描述: updated_at
        /// </summary>
        public static readonly string s_updated_at =  "user_amount_detail┋updated_at┋System.String";
        /// <summary>
        /// 信息描述: 支付类型,0其他，1短信，2充值
        /// </summary>
        public static readonly string s_type =  "user_amount_detail┋type┋System.Int32";
        #endregion







	}

    /// <summary>
    /// user_amount_detail实体集
    /// </summary>
    [Serializable]
    public class user_amount_detailS : CollectionBase
    {
        #region 构造函数
        /// <summary>
        /// user_amount_detail实体集
        /// </summary>
        public user_amount_detailS(){}
        #endregion

        #region 属性方法
        /// <summary>
        /// user_amount_detail集合 增加方法
        /// </summary>
        public void Add(user_amount_detail entity)
        {
            this.List.Add(entity);
        }
        /// <summary>
        /// user_amount_detail集合 索引
        /// </summary>
        public user_amount_detail this[int index]
        {
            get { return (user_amount_detail)this.List[index]; }
            set { this.List[index] = value; }
        }
        #endregion
    }
}
