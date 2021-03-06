﻿using DlibDotNet;
using DlibDotNet.Dnn;
using FaceDlib.Core.Models;
using FaceDlib.Core.Models.Common;
using Microsoft.AspNetCore.Mvc;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace FaceDlib.Core.Api.Controllers
{
    public class FaceLoginController : Controller
    {
        private static ShapePredictor _sp = null;
        private static LossMetric _net = null;

        /// <summary>
        /// 使用一个人脸标记模型来将人脸与标准姿势对齐
        /// </summary>
        public static ShapePredictor _SP
        {
            get
            {
                if (_sp == null)
                {
                    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ShapeModel", "shape_predictor_5_face_landmarks.dat");
                    ProxyDeserialize deserialize = new ProxyDeserialize(path);
                    _sp = ShapePredictor.Deserialize(deserialize);
                }
                // 深度复制一个对象 返还给用户使用

                return _sp;
            }
        }


        private static ProxyDeserialize _deserialize = null;
        /// <summary>
        /// 加载负责人脸识别的DNN。
        /// </summary>
        public static LossMetric _NET
        {
            get
            {
                if (_deserialize == null)
                {
                    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ShapeModel", "dlib_face_recognition_resnet_model_v1.dat");
                    _deserialize = new ProxyDeserialize(path);
                }
                _net = LossMetric.Deserialize(_deserialize);
                // 深度复制一个对象 返还给用户使用
                return _net;
            }
        }



        public static T Clone<T>(T RealObject) where T : class
        {

            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, RealObject);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as T;
            }

        }



        public class InputFaceModel
        {
            public string rmtp_url { get; set; }
            /// <summary>
            /// 应用ID
            /// </summary>
            public string user_name { get; set; }

        }

        [HttpPost]
        public async Task<ActionResult> InputFaceData([FromBody]InputFaceModel model)
        {
            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null
            };

            VideoCapture cap = null;
            try
            {
                if (model.rmtp_url == "0")
                {
                    cap = new VideoCapture(0);
                }
                else
                {
                    cap = new VideoCapture(model.rmtp_url);
                }

                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FaceImages", model.user_name);
                if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

                int i = 0;
                int num = 0;
                ////using (var win1 = new ImageWindow())
                //using (var win = new ImageWindow())
                //{
                // Load face detection and pose estimation models.
                using (var detector = Dlib.GetFrontalFaceDetector())
                using (var sp = ShapePredictor.Deserialize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ShapeModel", "shape_predictor_5_face_landmarks.dat")))
                {
                    //抓取和处理帧，直到用户关闭主窗口。
                    while (/*!win.IsClosed()*/true)
                    {
                        try
                        {
                            // Grab a frame
                            var temp = new Mat();
                            if (!cap.Read(temp))
                            {
                                break;
                            }

                            //把OpenCV的Mat变成dlib可以处理的东西。注意
                            //包装Mat对象，它不复制任何东西。所以cimg只对as有效
                            //只要温度是有效的。也不要做任何可能导致它的临时工作
                            //重新分配存储图像的内存，因为这将使cimg
                            //包含悬空指针。这基本上意味着您不应该修改temp
                            //使用cimg时。
                            var array = new byte[temp.Width * temp.Height * temp.ElemSize()];
                            Marshal.Copy(temp.Data, array, 0, array.Length);
                            using (var cimg = Dlib.LoadImageData<RgbPixel>(array, (uint)temp.Height, (uint)temp.Width, (uint)(temp.Width * temp.ElemSize())))
                            {
                                // Detect faces 
                                var faces = detector.Operator(cimg);

                                if (i % 7 == 0)
                                {
                                    foreach (var face in faces)
                                    {
                                        var shape = sp.Detect(cimg, face);
                                        var faceChipDetail = Dlib.GetFaceChipDetails(shape, 150, 0.25);
                                        var faceChip = Dlib.ExtractImageChip<RgbPixel>(cimg, faceChipDetail);
                                        //win1.ClearOverlay();
                                        //win1.SetImage(faceChip);
                                        Dlib.SaveJpeg(faceChip, Path.Combine(filePath, DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff") + ".jpg"));
                                        num++;
                                    }
                                }

                                i++;

                                if (num >= 7)
                                {
                                    break;
                                }


                                //在屏幕上显示
                                //win.ClearOverlay();
                                //win.SetImage(cimg);
                                //win.AddOverlay(faces, new RgbPixel { Red = 72, Green = 118, Blue = 255 });
                            }

                        }
                        catch (Exception ex)
                        {
                            request.Message = ex.ToString();
                            break;
                        }
                    }
                }
                //}
                request.Enum = RequestEnum.Succeed;
            }
            catch (Exception ex)
            {
                request.Message = ex.ToString();
            }
            finally
            {
                if (cap != null)
                    cap.Dispose();
            }


            return Ok(request);

        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody]InputFaceModel model)
        {
            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null
            };
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FaceImages", model.user_name);
            if (!Directory.Exists(filePath))
            {
                request.Enum = RequestEnum.Failed;
                Console.WriteLine(request.Message);
                Thread.Sleep(5000);
                return Ok(request);
            }
            FaceContrast faceContrast = new FaceContrast(filePath);

            VideoCapture cap = null;
            try
            {
                if (model.rmtp_url == "0")
                {
                    cap = new VideoCapture(0);
                }
                else
                {
                    cap = new VideoCapture(model.rmtp_url);
                }


                var flag = false;
                var faceFlag = false;

                var bioFlag = false;

                QueueFixedLength<double> leftEarQueue = new QueueFixedLength<double>(10);
                QueueFixedLength<double> rightEarQueue = new QueueFixedLength<double>(10);
                QueueFixedLength<double> mouthQueue = new QueueFixedLength<double>(20);
                bool leftEarFlag = false;
                bool rightEarFlag = false;
                bool mouthFlag = false;
                using (var sp = ShapePredictor.Deserialize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ShapeModel", "shape_predictor_5_face_landmarks.dat")))
                using (var win = new ImageWindow())
                {
                    // Load face detection and pose estimation models.
                    using (var detector = Dlib.GetFrontalFaceDetector())
                    using (var net = LossMetric.Deserialize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ShapeModel", "dlib_face_recognition_resnet_model_v1.dat")))
                    using (var poseModel = ShapePredictor.Deserialize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ShapeModel", "shape_predictor_68_face_landmarks.dat")))
                    {

                        var ti = true;

                        System.Timers.Timer t = new System.Timers.Timer(30000);
                        t.Elapsed += new System.Timers.ElapsedEventHandler((object source, System.Timers.ElapsedEventArgs e) =>
                        {
                            ti = false;
                        });

                        t.AutoReset = false;
                        t.Enabled = true;

                        //抓取和处理帧，直到用户关闭主窗口。
                        while (/*!win.IsClosed() &&*/ ti)
                        {
                            try
                            {
                                // Grab a frame
                                var temp = new Mat();
                                if (!cap.Read(temp))
                                {
                                    break;
                                }

                                //把OpenCV的Mat变成dlib可以处理的东西。注意
                                //包装Mat对象，它不复制任何东西。所以cimg只对as有效
                                //只要温度是有效的。也不要做任何可能导致它的临时工作
                                //重新分配存储图像的内存，因为这将使cimg
                                //包含悬空指针。这基本上意味着您不应该修改temp
                                //使用cimg时。
                                var array = new byte[temp.Width * temp.Height * temp.ElemSize()];
                                Marshal.Copy(temp.Data, array, 0, array.Length);
                                using (var cimg = Dlib.LoadImageData<RgbPixel>(array, (uint)temp.Height, (uint)temp.Width, (uint)(temp.Width * temp.ElemSize())))
                                {
                                    // Detect faces 
                                    var faces = detector.Operator(cimg);
                                    // Find the pose of each face.
                                    var shapes = new List<FullObjectDetection>();
                                    for (var i = 0; i < faces.Length; ++i)
                                    {
                                        var det = poseModel.Detect(cimg, faces[i]);
                                        shapes.Add(det);
                                    }

                                    if (shapes.Count > 0)
                                    {
                                        // 活体检测

                                        if (!bioFlag)
                                        {
                                            bioFlag = BioAssay(shapes[0], ref leftEarQueue, ref rightEarQueue, ref mouthQueue, ref leftEarFlag, ref rightEarFlag, ref mouthFlag);
                                        }
                                    }


                                    if (!faceFlag)
                                    {
                                        foreach (var face in faces)
                                        {
                                            var shape = sp.Detect(cimg, face);
                                            var faceChipDetail = Dlib.GetFaceChipDetails(shape, 150, 0.25);
                                            Matrix<RgbPixel> rgbPixels = new Matrix<RgbPixel>(cimg);
                                            var faceChip = Dlib.ExtractImageChip<RgbPixel>(rgbPixels, faceChipDetail);
                                            var faceDescriptors = net.Operator(faceChip);
                                            faceFlag = faceContrast.Contrast(faceDescriptors);
                                        }
                                    }
                                    Console.WriteLine(model.user_name + ":" + faceFlag);
                                    if (bioFlag && faceFlag)
                                    {
                                        flag = bioFlag && faceFlag;
                                        if (flag)
                                        {
                                            break;
                                        }
                                    }

                                    //在屏幕上显示
                                    win.ClearOverlay();
                                    win.SetImage(cimg);
                                    var lines = Dlib.RenderFaceDetections(shapes);
                                    win.AddOverlay(faces, new RgbPixel { Red = 72, Green = 118, Blue = 255 });
                                    win.AddOverlay(lines);
                                    foreach (var line in lines)
                                        line.Dispose();
                                }

                            }
                            catch (Exception ex)
                            {
                                request.Message = ex.ToString();
                                break;
                            }
                        }
                    }
                }

                if (flag)
                {
                    request.Enum = RequestEnum.Succeed;
                }
                else
                {
                    request.Enum = RequestEnum.Failed;
                }
            }
            catch (Exception ex)
            {
                request.Message = ex.ToString();
            }
            finally
            {
                if (cap != null)
                    cap.Dispose();
            }
            Console.WriteLine(request.Message);
            return Ok(request);
        }

        public ActionResult GetSettings()
        {
            RequestFaceModel request = new RequestFaceModel()
            {
                Status = 500,
                Message = null
            };
            var setting = new
            {
                Version = CoinAppSettings.Instance.AppSettings.Version,
                RtmpServer = CoinAppSettings.Instance.AppSettings.RtmpServer
            };
            request.Enum = RequestEnum.Succeed;
            request.Data = setting;
            return Ok(request);
        }


        public class FaceContrast
        {

            OutputLabels<Matrix<float>> _faceDescriptors = null;
            public FaceContrast(string path)
            {
                var files = Directory.GetFiles(path, "*.jpg");

                if (files.Length > 0)
                {
                    using (var net = LossMetric.Deserialize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ShapeModel", "dlib_face_recognition_resnet_model_v1.dat")))
                    {
                        List<Matrix<RgbPixel>> facesList = new List<Matrix<RgbPixel>>();
                        foreach (var item in files)
                        {
                            facesList.Add(Dlib.LoadImageAsMatrix<RgbPixel>(item));
                        }
                        _faceDescriptors = net.Operator(facesList);
                    }
                }
            }


            public bool Contrast(OutputLabels<Matrix<float>> faceDescriptors)
            {
                if (_faceDescriptors == null || _faceDescriptors.Count <= 0)
                {
                    return false;
                }

                foreach (var item in faceDescriptors)
                {
                    foreach (var item1 in _faceDescriptors)
                    {
                        var diff = item - item1;
                        if (Dlib.Length(diff) < 0.4)
                        {
                            return true;

                        }
                    }
                }
                return false;
            }

        }

        // 活体识别
        public bool BioAssay(FullObjectDetection face, ref QueueFixedLength<double> leftEarQueue, ref QueueFixedLength<double> rightEarQueue, ref QueueFixedLength<double> mouthQueue, ref bool leftEarFlag, ref bool rightEarFlag, ref bool mouthFlag)
        {

            if (!leftEarFlag)
            {
                double leftear = getEar(face, EarEnum.Left);
                // 左眼添加到队列
                leftEarQueue.Enqueue(leftear);
                var leftEarNum = GetqueueMaxAndMin(leftEarQueue);
                if (leftEarNum.Item1 - leftEarNum.Item2 > 0.1)
                {
                    leftEarFlag = true;
                }
            }


            if (!rightEarFlag)
            {
                double rightear = getEar(face, EarEnum.Right);
                // 右眼添加到队列
                rightEarQueue.Enqueue(rightear);
                var rightEarNum = GetqueueMaxAndMin(rightEarQueue);
                if (rightEarNum.Item1 - rightEarNum.Item2 > 0.1)
                {
                    rightEarFlag = true;
                }
            }


            if (!mouthFlag)
            {
                double mouth = getEar(face, EarEnum.Mouth);
                // 嘴巴添加到队列
                mouthQueue.Enqueue(mouth);
                var mouthEarNum = GetqueueMaxAndMin(mouthQueue);
                if (mouthEarNum.Item1 - mouthEarNum.Item2 > 0.08)
                {
                    mouthFlag = true;
                }
            }

            Console.WriteLine(leftEarFlag + ":" + rightEarFlag + ":" + mouthFlag);
            if (leftEarFlag && rightEarFlag && mouthFlag)
            {
                return true;
            }
            return false;
        }



        // 对比的枚举
        public enum EarEnum
        {
            Left,
            Right,
            Mouth
        }

        uint[] leftEarPoint = new uint[] { 36, 37, 38, 39, 40, 41 };
        uint[] rightEarPoint = new uint[] { 42, 43, 44, 45, 46, 47 };
        uint[] mouthPoint = new uint[] { 48, 61, 63, 54, 65, 67 };

        // 对比
        public double getEar(FullObjectDetection face, EarEnum earEnum)
        {
            List<DlibDotNet.Point> points = new List<DlibDotNet.Point>();

            if (earEnum == EarEnum.Left)
            {
                foreach (var item in leftEarPoint)
                {
                    points.Add(face.GetPart(item));
                }
            }
            else if (earEnum == EarEnum.Right)
            {
                foreach (var item in rightEarPoint)
                {
                    points.Add(face.GetPart(item));
                }
            }
            else
            {
                foreach (var item in mouthPoint)
                {
                    points.Add(face.GetPart(item));
                }
            }

            return getEarNum(points);

        }

        public double getEarNum(List<DlibDotNet.Point> points)
        {
            double p1x = Math.Pow(points[0].X - points[3].X, 2);
            double p1y = Math.Pow(points[0].Y - points[3].Y, 2);
            double p1num = Math.Sqrt(p1x + p1y);


            double p2x = Math.Pow(points[1].X - points[5].X, 2);
            double p2y = Math.Pow(points[1].Y - points[5].Y, 2);
            double p2num = Math.Sqrt(p2x + p2y);


            double p3x = Math.Pow(points[2].X - points[4].X, 2);
            double p3y = Math.Pow(points[2].Y - points[4].Y, 2);
            double p3num = Math.Sqrt(p3x + p3y);

            var num = (p2num + p3num) / (2 * p1num);

            return num;

        }


        // 固定长度的队列
        public class QueueFixedLength<T> : Queue<T>
        {
            public QueueFixedLength(int length)
            {
                this.fixedLength = length;
            }

            public QueueFixedLength() : this(10)
            {
            }

            private int fixedLength = 10;
            public int FixedLength
            {
                get
                {
                    return fixedLength;
                }
                set
                {
                    fixedLength = value;
                }
            }


            public new void Enqueue(T obj)
            {
                if (this.Count > 10)
                {
                    base.Dequeue();
                }
                base.Enqueue(obj);
            }
        }

        public (double, double) GetqueueMaxAndMin(QueueFixedLength<double> queue)
        {
            var _queueList = queue.ToArray();
            double max = int.MinValue;
            double min = int.MaxValue;
            foreach (var item in _queueList)
            {
                if (max < item)
                {
                    max = item;
                }
                if (min > item)
                {
                    min = item;
                }
            }

            return (max, min);
        }
    }
}
