/******************************************
* 模块名称：实体 user_face_image_data
* 当前版本：1.0
* 开发人员：Trojan
* 生成时间：2019/1/31
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
	/// 实体 user_face_image_data
	/// </summary>
	[Description("Primary:id")]
    [Serializable]
	public partial class user_face_image_data
	{
        #region 构造函数
        /// <summary>
        /// 实体 user_face_image_data
        /// </summary>
        public user_face_image_data(){}
        #endregion

        #region 私有变量
        private long _id = long.MinValue;
        private string _token = null;
        private string _url = null;
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
        /// token
        /// </summary>
        public string token
        {
            set{ _token=value;}
            get{return _token;}
        }
        /// <summary>
        /// url
        /// </summary>
        public string url
        {
            set{ _url=value;}
            get{return _url;}
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
        /// 更新时间
        /// </summary>
        public DateTime updated_at
        {
            set{ _updated_at=value;}
            get{return _updated_at;}
        }
        #endregion

        #region 公共静态只读属性
        /// <summary>
        /// 表名 表原信息描述: user_face_image_data
        /// </summary>
        public static readonly string s_TableName =  "user_face_image_data";
        /// <summary>
        /// 信息描述: id(NOT NULL)
        /// </summary>
        public static readonly string s_id =  "user_face_image_data┋id┋System.Int64";
        /// <summary>
        /// 信息描述: token
        /// </summary>
        public static readonly string s_token =  "user_face_image_data┋token┋System.String";
        /// <summary>
        /// 信息描述: url
        /// </summary>
        public static readonly string s_url =  "user_face_image_data┋url┋System.String";
        /// <summary>
        /// 信息描述: 创建时间
        /// </summary>
        public static readonly string s_created_at = "user_face_image_data┋created_at┋System.DateTime";
        /// <summary>
        /// 信息描述: 更新时间
        /// </summary>
        public static readonly string s_updated_at = "user_face_image_data┋updated_at┋System.DateTime";
        #endregion
	}

    /// <summary>
    /// user_face_image_data实体集
    /// </summary>
    [Serializable]
    public class user_face_image_dataS : CollectionBase
    {
        #region 构造函数
        /// <summary>
        /// user_face_image_data实体集
        /// </summary>
        public user_face_image_dataS(){}
        #endregion

        #region 属性方法
        /// <summary>
        /// user_face_image_data集合 增加方法
        /// </summary>
        public void Add(user_face_image_data entity)
        {
            this.List.Add(entity);
        }
        /// <summary>
        /// user_face_image_data集合 索引
        /// </summary>
        public user_face_image_data this[int index]
        {
            get { return (user_face_image_data)this.List[index]; }
            set { this.List[index] = value; }
        }
        #endregion
    }
}
