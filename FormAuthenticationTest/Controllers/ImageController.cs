using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omu.Drawing;

namespace FormAuthenticationTest.Controllers
{
    public class ImageController : Controller
    {
        private List<string> ImgFormate = new List<string>() {"png", "bmp", "gif", "jpg"};
        //bmp,jpg,tiff,gif,pcx,tga,exif,fpx,svg,psd,cdr,pcd,dxf,ufo,eps,ai,raw

        public static char DirSeparator =System.IO.Path.DirectorySeparatorChar;
        public static string FilesPath = null;

        //
        // GET: /Image/

        public ActionResult UploadImage()
        {
			//test37
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            if (file != null && ModelState.IsValid)
            {
                if(IsImageSuffix(file.FileName))
                {
                    ResizeImage(file, 150, 100);
                }
            }
            return View();
        }

        //判断文件后缀名
        private bool IsImageSuffix(string fileName)
        {
            var name = fileName;
            var dotPosi = name.LastIndexOf(".") + 1;
            var suffix = name.Substring(dotPosi, name.Length - dotPosi);
            if (ImgFormate.Contains(suffix))
            {
                return true;
            }
            else
            {
                ModelState.AddModelError("FormateError", "文件格式错误，接收的格式：" + string.Join(",", ImgFormate));
                return false;
            }
        }

        #region 根据指定大小创建图片的缩略图
        /*
        Then the original uploaded image is converted to an object of the Image class using the
        InputStream of the uploaded file. A new Bitmap image is created based on the width and
        height of the thumbnail that will be created. This Bitmap image is then used to create a
        new Graphics object. The Graphics object, NewImage, is then used to set and define the
        quality, smooth, interpolation mode. Without these settings, the thumbnail image
        would not look good and be extremely pixelated and resized awkwardly.
        Once this is all set, a new Rectangle is created and the original image is drawn to the
        Graphics object. This is what performs the actually resizing. Finally the Bitmap is saved
        and all of the objects created are disposed of, to free up resources.
        */
        public void ResizeImage(HttpPostedFileBase file,int width, int height)
        {
            FilesPath = Server.MapPath("/") + "UploadImage";
            string thumbnailDirectory = String.Format(@"{0}{1}{2}", FilesPath, DirSeparator, "Thumbnails");
            // Check if the directory we are saving to exists
            if (!Directory.Exists(thumbnailDirectory))
            {
                // If it doesn't exist, create the directory
                Directory.CreateDirectory(thumbnailDirectory);
            }
            // Final path we will save our thumbnail
            string imagePath =String.Format(@"{0}{1}{2}", thumbnailDirectory,DirSeparator, file.FileName);
            // Create a stream to save the file to when we're
            // done resizing
            FileStream stream = new FileStream(Path.GetFullPath(imagePath), FileMode.OpenOrCreate);
            // Convert our uploaded file to an image
            Image OrigImage = Image.FromStream(file.InputStream);
            // Create a new bitmap with the size of our
            // thumbnail
            Bitmap TempBitmap = new Bitmap(width, height);
            // Create a new image that contains quality
            // information
            Graphics NewImage = Graphics.FromImage(TempBitmap);
            NewImage.CompositingQuality =CompositingQuality.HighQuality;
            NewImage.SmoothingMode =SmoothingMode.HighQuality;
            NewImage.InterpolationMode =InterpolationMode.HighQualityBicubic;
            // Create a rectangle and draw the image
            Rectangle imageRectangle = new Rectangle(0, 0,width, height);
            NewImage.DrawImage(OrigImage, imageRectangle);
            // Save the final file
            TempBitmap.Save(stream, OrigImage.RawFormat);
            // Clean up the resources
            NewImage.Dispose();
            TempBitmap.Dispose();
            OrigImage.Dispose();
            stream.Close();
            stream.Dispose();
        }
        #endregion

        //用Omu.Drawing组件来操作图片：复制，改变大小，保存
        [HttpPost]
        public ActionResult UploadImage2(HttpPostedFileBase file)
        {
            if (file != null && ModelState.IsValid)
            {
                if (IsImageSuffix(file.FileName))
                {
                    var path = Server.MapPath("/") + "UploadImage" + DirSeparator +"Smaller_"+DateTime.Now.ToString("fff")+"_"+ file.FileName;
                    Image image = Image.FromStream(file.InputStream);
                    //Imager.Crop()只复制图片的一部分
                    //var smaller=Imager.Crop(image, new Rectangle(0, 0, 150, 100));

                    //Imager.Resize()将原图按照指定尺寸缩小
                    var smaller = Imager.Resize(image, 150, 100, true);

                    //Imager.SaveJpen()/Imager.Save() 将Image对象保存到文件
                    Imager.SaveJpeg(path,smaller);
                }
            }
            return View("UploadImage");
        }


    }
}
