using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FormAuthenticationTest.Models;
using System.Data.Entity;

namespace FormAuthenticationTest.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [GlobalAuthentication(Roles = "Admin,testA")]
        public ActionResult Index(string msg)
        {
            ViewBag.msg = msg;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string userName, string userPwd)
        {
            var user = new User();
            var isAccept = ValidateUser(userName, userPwd, out user);
            if (isAccept)
            {
                SetCookie(user);
                return RedirectToAction("Index", new {msg = userName + "/" + userPwd});
            }

            ModelState.AddModelError("ERROR","Account or Passwork Error.");
            return View();
        }

        private bool ValidateUser(string userName, string userPwd, out User user)
        {
            user = new User();
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userPwd))
            {
                try
                {
                    using (var ctx = new EFDbContext())
                    {
                        var u = ctx.Users.Single(m => m.UserName == userName && m.Password == userPwd);
                        ctx.Entry(u).Collection(d => d.Roles).Load();
                        user = u;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    //此处密码或者账号不对，直接返回登录视图
                }
            }
            return false;
        }

        private void SetCookie(User user)
        {
            var ticket = new FormsAuthenticationTicket(
                    1,
                    user.UserName,
                    DateTime.Now,
                    DateTime.Now.AddHours(1),
                    false,
                    string.Join(",", user.Roles.Select(m => m.RoleName).ToList()));

            //FormsAuthentication.FormsCookieName,cookie的名字，默认是".ASPXAUTH"
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            Response.Cookies.Add(cookie);
        }

        #region Regeste

        [AllowAnonymous]
        public ActionResult Regester()
        {

            ViewBag.RoleList = GetRoleList();
            return View();
        }

        private List<SelectListItem> GetRoleList()
        {
            using (var ctx = new EFDbContext())
            {
                var roles = ctx.Roles.ToList();
                var roleList = new List<SelectListItem>();
                foreach (var role in roles)
                {
                    roleList.Add(new SelectListItem() { Text = role.RoleName, Value = role.RoleId.ToString() });
                }
                return  roleList;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Regester([Bind(Include = "UserName,Password,Email")]User userModel, int roleId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userModel.LastActiveDate = DateTime.Now;
                    using (var ctx = new EFDbContext())
                    {
                        var role = ctx.Roles.Find(roleId);
                        if (role != null)
                            userModel.Roles.Add(role);
                        ctx.Users.Add(userModel);
                        ctx.SaveChanges();
                    }
                }
                catch (Exception ex){throw;}
               return RedirectToAction("Index");
            }
            ViewBag.RoleList = GetRoleList();
            return View(userModel);
        }


        public ActionResult UserList()
        {
            using (var ctx = new EFDbContext())
            {
                var users = ctx.Users.Include(m => m.Roles).ToList();
                ViewBag.UserList = users;
            }
            return View();
        }
        #endregion

        #region CreateRole
        public ActionResult Role()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Role(Role role)
        {
            if (ModelState.IsValid)
            {
                role.CreDate = DateTime.Now;
                role.UpdDate = DateTime.Now;
                using (var ctx = new EFDbContext())
                {
                    ctx.Roles.Add(role);
                    ctx.SaveChanges();
                }
                ViewBag.RoleName = role.RoleName;
            }
            return View();
        }


        public ActionResult RoleList()
        {
            using (var ctx = new EFDbContext())
            {
                var roles = ctx.Roles.ToList();
                ViewBag.RoleList = roles;
            }
            return View();
        }
        #endregion

    }
}
