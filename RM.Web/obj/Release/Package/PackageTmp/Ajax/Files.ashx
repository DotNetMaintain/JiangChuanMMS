<%@ WebHandler Language="c#" Class="File_WebHandler" Debug="true" %>

using System;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public class File_WebHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        HttpFileCollection files = context.Request.Files;
        if (files.Count > 0)
        {
            Random rnd = new Random();
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];

                if (file.ContentLength > 0)
                {
                  
                    
                    string fileName = file.FileName;
                    string extension = Path.GetExtension(fileName);
                    int num = rnd.Next(5000, 10000);
                    string path = "../MMS/file/" + num.ToString() + extension;

                    CutForSquare(file, System.Web.HttpContext.Current.Server.MapPath(path), 50, 50);
                   // file.SaveAs(System.Web.HttpContext.Current.Server.MapPath(path));
                    context.Response.Write("../file/" + num.ToString() + extension);
                  //  file.SaveAs(path);
                }
            }
        }
    }





    public static void CutForSquare(System.Web.HttpPostedFile postedFile, string fileSaveUrl, int side, int quality)
    {
        //创建目录
        string dir = Path.GetDirectoryName(fileSaveUrl);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
        System.Drawing.Image initImage = System.Drawing.Image.FromStream(postedFile.InputStream, true);

        //原图宽高均小于模版，不作处理，直接保存
        if (initImage.Width <= side && initImage.Height <= side)
        {
            initImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        else
        {
            //原始图片的宽、高
            int initWidth = initImage.Width;
            int initHeight = initImage.Height;

            //非正方型先裁剪为正方型
            if (initWidth != initHeight)
            {
                //截图对象
                System.Drawing.Image pickedImage = null;
                System.Drawing.Graphics pickedG = null;

                //宽大于高的横图
                if (initWidth > initHeight)
                {
                    //对象实例化
                    pickedImage = new System.Drawing.Bitmap(initHeight, initHeight);
                    pickedG = System.Drawing.Graphics.FromImage(pickedImage);
                    //设置质量
                    pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    //定位
                    Rectangle fromR = new Rectangle((initWidth - initHeight) / 2, 0, initHeight, initHeight);
                    Rectangle toR = new Rectangle(0, 0, initHeight, initHeight);
                    //画图
                    pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);
                    //重置宽
                    initWidth = initHeight;
                }
                //高大于宽的竖图
                else
                {
                    //对象实例化
                    pickedImage = new System.Drawing.Bitmap(initWidth, initWidth);
                    pickedG = System.Drawing.Graphics.FromImage(pickedImage);
                    //设置质量
                    pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    //定位
                    Rectangle fromR = new Rectangle(0, (initHeight - initWidth) / 2, initWidth, initWidth);
                    Rectangle toR = new Rectangle(0, 0, initWidth, initWidth);
                    //画图
                    pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);
                    //重置高
                    initHeight = initWidth;
                }

                //将截图对象赋给原图
                initImage = (System.Drawing.Image)pickedImage.Clone();
                //释放截图资源
                pickedG.Dispose();
                pickedImage.Dispose();
            }

            //缩略图对象
            System.Drawing.Image resultImage = new System.Drawing.Bitmap(side, side);
            System.Drawing.Graphics resultG = System.Drawing.Graphics.FromImage(resultImage);
            //设置质量
            resultG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            resultG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //用指定背景色清空画布
            resultG.Clear(Color.White);
            //绘制缩略图
            resultG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, side, side), new System.Drawing.Rectangle(0, 0, initWidth, initHeight), System.Drawing.GraphicsUnit.Pixel);

            //关键质量控制
            //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
            ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo i in icis)
            {
                if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                {
                    ici = i;
                }
            }
            EncoderParameters ep = new EncoderParameters(1);
            ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);

            //保存缩略图
            resultImage.Save(fileSaveUrl, ici, ep);

            //释放关键质量控制所用资源
            ep.Dispose();

            //释放缩略图资源
            resultG.Dispose();
            resultImage.Dispose();

            //释放原始图片资源
            initImage.Dispose();
        }
    }
    

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}