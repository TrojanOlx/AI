using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DlibDotNet;
using OpenCvSharp;

namespace WebcamFacePose
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                //var cap = new VideoCapture(0);
                //var cap = new VideoCapture("https://js.live-play.acgvideo.com/live-js/890069/live_30947419_1716018.flv?wsSecret=2cee8a379a871fa8dbf714ba9d16e8a4&wsTime=1548240723&trid=4f64a0ae5e2444938cfdd109a54c6e1c&sig=no&platform=web&pSession=yR3bsQk1-SCY4-4QGi-K7EG-AsbTiwbX7tZF");
                var cap = new VideoCapture(0);
                if (!cap.IsOpened())
                {
                    Console.WriteLine("Unable to connect to camera");
                    return;
                }

                using (var win = new ImageWindow())
                {
                    // Load face detection and pose estimation models.
                    using (var detector = Dlib.GetFrontalFaceDetector())
                    using (var poseModel = ShapePredictor.Deserialize("shape_predictor_68_face_landmarks.dat"))
                    {
                        //抓取和处理帧，直到用户关闭主窗口。
                        while (!win.IsClosed())
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
                                    Console.WriteLine(faces[i].Left);
                                    shapes.Add(det);
                                }

                                //在屏幕上显示
                                win.ClearOverlay();
                                win.SetImage(cimg);
                                var lines = Dlib.RenderFaceDetections(shapes);
                                win.AddOverlay(faces, new RgbPixel { Red = 255 });
                                win.AddOverlay(lines);
                                foreach (var line in lines)
                                    line.Dispose();
                            }
                        }
                    }
                }
            }
            //catch (serialization_error&e)
            //{
            //    cout << "You need dlib's default face landmarking model file to run this example." << endl;
            //    cout << "You can get it from the following URL: " << endl;
            //    cout << "   http://dlib.net/files/shape_predictor_68_face_landmarks.dat.bz2" << endl;
            //    cout << endl << e.what() << endl;
            //}
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}
