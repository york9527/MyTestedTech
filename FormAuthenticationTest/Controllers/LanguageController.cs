using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormAuthenticationTest.Models;

namespace FormAuthenticationTest.Controllers
{
    public class LanguageController : Controller
    {
        /*2013-09-23
         * 通过资源文件(Resource)来让网站支持不同语言
         * 1.创建资源文件Mui.resx,其他语言修改文件名即可自动匹配。例如中文(Mui.zh-cn.resx)，不同国家代码可在网上查询
         * 2.在Application_AcquireRequestState方法中设置Thread.CurrentThread.CurrentUICulture
         * 3.在改变语言的控制器方法中，设置cookie，cookie的值是创建CultureInfo的构造参数，及各国语言的缩写代码（eg. 英国 en/中国 zh-cn）
         * 4.程序中需要不同显示的地方，调用资源文件即可，ASP.NET会根据当前线程的CurrentUICulture来自动选择对应的资源文件
         */

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangeLanguage(string language)
        {
            if (!string.IsNullOrEmpty(language))
            {
                var cookie = new HttpCookie("lang", language);
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index");
        }

    }
}
