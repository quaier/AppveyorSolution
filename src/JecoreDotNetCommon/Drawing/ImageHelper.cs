using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using JecoreDotNetCommon.Tools;

namespace JecoreDotNetCommon.Drawing
{
    /// <summary>
    /// 图片类操作
    /// </summary>
    public static class ImageHelper
    {
        private static Dictionary<string, ImageFormat> _imageFormats;

        /// <summary>
        /// 所有支持的图片格式字典
        /// </summary>
        public static Dictionary<string, ImageFormat> ImageFormats
        {
            get
            {
                if (_imageFormats == null)
                {
                    var dic = new Dictionary<string, ImageFormat>();
                    var properties = typeof(ImageFormat).GetProperties(BindingFlags.Static | BindingFlags.Public);
                    foreach (var property in properties)
                    {
                        var format = property.GetValue(null, null) as ImageFormat;
                        if (format == null) continue;
                        dic.Add(("." + property.Name).ToLower(), format);
                    }
                    _imageFormats = dic;
                }
                return _imageFormats;
            }
        }

        /// <summary>
        /// 图片压缩(降低质量以减小文件的大小)
        /// </summary>
        /// <param name="originalImage">原始图片</param>
        /// <param name="level" >压缩质量，0到100，0 最差质量，100 最佳 默认50</param>
        /// <param name="width">压缩后的宽度，默认为原始图片宽度</param>
        /// <param name="height">压缩后的高度，默认为原始图片高度</param>
        /// <returns>压缩后的图片 文件流形式</returns>
        public static Image Compress(Image originalImage, long? quality = 50, int? width = null, int? height = null)
        {
            quality = quality ?? 50;

            if (width != null)
            {
                if (height == null)
                {
                    height = width * originalImage.Height / originalImage.Width;
                }
            }
            else
            {
                if (height != null)
                {
                    width = height * originalImage.Width / originalImage.Height;
                }
            }

            // 待输出图片 画布宽高  
            int resImageWidth = width ?? originalImage.Width;
            int resImageHeight = height ?? originalImage.Height;

            // 新建一个流 用于输出 
            MemoryStream byteStream = new MemoryStream();

            Bitmap resImage = new Bitmap(resImageWidth, resImageHeight);

            try
            {
                Graphics g = Graphics.FromImage(resImage);

                // 设置高质量插值法 
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // 设置高质量,低速度呈现平滑程度  
                g.SmoothingMode = SmoothingMode.HighQuality;

                // 清空画布并以透明背景色填充 
                g.Clear(Color.Transparent);

                // 在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new Rectangle(0, 0, resImageWidth, resImageHeight), new Rectangle(0, 0, originalImage.Width, originalImage.Height), GraphicsUnit.Pixel);

                g.Dispose();

                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, quality.Value);
                resImage.Save(byteStream, GetEncoderInfo(GetExtension(originalImage)), encoderParams);
                encoderParams.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }

            return Image.FromStream(byteStream);
        }

