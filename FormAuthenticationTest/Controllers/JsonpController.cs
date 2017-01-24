using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormAuthenticationTest.Controllers
{
    public class JsonpController : Controller
    {
        //
        // GET: /Jsonp/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetJson(string str)
        {
            return Json("Hello " + str, JsonRequestBehavior.AllowGet);
        }

    }
}
