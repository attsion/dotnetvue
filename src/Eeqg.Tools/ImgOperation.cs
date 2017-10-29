using System;
using System.Collections.Generic;
using System.Text;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.Runtime.InteropServices;
using System.DrawingCore.Imaging;
namespace EEQG.Tools
{
    public static class ImgOperation
    {
        public enum OperantionModeEnum
        {
            像素法, 内存拷贝法, 指针法
        }
        /// <summary>
        /// 比例不同会变形
        /// </summary>
        /// <param name="oldmap"></param>
        /// <param name="topx"></param>
        /// <param name="topy"></param>
        /// <param name="bottomx"></param>
        /// <param name="bottomy"></param>
        /// <returns></returns>
        public static Bitmap MakeThumbnailSmiple(Bitmap oldmap, int topx, int topy, int bottomx, int bottomy)
        {

            //Bitmap oldmap = new Bitmap(path);
            //新建一个bmp图片
            Bitmap bitmap = new Bitmap(bottomx - topx, bottomy - topy);

            //新建一个画板
            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(oldmap, new Rectangle(0, 0, bottomx - topx, bottomy - topy),
                new Rectangle(topx, topy, bottomx - topx, bottomy - topy),
                GraphicsUnit.Pixel);

            return bitmap;
        }
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static Bitmap MakeThumbnail(Bitmap originalImage, int width, int height, string mode)
        {
            if (originalImage.Width == width && originalImage.Height == height)
            {
                return new Bitmap(originalImage);
            }

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "WH"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Bitmap bitmap = new Bitmap(towidth, toheight);

            //新建一个画板
            using (Graphics g = Graphics.FromImage(bitmap))
            {

                //设置高质量插值法
                g.InterpolationMode = InterpolationMode.High;

                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = SmoothingMode.HighQuality;

                //清空画布并以透明背景色填充
                g.Clear(Color.Transparent);

                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                    new Rectangle(x, y, ow, oh),
                    GraphicsUnit.Pixel);
                g.Dispose();
                GC.Collect();
            }
            return bitmap;

        }

        public static unsafe void MakeBlackWhite(ref Bitmap curBitmap, OperantionModeEnum mode)
        {
            if (mode == OperantionModeEnum.像素法)
            {
                int width = curBitmap.Width;
                int height = curBitmap.Height;
                for (int i = 0; i < width; i++) //这里如果用i<curBitmap.Width做循环对性能有影响         
                {
                    for (int j = 0; j < height; j++)
                    {
                        Color curColor = curBitmap.GetPixel(i, j);
                        int ret = (int)(curColor.R * 0.299 + curColor.G * 0.587 + curColor.B * 0.114);
                        curBitmap.SetPixel(i, j, Color.FromArgb(ret, ret, ret));
                    }
                }
            }
            else if (mode == OperantionModeEnum.内存拷贝法)
            {
                int width = curBitmap.Width;
                int height = curBitmap.Height;

                Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                BitmapData bmpData = curBitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);//curBitmap.PixelFormat

                IntPtr ptr = bmpData.Scan0;
                int bytesCount = bmpData.Stride * bmpData.Height;
                byte[] arrDst = new byte[bytesCount];
                Marshal.Copy(ptr, arrDst, 0, bytesCount);
                for (int i = 0; i < bytesCount; i += 3)
                {
                    byte colorTemp = (byte)(arrDst[i + 2] * 0.299 + arrDst[i + 1] * 0.587 + arrDst[i] * 0.114);
                    arrDst[i] = arrDst[i + 1] = arrDst[i + 2] = (byte)colorTemp;

                }
                Marshal.Copy(arrDst, 0, ptr, bytesCount);
                curBitmap.UnlockBits(bmpData);

            }
            else if (mode == OperantionModeEnum.指针法)
            {
                int width = curBitmap.Width;
                int height = curBitmap.Height;
                Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                BitmapData bmpData = curBitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);//curBitmap.PixelFormat            
                byte temp = 0;
                int w = bmpData.Width;
                int h = bmpData.Height;
                unsafe
                {
                    byte* ptr = (byte*)(bmpData.Scan0);
                    for (int i = 0; i < h; i++)
                    {
                        for (int j = 0; j < w; j++)
                        {
                            temp = (byte)(0.299 * ptr[2] + 0.587 * ptr[1] + 0.114 * ptr[0]);
                            ptr[0] = ptr[1] = ptr[2] = temp;
                            ptr += 3; //Format24bppRgb格式每个像素占3字节                 
                        }
                        ptr += bmpData.Stride - bmpData.Width * 3;//每行读取到最后“有用”数据时，跳过未使用空间XX         
                    }
                }
                curBitmap.UnlockBits(bmpData);

            }
        }

        public static void DrawStringCenter(this Graphics g, string s, Font font, Brush brush, RectangleF layoutRectangle)
        {
            float x = 0;
            var mr = g.MeasureString(s, font);
            if (mr.Width > layoutRectangle.Width)
            {
                x = layoutRectangle.X;
            }
            else
            {
                x = layoutRectangle.X + layoutRectangle.Width / 2 - mr.Width / 2;
            }
            g.DrawString(s, font, brush, x, layoutRectangle.Y);

        }

    }
}
