using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DlibDotNet;
using DlibDotNet.Extensions;
using Dlib = DlibDotNet.Dlib;
using Rectangle = DlibDotNet.Rectangle;
using FaceDlib.Core.Common;
using Microsoft.AspNetCore.Hosting;
using FaceDlib.Core.Common.DBHelper;
using System.Drawing.Imaging;
using OpenCvSharp;
using DlibCore.Computer;
using NNSharp.IO;
using NNSharp.Models;

namespace FaceDlib.Core.Api.Controllers
{
    public class FaceController : Controller
    {


        /// <summary>
        /// 获取人脸坐标(单个文件)
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetFaceLocation(IFormFile files)
        {
            var date = SqlDapperHelper.ExecuteReaderReturnList<object>("SELECT * FROM \"user\"");

            var rectangle = await GetRectanglesAsync(new List<IFormFile>() { files });
            if (rectangle.Count <= 0)
            {
                return Ok(null);
            }
            return Ok(rectangle[0]);
        }


        public ActionResult GetResult()
        {

            var img = Cv2.ImRead("");

            CascadeClassifier face_classifier = new CascadeClassifier("");

            img = img.CvtColor(ColorConversionCodes.BGR2GRAY);

            var faces = face_classifier.DetectMultiScale(img,scaleFactor:1.2,minNeighbors:3,minSize:new OpenCvSharp.Size(140,140));

            //var gender_classifier = 

            var reder = new ReaderKerasModel("");
            //SequentalModel model = reader.GetSequentialExecutor();


            return Ok();
        }

        /// <summary>
        /// 获取人脸坐标（多个文件）
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetFaceLocations(List<IFormFile> files)
        {
            //var rectangles = await GetRectanglesAsync(files);


            var bitmaps = await FileToBitmapAsync(files);

            Test(bitmaps[0]);

            return Ok();
        }

        private static ShapePredictor _sp = null;


        public static ShapePredictor _SP
        {
            get
            {
                if (_sp == null)
                {
                    try
                    {
                        var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ShapeModel", "shape_predictor_68_face_landmarks.dat");
                        //Console.WriteLine(basePath);
                        // 模型文件，可替换
                        _sp = ShapePredictor.Deserialize(basePath);
                        return _sp;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);
                    }
                    // 加载模型文件
                }
                return _sp;
            }
        }


        private void Test(Bitmap bitmap)
        {
            var rvec = new double[] { 0, 0, 0 };
            var tvec = new double[] { 0, 0, 0 };


            //FullObjectDetection shape = null;
            using (var detector = Dlib.GetFrontalFaceDetector())
            using (var img = bitmap.ToArray2D<RgbPixel>())
            {
                //获取位置数据
                var dets = detector.Operator(img);

                // 循环人脸数据
                foreach (var rect in dets)
                {
                    // 特征点检测
                    var shape = _SP.Detect(img, rect);



                    var focal_length = (double)bitmap.Width; // 图片的宽度
                    var center = ((double)bitmap.Width / 2.0, (double)bitmap.Height / 2.0); //图片的宽度/2，图片的高度/2 中心点



                    var p1 = shape.GetPart(0);
                    var p2 = shape.GetPart(16);

                    var dx = (double)(p2.X - p1.X);
                    var dy = (double)(p2.Y - p1.Y);
                    var num = (dy / dx);
                    var aa = Math.Tan(num);
                    var bb = aa * 180;
                    var cc = bb / Math.PI;
                    var nA = (Math.Atan(dy / dx)) * 180 / Math.PI;




                    // 需要进行改变
                    var cameraMatrix = new double[3, 3]
                    {
                        { focal_length, 0, center.Item1 },
                        { 0, focal_length, center.Item2 },
                        { 0, 0, 1 }
                    };


                    var dist = new double[] { 0, 0, 0, 0, 0 };



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


                    // 这个参数
                    double[,] jacobian = new double[4, 1];

                    // 人脸的位置
                    var imgPts = new Point2f[]
                    {
                        new Point2f(shape.GetPart(30).X,shape.GetPart(30).Y),   // Nose tip
                        new Point2f(shape.GetPart(8).X,shape.GetPart(8).Y),     // Chin
                        new Point2f(shape.GetPart(36).X,shape.GetPart(36).Y),   // Left eye left corner
                        new Point2f(shape.GetPart(45).X,shape.GetPart(45).Y),   // Right eye right corne 
                        new Point2f(shape.GetPart(48).X,shape.GetPart(48).Y),   // Left Mouth corner
                        new Point2f(shape.GetPart(54).X,shape.GetPart(54).Y),   // Right mouth corner
                    };


                    //var imgPts = new Point2f[]
                    //{
                    //    new Point2f(359, 391),     // Nose tip
                    //    new Point2f(399, 561),     // Chin
                    //    new Point2f(337, 297),     // Left eye left corner
                    //    new Point2f(513, 301),     // Right eye right corne 
                    //    new Point2f(345, 465),     // Left Mouth corner
                    //    new Point2f(453, 469)      // Right mouth corner
                    //};

                    Cv2.SolvePnP(objPts, imgPts, cameraMatrix, dist, out rvec, out tvec, flags: SolvePnPFlags.Iterative);

                    GetEulerAngle(rvec);



                    var arr = new List<Point3f>() { new Point3f(0.0f, 0.0f, 1000.0f) };


                    Cv2.ProjectPoints(arr, rvec, tvec, cameraMatrix, dist, out imgPts, out jacobian);




                    Cv2.Rodrigues(rvec, out cameraMatrix);
                   





                    

                    var im = Cv2.ImRead("headPose.jpg");
                    Cv2.Line(im, (int)shape.GetPart(30).X, (int)shape.GetPart(30).Y, (int)imgPts[0].X, (int)imgPts[0].Y, Scalar.Blue, (int)LineTypes.Link8);

                    Cv2.ImShow("output", im);
                    Cv2.WaitKey(0);
                    //Cv2.Line(im, 0, 0, 0, 0, Scalar.All(255));

                }
            }

        }

