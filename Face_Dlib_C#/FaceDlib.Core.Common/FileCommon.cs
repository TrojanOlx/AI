using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FaceDlib.Core.Common
{
    public class FileCommon
    {
        /// <summary>
        /// 图片后缀
        /// </summary>
        public static List<string> IMAGE_Extension = new List<string>() { ".jpg", ".jpeg", ".png" };

        /// <summary>
        /// 文件转化为图片（多个文件）
        /// </summary>
        /// <param name="files"></param>
        /// <returns>图片,base64,图片名</returns>
        public static List<(Bitmap, string, string)> FileToBitmap(List<IFormFile> files)
        {
            List<(Bitmap, string, string)> bitmaps = new List<(Bitmap, string, string)>();
            // 缓存路径
            var filePath = string.Format(@"{0}{1}/", AppDomain.CurrentDomain.BaseDirectory, "Temp");
            foreach (var file in files)
            {
                try
                {
                    if (file.Length > 0)
                    {

                        // 后缀名
                        var fileExtension = Path.GetExtension(file.FileName).ToLower();
                        // 自动创建文件夹
                        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
                        if (IMAGE_Extension.Contains(fileExtension))
                        {
                            using (var stream = file.OpenReadStream())
                            {

                                // 将文件转化为字节数组
                                byte[] arr = new byte[stream.Length];
                                stream.Read(arr, 0, (int)stream.Length);
                                //保存
                                //await file.CopyToAsync(stream);
                                Bitmap bitmap = new Bitmap(stream);
                                // 将png 转化为jpg,(PixelFormat.Format32bppRgb to PixelFormat.Format24bppRgb)
                                if (fileExtension == ".png")
                                {
                                    bitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format24bppRgb);
                                }
                                // 转化为BASE64
                                var fileName = Guid.NewGuid().ToString("N") + fileExtension;

                                //var img = Base64ToBitmap(Convert.ToBase64String(arr));

                                bitmaps.Add((bitmap, Convert.ToBase64String(arr), fileName));
                            }
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
        /// 文件转化为图片（单个文件）
        /// </summary>
        /// <param name="files"></param>
        /// <returns>图片,base64,图片名</returns>
        public static (Bitmap, string, string) FileToBitmap(IFormFile file)
        {
            var bitmap = FileToBitmap(new List<IFormFile>() { file });
            if (bitmap.Count <= 0)
            {
                return (null, null, null);
            }
            return bitmap[0];
        }


        /// <summary>
        /// 保存图片到本地
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="fileName"></param>
        public static string SaveBitmap(Bitmap bitmap, string fileName)
        {
            //var fileUrl = @"FaceImages/" + DateTime.UtcNow.ToString("yyyy-MM-dd/");
            var fileUrl = Path.Combine("FaceImages", DateTime.UtcNow.ToString("yyyy-MM-dd"));
            //var filePath = string.Format(@"{0}{1}/", AppDomain.CurrentDomain.BaseDirectory, fileUrl);
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileUrl);
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
            var str = Path.Combine(filePath, fileName);
            // 重新画一张图，避免上传错误
            Bitmap bm = new Bitmap(bitmap.Width, bitmap.Height);
            Graphics g = Graphics.FromImage(bm);
            g.DrawImageUnscaled(bitmap, 0, 0);
            g.Dispose();
            bm.Save(Path.Combine(filePath, fileName));
            return Path.Combine(fileUrl, fileName);
        }


        /// <summary>
        /// Base64转图片
        /// </summary>
        /// <param name="base64str"></param>
        /// <returns>item1图片，item2图片名称</returns>
        public static (Bitmap, string) Base64ToBitmap(string base64str)
        {

            Bitmap bitmap = null;
            // 随机生成图片名称
            var fileName = Guid.NewGuid().ToString("N");
            // 去除头部
            var reg = new Regex("data:image/(.*);base64,");
            base64str = reg.Replace(base64str, "");

            try
            {
                // 将base64转化为字节
                var arr = Convert.FromBase64String(base64str);
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    bitmap = new Bitmap(ms);
                    // 将png转化为jpg
                    if (bitmap.PixelFormat == PixelFormat.Format32bppArgb)
                    {
                        //bitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format24bppRgb);
                        fileName += ".png";
                    }
                    else
                    {
                        fileName += ".jpg";
                    }

                    Bitmap bm = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format24bppRgb);
                    Graphics g = Graphics.FromImage(bm);
                    g.DrawImageUnscaled(bitmap, 0, 0);
                    g.Dispose();
                    bitmap = bm;
                }
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex);
                bitmap = null;
                fileName = "Base64图片不正确";
            }
            return (bitmap, fileName);
        }



        /// <summary>
        /// 将网络图片转化为Bitmap
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static (Bitmap, string) UrlToBitmap(string url)
        {
            // 随机生成图片名称
            var fileName = Guid.NewGuid().ToString("N");
            Bitmap bitmap = null;
            try
            {
                WebRequest request = WebRequest.Create(url);
                using (Stream stream = request.GetResponse().GetResponseStream())
                {
                    bitmap = new Bitmap(stream);
                    // 将png转化为jpg
                    if (bitmap.PixelFormat == PixelFormat.Format32bppArgb)
                    {
                        bitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format24bppRgb);
                        fileName += ".png";
                    }
                    else
                    {
                        fileName += ".jpg";
                    }
                    return (bitmap, fileName);
                }
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex);
                fileName = "图片下载失败,检查[image_type]是否正确";
            }
            return (bitmap, fileName);

        }


        /// <summary>
        /// 图片转化为Base64
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static string BitmapToBase64(Bitmap bitmap)
        {
            using (var ms = new MemoryStream())
            {
                using (var newbitmap = new Bitmap(bitmap))
                {
                    newbitmap.Save(ms, ImageFormat.Jpeg);
                    return Convert.ToBase64String(ms.GetBuffer());
                }
            }

        }

        public static Bitmap SrcToBitmap(string imageSrc)
        {
            try
            {
                string[] srcLinux = imageSrc.Replace("\\", "/").Split('/');

                List<string> Srcc = new List<string>();

                Srcc.Add(AppDomain.CurrentDomain.BaseDirectory);
                Srcc.AddRange(srcLinux);
                var filePath = Path.Combine(Srcc.ToArray());

                Bitmap bitmap = new Bitmap(filePath);

                Bitmap bm = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bm);
                g.DrawImageUnscaled(bitmap, 0, 0);
                g.Dispose();
                
                return bm;
            }
            catch (Exception ex)
            {
                LogHelperNLog.Error(ex);
                return null;
            }
        }



        public static List<Bitmap> SrcToBitmap(IEnumerable<string> imageSrcs)
        {
            List<Bitmap> bitmaps = new List<Bitmap>();
            foreach (var src in imageSrcs)
            {
                try
                {
                    string[] srcLinux = src.Replace("\\", "/").Split('/');
                    
                    List<string> Srcc = new List<string>();

                    Srcc.Add(AppDomain.CurrentDomain.BaseDirectory);
                    Srcc.AddRange(srcLinux);
                    var filePath = Path.Combine(Srcc.ToArray());
                    Bitmap bitmap = new Bitmap(filePath);
                    Bitmap bm = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format24bppRgb);
                    Graphics g = Graphics.FromImage(bm);
                    g.DrawImageUnscaled(bitmap, 0, 0);
                    g.Dispose();
                    bitmaps.Add(bm);
                }
                catch (Exception ex)
                {
                    LogHelperNLog.Error(ex);
                }
            }
            return bitmaps;
        }
    }
}
