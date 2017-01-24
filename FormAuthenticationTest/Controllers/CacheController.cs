using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevTrends.MvcDonutCaching;
using FormAuthenticationTest.Models;

namespace FormAuthenticationTest.Controllers
{
    public class CacheController : Controller
    {
        //
        // GET: /Cache/

        public ActionResult Index()
        {
            return View();
        }
        
        //根据时间缓存
        [OutputCache(Duration = 10,VaryByParam = "none")]
        public ActionResult CacheByTime()
        {
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM-dd mm:HH:ss");
            return View();
        }

        //根据参数缓存
        [OutputCache(Duration =10,VaryByParam = "n")]
        public ActionResult CacheByPara(string n)
        {
            using (var ctx = new EFDbContext())
            {
                var list = ctx.Users.ToList();
                var listDropDown = new List<SelectListItem>();
                foreach (var u in list)
                {
                    listDropDown.Add(new SelectListItem() {Text = u.UserName, Value = u.UserName});
                }
                ViewBag.UserList = listDropDown;

                if (!string.IsNullOrEmpty(n))
                {
                    var user = list.FirstOrDefault(u => u.UserName == n);
                    if (user != null)
                    {
                        ViewBag.UserName = user.UserName;
                        ViewBag.Email = user.Email;
                    }
                }
            }
            return View();
        }
        
        [DonutOutputCache(Duration = 10)]
        public ActionResult PageWithDonutCache()
        {
            ViewBag.TimeCached = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return View();
        }

        public ActionResult DonutCache()
        {
            ViewBag.CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return View();
        }

        
    }
}
