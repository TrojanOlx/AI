using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FaceDlib.Core.Models.ViewModel
{
    public class FaceLocationsViewModel
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        [Required(ErrorMessage = "应用ID不能为空")]
        public string secret_id { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        [Required(ErrorMessage = "时间戳不能为空")]
        public long timestamp { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        [Required(ErrorMessage = "随机字符串不能为空")]
        public string nonce_str { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [Required(ErrorMessage = "签名不能为空")]
        public string sign { get; set; }
        /// <summary>
        /// 图片文件
        /// </summary>
        [Required(ErrorMessage = "图片不能为空")]
        public string image { get; set; }

        /// <summary>
        /// 图片类型
        /// </summary>
        [Required(ErrorMessage = "图片类型不能为空")]
        public string image_type { get; set; }

        ///// <summary>
        ///// 最多处理人脸的数目，默认值为1，仅检测图片中面积最大的那个人脸；最大值10，检测图片中面积最大的几张人脸。
        ///// </summary>

        private int _max_face_num = 1;
        [Required(ErrorMessage = "请输入处理人脸的数目")]
        public int max_face_num { get { return _max_face_num; } set { _max_face_num = value > 0 ? value : _max_face_num; } }

        private string _face_field;
        [Required(ErrorMessage = "请输入返回类型")]
        public string face_field { get {return _face_field; } set { _face_field=value==null? "face_shape":value; }  }
        /// <summary>
        /// LIVE表示生活照：通常为手机、相机拍摄的人像图片、或从网络获取的人像图片等
        /// IDCARD表示身份证芯片照：二代身份证内置芯片中的人像照片
        /// WATERMARK表示带水印证件照：一般为带水印的小图，如公安网小图
        /// CERT表示证件照片：如拍摄的身份证、工卡、护照、学生证等证件图片
        /// 默认LIVE
        /// </summary>
        private string _face_type;
        public string face_type { get {return _face_type; } set {_face_type=value==null? "LIVE" :value; } }
    }
}
