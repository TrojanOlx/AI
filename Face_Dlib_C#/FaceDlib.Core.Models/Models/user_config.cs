﻿/******************************************
* 模块名称：实体 user_config
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
	/// 实体 user_config
	/// </summary>
	[Description("Primary:id")]
    [Serializable]
	public partial class user_config
	{
        #region 构造函数
        /// <summary>
        /// 实体 user_config
        /// </summary>
        public user_config(){}
        #endregion

        #region 私有变量
        private long _id = long.MinValue;
        private long _user_id = long.MinValue;
        private string _name = null;
        private string _value = null;
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
        /// config的Name名称(NOT NULL)
        /// </summary>
        public string name
        {
            set{ _name=value;}
            get{return _name;}
        }
        /// <summary>
        /// config对应的值(NOT NULL)
        /// </summary>
        public string value
        {
            set{ _value=value;}
            get{return _value;}
        }
        /// <summary>
        /// 用户id和Name的拼接字符串，避免重复提交(NOT NULL)
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
        /// 表名 表原信息描述: user_config
        /// </summary>
        public static readonly string s_TableName =  "user_config";
        /// <summary>
        /// 信息描述: id(NOT NULL)
        /// </summary>
        public static readonly string s_id =  "user_config┋id┋System.Int64";
        /// <summary>
        /// 信息描述: user_id(NOT NULL)
        /// </summary>
        public static readonly string s_user_id =  "user_config┋user_id┋System.Int64";
        /// <summary>
        /// 信息描述: config的Name名称(NOT NULL)
        /// </summary>
        public static readonly string s_name =  "user_config┋name┋System.String";
        /// <summary>
        /// 信息描述: config对应的值(NOT NULL)
        /// </summary>
        public static readonly string s_value =  "user_config┋value┋System.String";
        /// <summary>
        /// 信息描述: 用户id和Name的拼接字符串，避免重复提交(NOT NULL)
        /// </summary>
        public static readonly string s_index =  "user_config┋index┋System.String";
        /// <summary>
        /// 信息描述: created_at
        /// </summary>
        public static readonly string s_created_at =  "user_config┋created_at┋System.String";
        /// <summary>
        /// 信息描述: updated_at
        /// </summary>
        public static readonly string s_updated_at =  "user_config┋updated_at┋System.String";
        #endregion
	}

    /// <summary>
    /// user_config实体集
    /// </summary>
    [Serializable]
    public class user_configS : CollectionBase
    {
        #region 构造函数
        /// <summary>
        /// user_config实体集
        /// </summary>
        public user_configS(){}
        #endregion

        #region 属性方法
        /// <summary>
        /// user_config集合 增加方法
        /// </summary>
        public void Add(user_config entity)
        {
            this.List.Add(entity);
        }
        /// <summary>
        /// user_config集合 索引
        /// </summary>
        public user_config this[int index]
        {
            get { return (user_config)this.List[index]; }
            set { this.List[index] = value; }
        }
        #endregion
    }
}