        /// <summary>
        /// 图片裁剪
        /// </summary>
        /// <param name="originalImage">原始图片</param>
        /// <param name="width">裁剪后的宽度，默认为原始图片宽度</param>
        /// <param name="height">裁剪后的高度，默认为原始图片高度</param>
        /// <returns>裁剪后的图片</returns>
        public static Image Resizer(Image originalImage, int? width, int? height)
        {
            // 待输出图片 画布宽高  
            int resImageWidth = width ?? originalImage.Width;
            int resImageHeight = height ?? originalImage.Height;

            // 新建一个位图 用于输出 
            Bitmap resImage = new Bitmap(resImageWidth, resImageHeight);

            try
            {
                Graphics g = Graphics.FromImage(resImage);

                // 设置高质量插值法 
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // 设置高质量,低速度呈现平滑程度  
                g.SmoothingMode = SmoothingMode.HighQuality;

                // 清空画布并以透明背景色填充 
                g.Clear(Color.Transparent);

                // 在指定位置并且按指定大小绘制原图片的指定部分 
                g.DrawImage(originalImage, new Rectangle(0, 0, resImageWidth, resImageHeight), new Rectangle(0, 0, originalImage.Width, originalImage.Height), GraphicsUnit.Pixel);

                g.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
            return resImage;
        }

        /// <summary>
        /// 为图片添加水印
        /// </summary>
        /// <param name="originalImage"></param>
        /// <param name="watermarkPath"></param>
        /// <param name="transparency">水印的透明度 0-255 数值越</param>
        /// <returns></returns>
        public static Image AddWatermark(Image originalImage, string watermarkPath, WatermarkPosition? position, int? transparency = null)
        {
            // 水印图片
            Image watermarkImage = Image.FromFile(watermarkPath);

            Bitmap resImage = new Bitmap(originalImage.Width, originalImage.Height,PixelFormat.Format32bppArgb);

            try
            {
                //获取原图DPI
                float dpix = originalImage.HorizontalResolution;
                float dpiy = originalImage.VerticalResolution;

                // 设置其生成图片的dpi
                resImage.SetResolution(dpix, dpiy);

                Graphics g = Graphics.FromImage(resImage);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.Default;

                // 设置高质量插值法 
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // 设置高质量,低速度呈现平滑程度  
                g.SmoothingMode = SmoothingMode.HighQuality;

                // 清空画布并以透明背景色填充 
                g.Clear(Color.Transparent);

                // 在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, 0, 0, originalImage.Width, originalImage.Height);
                //g.DrawImage(originalImage, new Rectangle(0, 0, resImage.Width, resImage.Height), new Rectangle(0, 0, originalImage.Width, originalImage.Height), GraphicsUnit.Pixel);

                // 设定水印位置
                double drawX = 0;
                double drawY = 0;
                double drawWidth = 0;
                double drawHeight = 0;

                double resRatio = (double)resImage.Width / resImage.Height;
                double watermarkRatio = (double)watermarkImage.Width / watermarkImage.Height;

                if (watermarkRatio > resRatio)
                {
                    drawWidth = resImage.Width / 10;
                    drawWidth = drawWidth < 40 ? 40 : drawWidth;
                    drawWidth = drawWidth > 160 ? 160 : drawWidth;
                    drawHeight = drawWidth / watermarkRatio;
                }
                else
                {
                    drawHeight = resImage.Height / 10;
                    drawHeight = drawHeight < 40 ? 40 : drawHeight;
                    drawHeight = drawHeight > 160 ? 160 : drawHeight;
                    drawWidth = drawHeight / watermarkRatio;
                }

                switch (position)
                {
                    case WatermarkPosition.LeftTop:
                        drawX = watermarkImage.Width / 2f;
                        drawY = 8f;
                        break;
                    case WatermarkPosition.LeftBottom:
                        drawX = watermarkImage.Width / 2f;
                        drawY = originalImage.Height - watermarkImage.Height;
                        break;
                    case WatermarkPosition.RightTop:
                        drawX = originalImage.Width * 1f - watermarkImage.Width / 2f;
                        drawY = 8f;
                        break;
                    case WatermarkPosition.RightBottom:
                        drawX = originalImage.Width - watermarkImage.Width;
                        drawY = originalImage.Height - watermarkImage.Height;
                        break;
                    case WatermarkPosition.Center:
                        drawX = originalImage.Width / 2;
                        drawY = originalImage.Height / 2 - watermarkImage.Height / 2;
                        break;
                }

                // 为水印图片设置透明度
                if (transparency != null)
                {
                    // 颜色矩阵  
                    float[][] matrixItems = {
                        new float[]{1,0,0,0,0},
                        new float[]{0,1,0,0,0},
                        new float[]{0,0,1,0,0},
                        new float[]{0,0,0, transparency.Value / 255f,0},
                        new float[]{0,0,0,0,1}
                    };
                    ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
                    ImageAttributes imageAtt = new ImageAttributes();
                    imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    Bitmap bmpd = new Bitmap(watermarkImage.Width, watermarkImage.Height);
                    Graphics gd = Graphics.FromImage(bmpd);
                    gd.DrawImage(watermarkImage, new Rectangle(0, 0, watermarkImage.Width, watermarkImage.Height),
                            0, 0, watermarkImage.Width, watermarkImage.Height, GraphicsUnit.Pixel, imageAtt);
                    gd.Dispose();

                    watermarkImage = bmpd;
                }

                // 在指定位置绘制水印图片部分
                g.DrawImage(watermarkImage,
                       new Rectangle((int)Math.Floor(drawX), (int)Math.Floor(drawY), (int)Math.Floor(drawWidth), (int)Math.Floor(drawHeight)),
                       0, 0, watermarkImage.Width, watermarkImage.Height, GraphicsUnit.Pixel);

                g.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                watermarkImage.Dispose();
            }
            return resImage;
        }

        /// <summary>
        /// 等比例压缩生成三张 大、中、小的图片
        /// </summary>
        /// <param name="originalImage"></param>
        /// <param name="watermarkPath">水印图片地址</param>
        /// <returns></returns>
        public static Thumb GenerateThumb(Image originalImage, string watermarkPath)
        {
            // original
            var _original = Compress(originalImage, 50, 1200);
            var original = AddWatermark(_original, watermarkPath, WatermarkPosition.Center, 100);
            _original.Dispose();

            // mini
            var _mini = Compress(originalImage, 50, 360);
            var mini = AddWatermark(_mini, watermarkPath, WatermarkPosition.Center, 100);
            _mini.Dispose();

            // mini
            var _micro = Compress(originalImage, 50, 120);
            var micro = AddWatermark(_micro, watermarkPath, WatermarkPosition.Center, 100);
            _micro.Dispose();

            return new Thumb()
            {
                Original = original,
                Mini = mini,
                Micro = micro
            };
        }

        /// <summary>  
        /// 根据图片后缀名获取图片编码信息  
        /// </summary>  
        private static ImageCodecInfo GetEncoderInfo(string extension)
        {
            extension = extension.Replace(".", "");
            extension = "image/" + extension;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < encoders.Length; i++)
            {
                if (encoders[i].MimeType.ToLower() == extension.ToLower())
                {
                    return encoders[i];
                }

            }
            return null;
        }

