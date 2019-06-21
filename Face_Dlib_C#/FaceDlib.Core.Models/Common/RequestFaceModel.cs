using FaceDlib.Core.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FaceDlib.Core.Models.Common
{
    public class RequestFaceModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 人脸相似度得分
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public float Score { get; set; }

        /// <summary>
        /// 人脸标志
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FaceToken { get; set; }
        
        /// <summary>
        /// 数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object FaceList { get; set; }

        /// <summary>
        /// 68个特征点
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<List<Landmark68ViewModler>> LandmarkList { get; set; }


        /// <summary>
        /// 单个位置
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Location { get; set; }



        /// <summary>
        /// 人脸数量
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int FaceNum { get; set; }

        /// <summary>
        /// 用户组列表
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object GroupIdList { get; set; }



        /// <summary>
        /// 用户列表
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object UserList { get; set; }


        /// <summary>
        /// 用户ID列表
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object UserIdList { get; set; }




        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }


        public RequestEnum Enum
        {
            set
            {
                Status = Status == 500 ? (int)value : Status;
                Message = string.IsNullOrWhiteSpace(Message) ? value.ToString() : Message;
            }
        }


    }
}