        /// 旋转向量转化为欧拉角
        public void GetEulerAngle(double[] rotation_vector)
        {
            //var X = rotation_vector[0];
            //var Y = rotation_vector[1];
            //var Z = rotation_vector[2];

            //var x = Math.Sin(Y / 2)*Math.Sin(Z / 2)*Math.Cos(X / 2) + Math.Cos(Y / 2)*Math.Cos(Z / 2)*Math.Sin(X / 2);
            //var y = Math.Sin(Y / 2)*Math.Cos(Z / 2)*Math.Cos(X / 2) + Math.Cos(Y / 2)*Math.Sin(Z / 2)*Math.Sin(X / 2);
            //var z = Math.Cos(Y / 2)*Math.Sin(Z / 2)*Math.Cos(X / 2) - Math.Sin(Y / 2)*Math.Cos(Z / 2)*Math.Sin(X / 2);
            //var w = Math.Cos(Y / 2)*Math.Cos(Z / 2)*Math.Cos(X / 2) - Math.Sin(Y / 2)*Math.Sin(Z / 2)*Math.Sin(X / 2);


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

        }




        /// <summary>
        /// 逻辑处理
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<List<Rectangle[]>> GetRectanglesAsync(List<IFormFile> files)
        {

            var filePath = Path.GetTempFileName();
            List<Rectangle[]> rectangles = new List<Rectangle[]>();
            var bitmaps = await FileToBitmapAsync(files);
            try
            {
                foreach (var bitmap in bitmaps)
                {
                    rectangles.Add(Face(bitmap));
                }
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex, "FaceDetectionController.GetRectanglesAsync:人脸获取坐标出错");
            }
            return rectangles;
        }


        /// <summary>
        /// 文件转化为图片（多个文件）
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static async Task<List<Bitmap>> FileToBitmapAsync(List<IFormFile> files)
        {
            List<Bitmap> bitmaps = new List<Bitmap>();
            var filePath = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var file in files)
            {
                try
                {
                    if (file.Length > 0)
                    {
                        var fileExtension = Path.GetExtension(file.FileName);
                        filePath += file.FileName;
                        using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
                        {
                            await file.CopyToAsync(stream);
                            //stream.Flush();
                            Bitmap bitmap = new Bitmap(stream);
                            if (fileExtension == ".png")
                            {
                                //Bitmap bitmap24 = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format24bppRgb);
                                //Graphics g = Graphics.FromImage(bitmap24);
                                //g.DrawImageUnscaled(bitmap, 0, 0);
                                //bitmap = bitmap24;
                                bitmap = bitmap.Clone(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format24bppRgb);
                            }
                            bitmaps.Add(bitmap);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelperNLog.Error(ex, "FileCommon.FileToBitmapAsync 文件转换图片出错");
                }
            }
            return bitmaps;
        }
        /// <summary>
        /// 具体计算
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public Rectangle[] Face(Bitmap bitmap)
        {
            var dets = new Rectangle[0];
            using (var detector = Dlib.GetFrontalFaceDetector())
            //using (var img = Dlib.LoadImage<RgbPixel>("png.png"))
            using (var img = bitmap.ToArray2D<RgbPixel>())
            {

                dets = detector.Operator(img);
            }
            return dets;
        }
    }
}
