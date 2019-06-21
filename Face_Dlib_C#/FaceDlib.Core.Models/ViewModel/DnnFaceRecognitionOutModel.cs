using DlibDotNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FaceDlib.Core.Models.ViewModel
{
    public class DnnFaceRecognitionOutModel
    {
    }

    public class ContrastData {
        /// <summary>
        /// 人脸token
        /// </summary>
        public string face_token { get; set; }

        /// <summary>
        /// 坐标
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Rectangle[] rectangles { get; set; }

    }
}
