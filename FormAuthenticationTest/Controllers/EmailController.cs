using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormAuthenticationTest.Utils;

namespace FormAuthenticationTest.Controllers
{
    public class EmailController : Controller
    {
        //
        // GET: /Email/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendEmail()
        {
            string content = "邮件发送失败！";
            var isSuccess = false;
            try
            {
                isSuccess = EmailHelper.SendMessage("", "villagearcher@qq.com", "subject test",
                                                    "body test", "F:\\yuzhengwen\\DownloadWin7\\howaspnetworks.zip");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (isSuccess)
            {
                content = "邮件发送成功！";
            }
            return Content(content);
        }

    }
}
