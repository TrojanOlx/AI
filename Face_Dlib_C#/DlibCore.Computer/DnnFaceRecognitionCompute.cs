using FaceDlib.Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DlibDotNet;
using DlibDotNet.Extensions;
using Dlib = DlibDotNet.Dlib;
using DlibDotNet.Dnn;
using System.Drawing;
using System.Linq;
using Rectangle = DlibDotNet.Rectangle;

namespace DlibCore.Computer
{
    public class DnnFaceRecognitionCompute
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
                    _sp = ShapePredictor.Deserialize(path);
                }
                return _sp;
            }
        }

        /// <summary>
        /// 加载负责人脸识别的DNN。
        /// </summary>
        public static LossMetric _NET
        {
            get
            {
                if (_net == null)
                {
                    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ShapeModel", "dlib_face_recognition_resnet_model_v1.dat");
                    _net = LossMetric.Deserialize(path);
                }
                return _net;
            }
        }



        /// <summary>
        /// 人脸对比
        /// </summary>
        /// <param name="bitmap1"></param>
        /// <param name="bitmap2"></param>
        /// <returns></returns>
        public static (float, List<Rectangle[]>) GetOutputLabels(Bitmap bitmap1, Bitmap bitmap2)
        {
            var data1 = GetData(new List<Bitmap>() { bitmap1 }, true);
            var data2 = GetData(new List<Bitmap>() { bitmap2 }, true);


            List<Rectangle[]> rectangles = new List<Rectangle[]>();

            if (data1[0].Item1[0] == null)
            {
                return (-1, null);
            }

            if (data2[0].Item1[0] == null)
            {
                return (-2, null);
            }

            // 欧里几德距离
            var diff = data1[0].Item1[0] - data2[0].Item1[0];

            rectangles.Add(data1[0].Item2);
            rectangles.Add(data2[0].Item2);
            return (Dlib.Length(diff), rectangles);
        }

        /// <summary>
        /// 人脸查找
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="bitmaps"></param>
        /// <returns></returns>
        public static List<(float, Rectangle[])> GetOutputLabels(Bitmap bitmap, List<Bitmap> bitmaps, bool isAFace = false)
        {
            List<(float, Rectangle[])> fNums = new List<(float, Rectangle[])>();
            var data1 = GetData(new List<Bitmap>() { bitmap },isAFace);
            var data2 = GetData(bitmaps,true);


            if (data1[0].Item1[0] == null)
            {
                return null;
            }

            foreach (var item in data2)
            {
                if (item.Item1[0] == null)
                {
                    fNums.Add((99.99f, item.Item2));
                }
                else
                {
                    // 欧里几德距离
                    var diff = data1[0].Item1[0] - data2[0].Item1[0];
                    fNums.Add((Dlib.Length(diff), item.Item2));
                }

            }
            return fNums;
        }




        private static List<(OutputLabels<Matrix<float>>, Rectangle[])> GetData(List<Bitmap> bitmaps, bool isAFace = false)
        {

            var datas = new List<(OutputLabels<Matrix<float>>, Rectangle[])>();
            try
            {
                foreach (var bitmap in bitmaps)
                {
                    var faces = new List<Matrix<RgbPixel>>();
                    var dets = new Rectangle[0];
                    //在图像中寻找人脸我们需要一个人脸检测器:
                    using (var detector = Dlib.GetFrontalFaceDetector())
                    {
                        using (var img = bitmap.ToMatrix<RgbPixel>())
                        {
                            // 人脸 面积从大到小排序
                            dets = detector.Operator(img).OrderByDescending(x => x.Area).ToArray();
                            // 是否只检测面积最大的人脸
                            if (isAFace)
                            {
                                var shape = _SP.Detect(img, dets[0]);
                                var faceChipDetail = Dlib.GetFaceChipDetails(shape, 150, 0.25);
                                var faceChip = Dlib.ExtractImageChip<RgbPixel>(img, faceChipDetail);
                                faces.Add(faceChip);
                            }
                            else
                            {
                                foreach (var face in dets)
                                {
                                    var shape = _SP.Detect(img, face);
                                    var faceChipDetail = Dlib.GetFaceChipDetails(shape, 150, 0.25);
                                    var faceChip = Dlib.ExtractImageChip<RgbPixel>(img, faceChipDetail);
                                    faces.Add(faceChip);
                                }
                            }
                            if (!faces.Any())
                            {
                                datas.Add((null, null));
                            }
                            else
                            {
                                //此调用要求DNN将每个人脸图像转换为128D矢量。
                                //在这个128D的矢量空间中，来自同一个人的图像会彼此接近
                                //但是来自不同人的载体将会非常不同。所以我们可以用这些向量
                                //辨别一对图片是来自同一个人还是不同的人。
                                datas.Add((_NET.Operator(faces), dets));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                LogHelperNLog.Error(ex);
            }
            return datas;
        }
    }
}
