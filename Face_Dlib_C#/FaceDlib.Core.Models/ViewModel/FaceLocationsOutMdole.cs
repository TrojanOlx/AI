using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FaceDlib.Core.Models.ViewModel
{


    public class Point
    {
        /// <summary>
        /// 
        /// </summary>
        public double Length { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double LengthSquared { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Y { get; set; }
    }

    public class DPoint
    {
        /// <summary>
        /// 
        /// </summary>
        public double Length { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double LengthSquared { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double Y { get; set; }
    }



    public class FaceLocationsOutMdole
    {
        /// <summary>
        /// 标识码
        /// </summary>
        public string FaceToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Area { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Bottom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Point BottomLeft { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Point BottomRight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Point Center { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DPoint DCenter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IsEmpty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Left { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Right { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Top { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Point TopLeft { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Point TopRight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 旋转角度
        /// </summary>
        public double Angle { get; set; }
        /// <summary>
        /// 人脸姿态
        /// </summary>
        public Gesture Gesture { get; set; }
    }

    public class Gesture
    {
        /// <summary>
        /// 三维旋转之左右旋转角[-90(左), 90(右)]
        /// </summary>
        public double Yaw { get; set; }
        /// <summary>
        /// 三维旋转之俯仰角度[-90(上), 90(下)]
        /// </summary>
        public double Pitch { get; set; }
        /// <summary>
        /// 平面内旋转角[-180(逆时针), 180(顺时针)]
        /// </summary>
        public double Roll { get; set; }

    }
}