        /// <summary>
        /// 获取指定image对象的后缀名
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string GetExtension(Image image)
        {
            foreach (var pair in ImageFormats)
            {

                if (pair.Value.Guid == image.RawFormat.Guid)
                {
                    return pair.Key;
                }
            }
            throw new BadImageFormatException();
        }

        /// <summary>
        /// 将image对象以指定的文件名、格式 保存到指定目录
        /// </summary>
        /// <param name="directoryPath">目录</param>
        /// <param name="fileName">文件名</param>
        /// <param name="image">图片</param>
        /// <param name="imageFormat">图片格式</param>
        /// <returns></returns>
        public static string Save(string directoryPath, string fileName, Image image, ImageFormat imageFormat)
        {
            // 保存到指定文件
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string fielPath = Helps.PathCombine(directoryPath, fileName);
            image.Save(fielPath, imageFormat);
            return fielPath;
        }

        /// <summary>
        /// 将stream对象以指定的文件名、格式 保存到指定目录
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="fileName"></param>
        /// <param name="image"></param>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        public static string Save(string directoryPath, string fileName, Stream stream, ImageFormat imageFormat)
        {
            // 保存到指定文件
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string fielPath = Helps.PathCombine(directoryPath, fileName);
            Image image = Image.FromStream(stream);
            image.Save(fielPath, imageFormat);
            return fielPath;
        }

        /// <summary>
        /// 根据文件拓展名 获取 对应的存储类型
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static ImageFormat GetImageFormat(string extension)
        {
            switch (extension)
            {
                case ".gif": return ImageFormat.Gif;
                case ".jpg": return ImageFormat.Jpeg;
                case ".bmp": return ImageFormat.Bmp;
                case ".png": return ImageFormat.Png;
                case ".ico": return ImageFormat.Icon;
                case ".emf": return ImageFormat.Emf;
                case ".exif": return ImageFormat.Exif;
                case ".tiff": return ImageFormat.Tiff;
                case ".wmf": return ImageFormat.Wmf;
                case "memorybmp": return ImageFormat.MemoryBmp;
            }
            return ImageFormat.MemoryBmp;
        }

        /// <summary>
        /// 将image对象转换为stream流
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Stream ToStream(Image image, ImageFormat format = null)
        {
            if (format == null)
            {
                var extension = GetExtension(image);
                format = GetImageFormat(extension);
            };
            var stream = new MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// 将二进制 转换为image对象
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Image ToImage(byte[] bytes)
        {
            var imageStream = new MemoryStream(bytes);

            return Image.FromStream(imageStream);
        }

        public static string ToMiniImgUrl(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            return str.Replace("original", "mini");
        }
        public static string ToMicroImgUrl(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            return str.Replace("original", "micro");
        }
        public static string ToShortTitle(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            return str.Substring(0, 40) + "...";
        }
    }

    public class Thumb
    {
        public Image Original { get; set; }
        public Image Mini { get; set; }
        public Image Micro { get; set; }
    }

    public enum WatermarkPosition
    {
        Center,
        LeftTop,
        LeftBottom,
        RightTop,
        RightBottom
    }
}
