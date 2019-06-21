﻿/******************************************
* 模块名称：实体 user_face_set
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
	/// 实体 user_face_set
	/// </summary>
	[Description("Primary:id")]
    [Serializable]
	public partial class user_face_set
	{
        #region 构造函数
        /// <summary>
        /// 实体 user_face_set
        /// </summary>
        public user_face_set(){}
        #endregion

        #region 私有变量
        private long _id = long.MinValue;
        private long _user_id = long.MinValue;
        private string _secret_id = null;
        private string _secret_key = null;
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
        /// secret_id(NOT NULL)
        /// </summary>
        public string secret_id
        {
            set{ _secret_id=value;}
            get{return _secret_id;}
        }
        /// <summary>
        /// secret_key(NOT NULL)
        /// </summary>
        public string secret_key
        {
            set{ _secret_key=value;}
            get{return _secret_key;}
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
        /// 表名 表原信息描述: user_face_set
        /// </summary>
        public static readonly string s_TableName =  "user_face_set";
        /// <summary>
        /// 信息描述: id(NOT NULL)
        /// </summary>
        public static readonly string s_id =  "user_face_set┋id┋System.Int64";
        /// <summary>
        /// 信息描述: user_id(NOT NULL)
        /// </summary>
        public static readonly string s_user_id =  "user_face_set┋user_id┋System.Int64";
        /// <summary>
        /// 信息描述: secret_id(NOT NULL)
        /// </summary>
        public static readonly string s_secret_id =  "user_face_set┋secret_id┋System.String";
        /// <summary>
        /// 信息描述: secret_key(NOT NULL)
        /// </summary>
        public static readonly string s_secret_key =  "user_face_set┋secret_key┋System.String";
        /// <summary>
        /// 信息描述: created_at
        /// </summary>
        public static readonly string s_created_at =  "user_face_set┋created_at┋System.String";
        /// <summary>
        /// 信息描述: updated_at
        /// </summary>
        public static readonly string s_updated_at =  "user_face_set┋updated_at┋System.String";
        #endregion
	}

    /// <summary>
    /// user_face_set实体集
    /// </summary>
    [Serializable]
    public class user_face_setS : CollectionBase
    {
        #region 构造函数
        /// <summary>
        /// user_face_set实体集
        /// </summary>
        public user_face_setS(){}
        #endregion

        #region 属性方法
        /// <summary>
        /// user_face_set集合 增加方法
        /// </summary>
        public void Add(user_face_set entity)
        {
            this.List.Add(entity);
        }
        /// <summary>
        /// user_face_set集合 索引
        /// </summary>
        public user_face_set this[int index]
        {
            get { return (user_face_set)this.List[index]; }
            set { this.List[index] = value; }
        }
        #endregion
    }
}
