using System;
using System.Web.Mvc;

namespace FormAuthenticationTest.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(Exception error)
        {
            Response.StatusCode = 404;
            var m = error.Message;
            if (error.InnerException != null) m += " | " + error.InnerException.Message;
            ViewBag.message = m;

            if (Request.IsAjaxRequest())
            {
                return Json(new
                    {
                        error = true,
                        message = m
                    }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.Headers.Set("Content-Type", "text/html; charset=utf-8");
                return View("Error/Error");    
            }
        }

        //iis 服务器默认404页面
        public ActionResult Error()
        {
            return View("Error/Error");
        }
    }
}