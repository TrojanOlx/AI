/******************************************
* 模块名称：实体 user_face_compare_prepayment_detail
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
	/// 实体 user_face_compare_prepayment_detail
	/// </summary>
	[Description("Primary:id")]
    [Serializable]
	public partial class user_face_compare_prepayment_detail
	{
        #region 构造函数
        /// <summary>
        /// 实体 user_face_compare_prepayment_detail
        /// </summary>
        public user_face_compare_prepayment_detail(){}
        #endregion

        #region 私有变量
        private long _id = long.MinValue;
        private long _user_id = long.MinValue;
        private long _change_number = long.MinValue;
        private long _now_number = long.MinValue;
        private string _info = null;
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
        /// 用户ID(NOT NULL)
        /// </summary>
        public long user_id
        {
            set{ _user_id=value;}
            get{return _user_id;}
        }
        /// <summary>
        /// 改变数量(NOT NULL)
        /// </summary>
        public long change_number
        {
            set{ _change_number=value;}
            get{return _change_number;}
        }
        /// <summary>
        /// 改变后的数量(NOT NULL)
        /// </summary>
        public long now_number
        {
            set{ _now_number=value;}
            get{return _now_number;}
        }
        /// <summary>
        /// info(NOT NULL)
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
        #endregion

        #region 公共静态只读属性
        /// <summary>
        /// 表名 表原信息描述: user_face_compare_prepayment_detail
        /// </summary>
        public static readonly string s_TableName =  "user_face_compare_prepayment_detail";
        /// <summary>
        /// 信息描述: id(NOT NULL)
        /// </summary>
        public static readonly string s_id =  "user_face_compare_prepayment_detail┋id┋System.Int64";
        /// <summary>
        /// 信息描述: 用户ID(NOT NULL)
        /// </summary>
        public static readonly string s_user_id =  "user_face_compare_prepayment_detail┋user_id┋System.Int64";
        /// <summary>
        /// 信息描述: 改变数量(NOT NULL)
        /// </summary>
        public static readonly string s_change_number =  "user_face_compare_prepayment_detail┋change_number┋System.Int64";
        /// <summary>
        /// 信息描述: 改变后的数量(NOT NULL)
        /// </summary>
        public static readonly string s_now_number =  "user_face_compare_prepayment_detail┋now_number┋System.Int64";
        /// <summary>
        /// 信息描述: info(NOT NULL)
        /// </summary>
        public static readonly string s_info =  "user_face_compare_prepayment_detail┋info┋System.String";
        /// <summary>
        /// 信息描述: created_at
        /// </summary>
        public static readonly string s_created_at =  "user_face_compare_prepayment_detail┋created_at┋System.String";
        /// <summary>
        /// 信息描述: updated_at
        /// </summary>
        public static readonly string s_updated_at =  "user_face_compare_prepayment_detail┋updated_at┋System.String";
        #endregion
	}

    /// <summary>
    /// user_face_compare_prepayment_detail实体集
    /// </summary>
    [Serializable]
    public class user_face_compare_prepayment_detailS : CollectionBase
    {
        #region 构造函数
        /// <summary>
        /// user_face_compare_prepayment_detail实体集
        /// </summary>
        public user_face_compare_prepayment_detailS(){}
        #endregion

        #region 属性方法
        /// <summary>
        /// user_face_compare_prepayment_detail集合 增加方法
        /// </summary>
        public void Add(user_face_compare_prepayment_detail entity)
        {
            this.List.Add(entity);
        }
        /// <summary>
        /// user_face_compare_prepayment_detail集合 索引
        /// </summary>
        public user_face_compare_prepayment_detail this[int index]
        {
            get { return (user_face_compare_prepayment_detail)this.List[index]; }
            set { this.List[index] = value; }
        }
        #endregion
    }
}
