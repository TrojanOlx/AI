/******************************************
* 模块名称：实体 user_face_compare_prepayment
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
	/// 实体 user_face_compare_prepayment
	/// </summary>
	[Description("Primary:id")]
    [Serializable]
	public partial class user_face_compare_prepayment
	{
        #region 构造函数
        /// <summary>
        /// 实体 user_face_compare_prepayment
        /// </summary>
        public user_face_compare_prepayment(){}
        #endregion

        #region 私有变量
        private long _id = long.MinValue;
        private long _user_id = long.MinValue;
        private long _number = long.MinValue;
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
        /// 用户ID(NOT NULL)
        /// </summary>
        public long user_id
        {
            set{ _user_id=value;}
            get{return _user_id;}
        }
        /// <summary>
        /// 数量(NOT NULL)
        /// </summary>
        public long number
        {
            set{ _number=value;}
            get{return _number;}
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
        /// 表名 表原信息描述: user_face_compare_prepayment
        /// </summary>
        public static readonly string s_TableName =  "user_face_compare_prepayment";
        /// <summary>
        /// 信息描述: id(NOT NULL)
        /// </summary>
        public static readonly string s_id =  "user_face_compare_prepayment┋id┋System.Int64";
        /// <summary>
        /// 信息描述: 用户ID(NOT NULL)
        /// </summary>
        public static readonly string s_user_id =  "user_face_compare_prepayment┋user_id┋System.Int64";
        /// <summary>
        /// 信息描述: 数量(NOT NULL)
        /// </summary>
        public static readonly string s_number =  "user_face_compare_prepayment┋number┋System.Int64";
        /// <summary>
        /// 信息描述: created_at
        /// </summary>
        public static readonly string s_created_at =  "user_face_compare_prepayment┋created_at┋System.String";
        /// <summary>
        /// 信息描述: updated_at
        /// </summary>
        public static readonly string s_updated_at =  "user_face_compare_prepayment┋updated_at┋System.String";
        #endregion
	}

    /// <summary>
    /// user_face_compare_prepayment实体集
    /// </summary>
    [Serializable]
    public class user_face_compare_prepaymentS : CollectionBase
    {
        #region 构造函数
        /// <summary>
        /// user_face_compare_prepayment实体集
        /// </summary>
        public user_face_compare_prepaymentS(){}
        #endregion

        #region 属性方法
        /// <summary>
        /// user_face_compare_prepayment集合 增加方法
        /// </summary>
        public void Add(user_face_compare_prepayment entity)
        {
            this.List.Add(entity);
        }
        /// <summary>
        /// user_face_compare_prepayment集合 索引
        /// </summary>
        public user_face_compare_prepayment this[int index]
        {
            get { return (user_face_compare_prepayment)this.List[index]; }
            set { this.List[index] = value; }
        }
        #endregion
    }
}
