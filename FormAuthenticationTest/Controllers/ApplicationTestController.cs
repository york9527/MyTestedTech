using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace FormAuthenticationTest.Controllers
{
    public class ApplicationTestController : Controller
    {
        //
        // GET: /ApplicationTest/
        [AllowAnonymous]
        public ActionResult HttpApplicationTest()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            return View();
        }



    }
}
