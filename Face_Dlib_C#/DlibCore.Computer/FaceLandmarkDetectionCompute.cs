using DlibDotNet;
using FaceDlib.Core.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using DlibDotNet.Extensions;
using Dlib = DlibDotNet.Dlib;
using System.Linq;
using System.IO;
using FaceDlib.Core.Models.ViewModel;

namespace DlibCore.Computer
{
    public class FaceLandmarkDetectionCompute
    {

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


        public static List<Landmark68ViewModler> GetLandmark(Bitmap bitmaps)
        {
            return GetDate(new List<Bitmap>() { bitmaps })[0];
        }


        public static List<List<Landmark68ViewModler>> GetLandmark(List<Bitmap> bitmaps)
        {
            return GetDate(bitmaps);
        }

        /// <summary>
        /// 具体计算
        /// </summary>
        /// <param name="bitmaps"></param>
        /// <returns></returns>
        private static List<List<Landmark68ViewModler>> GetDate(List<Bitmap> bitmaps)
        {
            List<List<FullObjectDetection>> Detection = new List<List<FullObjectDetection>>();


            List<List<Landmark68ViewModler>> landmark68s = new List<List<Landmark68ViewModler>>();

            
            //人脸检测器
            using (var detector = Dlib.GetFrontalFaceDetector())
            {
                foreach (var bitmap in bitmaps)
                {
                    var shapes = new List<FullObjectDetection>();
                    // 图片转换
                    using (var img = bitmap.ToArray2D<RgbPixel>())
                    {
                        //获取位置数据
                        var dets = detector.Operator(img);

                        // 循环人脸数据
                        foreach (var rect in dets)
                        {
                            // 特征点检测
                            var shape = _SP.Detect(img, rect);
                            if (shape.Parts > 2)
                            {
                                shapes.Add(shape);
                            }
                            List<Landmark68ViewModler> landmark68 = new List<Landmark68ViewModler>();
                            for (uint i = 0; i < shape.Parts; i++)
                            {
                                var item = shape.GetPart(i);
                                landmark68.Add(new Landmark68ViewModler()
                                {
                                    X = item.X,
                                    Y = item.Y,
                                });
                            }
                            landmark68s.Add(landmark68);

                        }
                    }
                    Detection.Add(shapes);
                }
            }
            return landmark68s;
        }

        
        /// <summary>
        /// 根据图片位置和图片获取68个点
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="dets"></param>
        public static List<(List<Landmark68ViewModler>, FullObjectDetection)> DetsTolandmark68(Bitmap bitmap, DlibDotNet.Rectangle[] dets)
        {
            //var shapes = new List<FullObjectDetection>();
            List<(List<Landmark68ViewModler>, FullObjectDetection)> landmark68s = new List<(List<Landmark68ViewModler>, FullObjectDetection)>();
            
            //获取位置数据
            using (var img = bitmap.ToArray2D<RgbPixel>())
            {
                foreach (var rect in dets)
                {
                    // 特征点检测
                    var shape = _SP.Detect(img, rect);
                    List<Landmark68ViewModler> landmark68 = new List<Landmark68ViewModler>();
                    for (uint i = 0; i < shape.Parts; i++)
                    {
                        var item = shape.GetPart(i);
                        landmark68.Add(new Landmark68ViewModler()
                        {
                            X = item.X,
                            Y = item.Y,
                        });
                    }
                    landmark68s.Add((landmark68,shape));
                }
            }
            return landmark68s;
        }


        /// <summary>
        /// 连线
        /// </summary>
        /// <param name="shapes"></param>
        /// <returns></returns>
        public static ImageWindow.OverlayLine[] Getlines(List<FullObjectDetection> shapes)
        {
            //这个是用来连线的
            ImageWindow.OverlayLine[] lines = null;
            if (shapes.Any())
            {
                //就是这个
                lines = Dlib.RenderFaceDetections(shapes);
            }
            return lines;
        }
    }
}
