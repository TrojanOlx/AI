/******************************************
* 模块名称：实体 user_charge
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
	/// 实体 user_charge
	/// </summary>
	[Description("Primary:id")]
    [Serializable]
	public partial class user_charge
	{
        #region 构造函数
        /// <summary>
        /// 实体 user_charge
        /// </summary>
        public user_charge(){}
        #endregion

        #region 私有变量
        private long _id = long.MinValue;
        private long _user_id = long.MinValue;
        private string _order_number = null;
        private string _charge_type = null;
        private string _pay_type = null;
        private string _amount = null;
        private string _pay_card_name = null;
        private string _pay_card = null;
        private string _pay_card_img = null;
        private Int32 _pay_status = Int32.MinValue;
        private string _pay_response = null;
        private string _check_time = null;
        private string _created_at = null;
        private string _updated_at = null;
        private Boolean _is_increase_success = false;
        private string _refuse_info = null;
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
        /// 唯一订单号(NOT NULL)
        /// </summary>
        public string order_number
        {
            set{ _order_number=value;}
            get{return _order_number;}
        }
        /// <summary>
        /// 充值方式，在线充值和线下充值(NOT NULL)
        /// </summary>
        public string charge_type
        {
            set{ _charge_type=value;}
            get{return _charge_type;}
        }
        /// <summary>
        /// 充值类型，支付宝、银行转账(NOT NULL)
        /// </summary>
        public string pay_type
        {
            set{ _pay_type=value;}
            get{return _pay_type;}
        }
        /// <summary>
        /// 金额(NOT NULL)
        /// </summary>
        public string amount
        {
            set{ _amount=value;}
            get{return _amount;}
        }
        /// <summary>
        /// 银行卡姓名(NOT NULL)
        /// </summary>
        public string pay_card_name
        {
            set{ _pay_card_name=value;}
            get{return _pay_card_name;}
        }
        /// <summary>
        /// 银行卡号(NOT NULL)
        /// </summary>
        public string pay_card
        {
            set{ _pay_card=value;}
            get{return _pay_card;}
        }
        /// <summary>
        /// 转账截图(NOT NULL)
        /// </summary>
        public string pay_card_img
        {
            set{ _pay_card_img=value;}
            get{return _pay_card_img;}
        }
        /// <summary>
        /// 0审核中，1通过，2拒绝
        /// </summary>
        public Int32 pay_status
        {
            set{ _pay_status=value;}
            get{return _pay_status;}
        }
        /// <summary>
        /// 在线支付的返回值(NOT NULL)
        /// </summary>
        public string pay_response
        {
            set{ _pay_response=value;}
            get{return _pay_response;}
        }
        /// <summary>
        /// 审核的时间
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
        /// <summary>
        /// 是否增加了费用
        /// </summary>
        public Boolean is_increase_success
        {
            set{ _is_increase_success=value;}
            get{return _is_increase_success;}
        }
        /// <summary>
        /// 拒绝理由
        /// </summary>
        public string refuse_info
        {
            set{ _refuse_info=value;}
            get{return _refuse_info;}
        }
        #endregion

        #region 公共静态只读属性
        /// <summary>
        /// 表名 表原信息描述: user_charge
        /// </summary>
        public static readonly string s_TableName =  "user_charge";
        /// <summary>
        /// 信息描述: id(NOT NULL)
        /// </summary>
        public static readonly string s_id =  "user_charge┋id┋System.Int64";
        /// <summary>
        /// 信息描述: user_id(NOT NULL)
        /// </summary>
        public static readonly string s_user_id =  "user_charge┋user_id┋System.Int64";
        /// <summary>
        /// 信息描述: 唯一订单号(NOT NULL)
        /// </summary>
        public static readonly string s_order_number =  "user_charge┋order_number┋System.String";
        /// <summary>
        /// 信息描述: 充值方式，在线充值和线下充值(NOT NULL)
        /// </summary>
        public static readonly string s_charge_type =  "user_charge┋charge_type┋System.String";
        /// <summary>
        /// 信息描述: 充值类型，支付宝、银行转账(NOT NULL)
        /// </summary>
        public static readonly string s_pay_type =  "user_charge┋pay_type┋System.String";
        /// <summary>
        /// 信息描述: 金额(NOT NULL)
        /// </summary>
        public static readonly string s_amount =  "user_charge┋amount┋System.String";
        /// <summary>
        /// 信息描述: 银行卡姓名(NOT NULL)
        /// </summary>
        public static readonly string s_pay_card_name =  "user_charge┋pay_card_name┋System.String";
        /// <summary>
        /// 信息描述: 银行卡号(NOT NULL)
        /// </summary>
        public static readonly string s_pay_card =  "user_charge┋pay_card┋System.String";
        /// <summary>
        /// 信息描述: 转账截图(NOT NULL)
        /// </summary>
        public static readonly string s_pay_card_img =  "user_charge┋pay_card_img┋System.String";
        /// <summary>
        /// 信息描述: 0审核中，1通过，2拒绝
        /// </summary>
        public static readonly string s_pay_status =  "user_charge┋pay_status┋System.Int32";
        /// <summary>
        /// 信息描述: 在线支付的返回值(NOT NULL)
        /// </summary>
        public static readonly string s_pay_response =  "user_charge┋pay_response┋System.String";
        /// <summary>
        /// 信息描述: 审核的时间
        /// </summary>
        public static readonly string s_check_time =  "user_charge┋check_time┋System.String";
        /// <summary>
        /// 信息描述: created_at
        /// </summary>
        public static readonly string s_created_at =  "user_charge┋created_at┋System.String";
        /// <summary>
        /// 信息描述: updated_at
        /// </summary>
        public static readonly string s_updated_at =  "user_charge┋updated_at┋System.String";
        /// <summary>
        /// 信息描述: 是否增加了费用
        /// </summary>
        public static readonly string s_is_increase_success =  "user_charge┋is_increase_success┋System.Boolean";
        /// <summary>
        /// 信息描述: 拒绝理由
        /// </summary>
        public static readonly string s_refuse_info =  "user_charge┋refuse_info┋System.String";
        #endregion
	}

    /// <summary>
    /// user_charge实体集
    /// </summary>
    [Serializable]
    public class user_chargeS : CollectionBase
    {
        #region 构造函数
        /// <summary>
        /// user_charge实体集
        /// </summary>
        public user_chargeS(){}
        #endregion

        #region 属性方法
        /// <summary>
        /// user_charge集合 增加方法
        /// </summary>
        public void Add(user_charge entity)
        {
            this.List.Add(entity);
        }
        /// <summary>
        /// user_charge集合 索引
        /// </summary>
        public user_charge this[int index]
        {
            get { return (user_charge)this.List[index]; }
            set { this.List[index] = value; }
        }
        #endregion
    }
}
