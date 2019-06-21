/******************************************
* 模块名称：实体 user_face_set_history
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
	/// 实体 user_face_set_history
	/// </summary>
	[Description("Primary:id")]
    [Serializable]
	public partial class user_face_set_history
	{
        #region 构造函数
        /// <summary>
        /// 实体 user_face_set_history
        /// </summary>
        public user_face_set_history(){}
        #endregion

        #region 私有变量
        private long _id = long.MinValue;
        private long _user_id = long.MinValue;
        private string _old_secret_key = null;
        private string _new_secret_key = null;
        private string _change_client_ip = null;
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
        /// old_secret_key(NOT NULL)
        /// </summary>
        public string old_secret_key
        {
            set{ _old_secret_key=value;}
            get{return _old_secret_key;}
        }
        /// <summary>
        /// new_secret_key(NOT NULL)
        /// </summary>
        public string new_secret_key
        {
            set{ _new_secret_key=value;}
            get{return _new_secret_key;}
        }
        /// <summary>
        /// change_client_ip(NOT NULL)
        /// </summary>
        public string change_client_ip
        {
            set{ _change_client_ip=value;}
            get{return _change_client_ip;}
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
        /// 表名 表原信息描述: user_face_set_history
        /// </summary>
        public static readonly string s_TableName =  "user_face_set_history";
        /// <summary>
        /// 信息描述: id(NOT NULL)
        /// </summary>
        public static readonly string s_id =  "user_face_set_history┋id┋System.Int64";
        /// <summary>
        /// 信息描述: user_id(NOT NULL)
        /// </summary>
        public static readonly string s_user_id =  "user_face_set_history┋user_id┋System.Int64";
        /// <summary>
        /// 信息描述: old_secret_key(NOT NULL)
        /// </summary>
        public static readonly string s_old_secret_key =  "user_face_set_history┋old_secret_key┋System.String";
        /// <summary>
        /// 信息描述: new_secret_key(NOT NULL)
        /// </summary>
        public static readonly string s_new_secret_key =  "user_face_set_history┋new_secret_key┋System.String";
        /// <summary>
        /// 信息描述: change_client_ip(NOT NULL)
        /// </summary>
        public static readonly string s_change_client_ip =  "user_face_set_history┋change_client_ip┋System.String";
        /// <summary>
        /// 信息描述: created_at
        /// </summary>
        public static readonly string s_created_at =  "user_face_set_history┋created_at┋System.String";
        /// <summary>
        /// 信息描述: updated_at
        /// </summary>
        public static readonly string s_updated_at =  "user_face_set_history┋updated_at┋System.String";
        #endregion
	}

    /// <summary>
    /// user_face_set_history实体集
    /// </summary>
    [Serializable]
    public class user_face_set_historyS : CollectionBase
    {
        #region 构造函数
        /// <summary>
        /// user_face_set_history实体集
        /// </summary>
        public user_face_set_historyS(){}
        #endregion

        #region 属性方法
        /// <summary>
        /// user_face_set_history集合 增加方法
        /// </summary>
        public void Add(user_face_set_history entity)
        {
            this.List.Add(entity);
        }
        /// <summary>
        /// user_face_set_history集合 索引
        /// </summary>
        public user_face_set_history this[int index]
        {
            get { return (user_face_set_history)this.List[index]; }
            set { this.List[index] = value; }
        }
        #endregion
    }
}
