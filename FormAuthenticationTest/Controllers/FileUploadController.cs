using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace FormAuthenticationTest.Controllers
{
    public class FileUploadController : Controller
    {
        //
        // GET: /FileUpload/

        public ActionResult Upload()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return View();
            }
            else
            {
                string rst = "{\"msg\":\"文件上传成功！\"}";
                if (!SaveFile(file))
                {
                    rst = "{\"msg\":\"文件上传失败！\"}";
                }
                return Content(rst);
                /*
                //重定向，防止刷新页面导致重复提交form
                return RedirectToAction("UploadSuccess", new {fileName = file.FileName});
                */
            }
        }


        private bool SaveFile(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var path = Server.MapPath("/") + "UploadFields";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                file.SaveAs(path + "\\[" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + "]" + file.FileName);
                return true;
            }
            return false;
        }

        public ActionResult UploadSuccess(string fileName = "")
        {
            ViewBag.FileName = fileName;
            return View();
        }
        
    }
}
