using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using DlibDotNet;
using DlibDotNet.Extensions;
using Dlib = DlibDotNet.Dlib;
using Rectangle = DlibDotNet.Rectangle;
using FaceDlib.Core.Common;
using System.IO;
using OpenCvSharp;

namespace DlibCore.Computer
{
    public class FaceDetectionCompute
    {
        /// <summary>
        /// 单张图片
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Rectangle[] GetRectangle(Bitmap bitmap)
        {
            var rectangles = GetRectangle(new List<Bitmap>() { bitmap });
            return rectangles != null ? rectangles[0] : null;
        }

        /// <summary>
        /// 多张图片
        /// </summary>
        /// <param name="bitmaps"></param>
        /// <returns></returns>
        public static List<Rectangle[]> GetRectangle(List<Bitmap> bitmaps)
        {
            // 要返回的数据
            List<Rectangle[]> rectangles = new List<Rectangle[]>();
            try
            {
                rectangles = GetDate(bitmaps);
            }
            catch (Exception ex)
            {
                // 出错日志打印
                LogHelperNLog.Error(ex, "FaceDetectionCompute.GetRectangles:人脸获取坐标出错");
            }
            return rectangles.Count > 0 ? rectangles : null;
        }


        /// <summary>
        /// 具体计算
        /// </summary>
        /// <param name="bitmaps"></param>
        /// <returns></returns>
        private static List<Rectangle[]> GetDate(List<Bitmap> bitmaps)
        {
            List<Rectangle[]> rectangles = new List<Rectangle[]>();
            // 检测器
            using (var detector = Dlib.GetFrontalFaceDetector())
            {
                // 循环所有的图片
                foreach (var bitmap in bitmaps)
                {
                    // 图片格式转化
                    using (var img = bitmap.ToArray2D<RgbPixel>())
                    {
                        // 获取位置数据
                        var dets = detector.Operator(img);
                        rectangles.Add(dets);
                    }
                }
            }
            return rectangles;
        }

        /// <summary>
        /// 使用路径获取位置数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Rectangle[] GetResult(string url)
        {
            var dets = new Rectangle[0];
            url = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, url);
            using (var detector = Dlib.GetFrontalFaceDetector())
            //using (var img = Dlib.LoadImage<RgbPixel>("png.png"))
            using (var img = Dlib.LoadImage<RgbPixel>(url))
            {

                dets = detector.Operator(img);
            }
            return dets;
        }

        /// <summary>
        /// 旋转角度
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public static double GetRotationAngle(FullObjectDetection shape)
        {
            return GetRotationAngle(new List<FullObjectDetection>() { shape })[0];
        }
        /// <summary>
        /// 旋转角度
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public static List<double> GetRotationAngle(IEnumerable<FullObjectDetection> shapes)
        {
            List<double> angles = new List<double>();
            foreach (var shape in shapes)
            {
                var p1 = shape.GetPart(0);
                var p2 = shape.GetPart(16);
                var dx = (double)(p2.X - p1.X);
                var dy = (double)(p2.Y - p1.Y);
                var angle = (Math.Atan(dy / dx)) * 180 / Math.PI;
                // 后期代码优化，加精确度
                angles.Add(angle);
            }

            return angles;
        }

        /// <summary>
        /// 姿态预测
        /// </summary>
        /// <param name="shapes"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>List<(Y, X, Z)></returns>
        public static List<(double, double, double)> GetFaceGesture(IEnumerable<FullObjectDetection> shapes, double width, double height)
        {
            List<(double, double, double)> angleS = new List<(double, double, double)>();
            if (width == 0 || height == 0)
            {
                return angleS;
            }

            foreach (var shape in shapes)
            {
                var rvec = new double[] { 0, 0, 0 };//旋转矢量
                var tvec = new double[] { 0, 0, 0 };//平移矢量

                //假设没有透镜畸变
                var dist = new double[] { 0, 0, 0, 0, 0 };
                //相机矩阵
                var cameraMatrix = new double[3, 3]
                {
                { width, 0, width/2 },
                { 0, width, height/2 },
                { 0, 0, 1 }
                };

                // 基础点
                var objPts = new Point3f[]
                {
                new Point3f(0.0f, 0.0f, 0.0f),             // Nose tip
                new Point3f(0.0f, -330.0f, -65.0f),        // Chin
                new Point3f(-225.0f, 170.0f, -135.0f),     // Left eye left corner
                new Point3f(225.0f, 170.0f, -135.0f),      // Right eye right corne
                new Point3f(-150.0f, -150.0f, -125.0f),    // Left Mouth corner
                new Point3f(150.0f, -150.0f, -125.0f)      // Right mouth corner
                };


                // 人脸的位置
                var imgPts = new Point2f[]
                {
                        new Point2f(shape.GetPart(30).X,shape.GetPart(30).Y),
                        new Point2f(shape.GetPart(8).X,shape.GetPart(8).Y),
                        new Point2f(shape.GetPart(36).X,shape.GetPart(36).Y),
                        new Point2f(shape.GetPart(45).X,shape.GetPart(45).Y),
                        new Point2f(shape.GetPart(48).X,shape.GetPart(48).Y),
                        new Point2f(shape.GetPart(54).X,shape.GetPart(54).Y),
                };
                Cv2.SolvePnP(objPts, imgPts, cameraMatrix, dist, out rvec, out tvec, flags: SolvePnPFlags.Iterative);
                angleS.Add(GetEulerAngle(rvec));
            }

            return angleS;
        }



        /// <summary>
        /// 旋转向量转化为欧拉角
        /// </summary>
        /// <param name="rotation_vector"></param>
        /// <returns>(Y, X, Z)</returns>
        private static (double, double, double) GetEulerAngle(double[] rotation_vector)
        {
            Mat mat = new Mat(3, 1, MatType.CV_64FC1, rotation_vector);
            var theta = Cv2.Norm(mat, NormTypes.L2);
            var w = Math.Cos(theta / 2);
            var x = Math.Sin(theta / 2) * rotation_vector[0] / theta;
            var y = Math.Sin(theta / 2) * rotation_vector[1] / theta;
            var z = Math.Sin(theta / 2) * rotation_vector[2] / theta;

            var ysqr = y * y;

            // pitch (x-axis rotation)
            var t0 = 2.0 * (w * x + y * z);
            var t1 = 1.0 - 2.0 * (x * x + ysqr);
            var pitch = Math.Atan2(t0, t1);//反正切（给坐标轴，x，y）

            // yaw (y-axis rotation)
            var t2 = 2.0 * (w * y - z * x);
            if (t2 > 1.0) t2 = 1.0;
            if (t2 < -1.0) t2 = -1.0;
            var yaw = Math.Asin(t2); //反正弦函数

            // roll (z-axis rotation)
            var t3 = 2.0 * (w * z + x * y);
            var t4 = 1.0 - 2.0 * (ysqr + z * z);
            var roll = Math.Atan2(t3, t4);

            // 单位转换：将弧度转换为度
            var Y = (pitch / Math.PI) * 180;
            var X = (yaw / Math.PI) * 180;
            var Z = (roll / Math.PI) * 180;

            return (Y, X, Z);
        }


    }
}
