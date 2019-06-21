using FaceDlib.Core.Models.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FaceDlib.Core.Models.ViewModel
{
    public class FaceContrastViewModel
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
        /// 图片
        /// </summary>
        [EnsureMinimumElements(1,2,ErrorMessage ="图片数量不正确")]
        public List<FaceImage> images { get; set; }


        private string _face_field;
        /// <summary>
        /// 返回类型
        /// </summary>
        //[Required(ErrorMessage = "请输入返回类型")]
        public string face_field {
            get
            {
                if (string.IsNullOrWhiteSpace(_face_field))
                {
                    _face_field = "Score";
                }
                return _face_field;
             }
            set { _face_field = string.IsNullOrWhiteSpace(value) ? "Score" : value; } }
    }

    public class FaceImage {
        public string image { get; set; }

        public string image_type { get; set; }

        /// <summary>
        /// LIVE表示生活照：通常为手机、相机拍摄的人像图片、或从网络获取的人像图片等
        /// IDCARD表示身份证芯片照：二代身份证内置芯片中的人像照片
        /// WATERMARK表示带水印证件照：一般为带水印的小图，如公安网小图
        /// CERT表示证件照片：如拍摄的身份证、工卡、护照、学生证等证件图片
        /// 默认LIVE
        /// </summary>
        private string _face_type;
        public string face_type { get { return _face_type; } set { _face_type = value == null ? "LIVE" : value; } }

    }
}
