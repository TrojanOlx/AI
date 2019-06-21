/******************************************
* 模块名称：实体 user_face_storage_group
* 当前版本：1.0
* 开发人员：Trojan
* 生成时间：2019/2/12
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
	/// 实体 user_face_storage_group
	/// </summary>
	[Description("Primary:id")]
    [Serializable]
	public partial class user_face_storage_group
	{
        #region 构造函数
        /// <summary>
        /// 实体 user_face_storage_group
        /// </summary>
        public user_face_storage_group(){}
        #endregion

        #region 私有变量
        private long _id = long.MinValue;
        private long _user_id = long.MinValue;
        private string _group_name = null;
        private string _remake = null;
        private DateTime _created_at = new DateTime();
        private string _secret_id = null;
        private DateTime _updated_at = new DateTime();
        private string _api_respone = null;
        private Boolean _is_delete = false;
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
        /// 创建人ID
        /// </summary>
        public long user_id
        {
            set{ _user_id=value;}
            get{return _user_id;}
        }
        /// <summary>
        /// 提交的分组信息
        /// </summary>
        public string group_name
        {
            set{ _group_name=value;}
            get{return _group_name;}
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remake
        {
            set{ _remake=value;}
            get{return _remake;}
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime created_at
        {
            set{ _created_at=value;}
            get{return _created_at;}
        }
        /// <summary>
        /// 应用id
        /// </summary>
        public string secret_id
        {
            set{ _secret_id=value;}
            get{return _secret_id;}
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updated_at
        {
            set{ _updated_at=value;}
            get{return _updated_at;}
        }
        /// <summary>
        /// 接口返回
        /// </summary>
        public string api_respone
        {
            set{ _api_respone=value;}
            get{return _api_respone;}
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public Boolean is_delete
        {
            set{ _is_delete=value;}
            get{return _is_delete;}
        }
        #endregion

        #region 公共静态只读属性
        /// <summary>
        /// 表名 表原信息描述: user_face_storage_group
        /// </summary>
        public static readonly string s_TableName =  "user_face_storage_group";
        /// <summary>
        /// 信息描述: id(NOT NULL)
        /// </summary>
        public static readonly string s_id =  "user_face_storage_group┋id┋System.Int64";
        /// <summary>
        /// 信息描述: 创建人ID
        /// </summary>
        public static readonly string s_user_id =  "user_face_storage_group┋user_id┋System.Int64";
        /// <summary>
        /// 信息描述: 提交的分组信息
        /// </summary>
        public static readonly string s_group_name =  "user_face_storage_group┋group_name┋System.String";
        /// <summary>
        /// 信息描述: 备注
        /// </summary>
        public static readonly string s_remake =  "user_face_storage_group┋remake┋System.String";
        /// <summary>
        /// 信息描述: 创建时间
        /// </summary>
        public static readonly string s_created_at =  "user_face_storage_group┋created_at┋System.String";
        /// <summary>
        /// 信息描述: 应用id
        /// </summary>
        public static readonly string s_secret_id =  "user_face_storage_group┋secret_id┋System.String";
        /// <summary>
        /// 信息描述: 更新时间
        /// </summary>
        public static readonly string s_updated_at =  "user_face_storage_group┋updated_at┋System.String";
        /// <summary>
        /// 信息描述: 接口返回
        /// </summary>
        public static readonly string s_api_respone =  "user_face_storage_group┋api_respone┋System.String";
        /// <summary>
        /// 信息描述: 是否删除
        /// </summary>
        public static readonly string s_is_delete =  "user_face_storage_group┋is_delete┋System.Boolean";
        #endregion
	}

    /// <summary>
    /// user_face_storage_group实体集
    /// </summary>
    [Serializable]
    public class user_face_storage_groupS : CollectionBase
    {
        #region 构造函数
        /// <summary>
        /// user_face_storage_group实体集
        /// </summary>
        public user_face_storage_groupS(){}
        #endregion

        #region 属性方法
        /// <summary>
        /// user_face_storage_group集合 增加方法
        /// </summary>
        public void Add(user_face_storage_group entity)
        {
            this.List.Add(entity);
        }
        /// <summary>
        /// user_face_storage_group集合 索引
        /// </summary>
        public user_face_storage_group this[int index]
        {
            get { return (user_face_storage_group)this.List[index]; }
            set { this.List[index] = value; }
        }
        #endregion
    }
}
