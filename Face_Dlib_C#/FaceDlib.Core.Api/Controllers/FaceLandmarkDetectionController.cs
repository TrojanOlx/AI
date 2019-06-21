using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DlibDotNet;
using DlibDotNet.Extensions;
using Dlib = DlibDotNet.Dlib;
using System.Linq;
using FaceDlib.Core.Common;
using DlibCore.Computer;

namespace FaceDlib.Core.Api.Controllers
{
    public class FaceLandmarkDetectionController : Controller
    {
        private ShapePredictor sp = null;

        /// <summary>
        /// 获取人脸坐标（68个点）
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public  ActionResult GetFaceLink(string image)
        {
            //var lines = await GetLinesAsync(new List<IFormFile>() { file });
            //if (lines.Count <= 0)
            //{
            //    return Ok(null);
            //}
            (Bitmap, string) bitmap = (null, null);
            // 获取图片文件
            //bitmap = model.image_type.ToUpper() == "BASE64" ? FileCommon.Base64ToBitmap(model.image) : FileCommon.UrlToBitmap(model.image);
            bitmap= FileCommon.UrlToBitmap(image);
            var date = FaceLandmarkDetectionCompute.GetLandmark(bitmap.Item1);

            //var hh= FaceLandmarkDetectionCompute.Getlines(date);
            return Ok(date);
        }

        /// <summary>
        /// 获取人脸坐标（68个点）
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetFaceLinks(List<IFormFile> files)
        {
            // 获取根目录
            return Ok(await GetLinesAsync(files));
        }


        public async Task<List<List<FullObjectDetection>>> GetLinesAsync(List<IFormFile> files)
        {
            List<List<FullObjectDetection>> Detection = new List<List<FullObjectDetection>>();
            // 模型文件，可替换
            var bitmaps =FileCommon.FileToBitmap(files);
            foreach (var bitmap in bitmaps)
            {
                Detection.Add(Face(bitmap.Item1));
            }
            return Detection;
        }

        /// <summary>
        /// 具体计算
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public List<FullObjectDetection> Face(Bitmap bitmap)
        {
            // 加载模型文件
            if (sp == null)
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                sp = ShapePredictor.Deserialize(basePath + "ShapeModel/shape_predictor_68_face_landmarks.dat");
            }

            //var link = new ImageWindow.OverlayLine[0];
            var shapes = new List<FullObjectDetection>();
            using (var detector = Dlib.GetFrontalFaceDetector())
            {
                using (var img = bitmap.ToArray2D<RgbPixel>())
                {
                    var dets = detector.Operator(img);
                    
                    foreach (var rect in dets)
                    {
                        var shape = sp.Detect(img, rect);
                        if (shape.Parts > 2)
                        {
                            shapes.Add(shape);
                        }
                    }
                    //if (shapes.Any())
                    //{
                    //    //就是这个
                    //    var lines = Dlib.RenderFaceDetections(shapes);
                    //    link = lines;
                    //}
                }

            }
            return shapes;
        }
    }
}
