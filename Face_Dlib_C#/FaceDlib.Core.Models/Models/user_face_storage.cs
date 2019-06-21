/******************************************
* 模块名称：实体 user_face_storage
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
	/// 实体 user_face_storage
	/// </summary>
	[Description("Primary:id")]
    [Serializable]
	public partial class user_face_storage
	{
        #region 构造函数
        /// <summary>
        /// 实体 user_face_storage
        /// </summary>
        public user_face_storage(){}
        #endregion

        #region 私有变量
        private long _id = long.MinValue;
        private long _user_id = long.MinValue;
        private string _secret_id = null;
        private string _face_token = null;
        private string _image = null;
        private string _image_type = null;
        private long _api_group_id = long.MinValue;
        private string _api_user_id = null;
        private string _api_user_info = null;
        private string _quality_control = null;
        private string _liveness_control = null;
        private string _sign = null;
        private string _timestamp = null;
        private Boolean _is_delete = false;
        private string _api_respone = null;
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
        /// user_id(NOT NULL)
        /// </summary>
        public long user_id
        {
            set{ _user_id=value;}
            get{return _user_id;}
        }
        /// <summary>
        /// 应用的ID(NOT NULL)
        /// </summary>
        public string secret_id
        {
            set{ _secret_id=value;}
            get{return _secret_id;}
        }
        /// <summary>
        /// 根据图片路径生成的唯一token(NOT NULL)
        /// </summary>
        public string face_token
        {
            set{ _face_token=value;}
            get{return _face_token;}
        }
        /// <summary>
        /// 图片路径或者BASE64位的图片
        /// </summary>
        public string image
        {
            set{ _image=value;}
            get{return _image;}
        }
        /// <summary>
        /// 图片类型(NOT NULL)
        /// </summary>
        public string image_type
        {
            set{ _image_type=value;}
            get{return _image_type;}
        }
        /// <summary>
        /// 用户提交的GroupId(NOT NULL)
        /// </summary>
        public long api_group_id
        {
            set{ _api_group_id=value;}
            get{return _api_group_id;}
        }
        /// <summary>
        /// 用户提交的UserId(NOT NULL)
        /// </summary>
        public string api_user_id
        {
            set{ _api_user_id=value;}
            get{return _api_user_id;}
        }
        /// <summary>
        /// 用户提交的UserInfo
        /// </summary>
        public string api_user_info
        {
            set{ _api_user_info=value;}
            get{return _api_user_info;}
        }
        /// <summary>
        /// 图片质量控制
        /// </summary>
        public string quality_control
        {
            set{ _quality_control=value;}
            get{return _quality_control;}
        }
        /// <summary>
        /// 活体检测控制
        /// </summary>
        public string liveness_control
        {
            set{ _liveness_control=value;}
            get{return _liveness_control;}
        }
        /// <summary>
        /// 签名(NOT NULL)
        /// </summary>
        public string sign
        {
            set{ _sign=value;}
            get{return _sign;}
        }
        /// <summary>
        /// 当前的时间搓(NOT NULL)
        /// </summary>
        public string timestamp
        {
            set{ _timestamp=value;}
            get{return _timestamp;}
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public Boolean is_delete
        {
            set{ _is_delete=value;}
            get{return _is_delete;}
        }
        /// <summary>
        /// 云片返回的信息
        /// </summary>
        public string api_respone
        {
            set{ _api_respone=value;}
            get{return _api_respone;}
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
        /// 表名 表原信息描述: user_face_storage
        /// </summary>
        public static readonly string s_TableName =  "user_face_storage";
        /// <summary>
        /// 信息描述: id(NOT NULL)
        /// </summary>
        public static readonly string s_id =  "user_face_storage┋id┋System.Int64";
        /// <summary>
        /// 信息描述: user_id(NOT NULL)
        /// </summary>
        public static readonly string s_user_id =  "user_face_storage┋user_id┋System.Int64";
        /// <summary>
        /// 信息描述: 应用的ID(NOT NULL)
        /// </summary>
        public static readonly string s_secret_id =  "user_face_storage┋secret_id┋System.String";
        /// <summary>
        /// 信息描述: 根据图片路径生成的唯一token(NOT NULL)
        /// </summary>
        public static readonly string s_face_token =  "user_face_storage┋face_token┋System.String";
        /// <summary>
        /// 信息描述: 图片路径或者BASE64位的图片
        /// </summary>
        public static readonly string s_image =  "user_face_storage┋image┋System.String";
        /// <summary>
        /// 信息描述: 图片类型(NOT NULL)
        /// </summary>
        public static readonly string s_image_type =  "user_face_storage┋image_type┋System.String";
        /// <summary>
        /// 信息描述: 用户提交的GroupId(NOT NULL)
        /// </summary>
        public static readonly string s_api_group_id =  "user_face_storage┋api_group_id┋System.Int64";
        /// <summary>
        /// 信息描述: 用户提交的UserId(NOT NULL)
        /// </summary>
        public static readonly string s_api_user_id =  "user_face_storage┋api_user_id┋System.String";
        /// <summary>
        /// 信息描述: 用户提交的UserInfo
        /// </summary>
        public static readonly string s_api_user_info =  "user_face_storage┋api_user_info┋System.String";
        /// <summary>
        /// 信息描述: 图片质量控制
        /// </summary>
        public static readonly string s_quality_control =  "user_face_storage┋quality_control┋System.String";
        /// <summary>
        /// 信息描述: 活体检测控制
        /// </summary>
        public static readonly string s_liveness_control =  "user_face_storage┋liveness_control┋System.String";
        /// <summary>
        /// 信息描述: 签名(NOT NULL)
        /// </summary>
        public static readonly string s_sign =  "user_face_storage┋sign┋System.String";
        /// <summary>
        /// 信息描述: 当前的时间搓(NOT NULL)
        /// </summary>
        public static readonly string s_timestamp =  "user_face_storage┋timestamp┋System.String";
        /// <summary>
        /// 信息描述: 是否删除
        /// </summary>
        public static readonly string s_is_delete =  "user_face_storage┋is_delete┋System.Boolean";
        /// <summary>
        /// 信息描述: 云片返回的信息
        /// </summary>
        public static readonly string s_api_respone =  "user_face_storage┋api_respone┋System.String";
        /// <summary>
        /// 信息描述: created_at
        /// </summary>
        public static readonly string s_created_at =  "user_face_storage┋created_at┋System.String";
        /// <summary>
        /// 信息描述: updated_at
        /// </summary>
        public static readonly string s_updated_at =  "user_face_storage┋updated_at┋System.String";
        #endregion
	}

    /// <summary>
    /// user_face_storage实体集
    /// </summary>
    [Serializable]
    public class user_face_storageS : CollectionBase
    {
        #region 构造函数
        /// <summary>
        /// user_face_storage实体集
        /// </summary>
        public user_face_storageS(){}
        #endregion

        #region 属性方法
        /// <summary>
        /// user_face_storage集合 增加方法
        /// </summary>
        public void Add(user_face_storage entity)
        {
            this.List.Add(entity);
        }
        /// <summary>
        /// user_face_storage集合 索引
        /// </summary>
        public user_face_storage this[int index]
        {
            get { return (user_face_storage)this.List[index]; }
            set { this.List[index] = value; }
        }
        #endregion
    }
}
