using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using FormAuthenticationTest.Controllers;
using FormAuthenticationTest.Models;
using Microsoft.AspNet.SignalR;

namespace FormAuthenticationTest
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public string ApplicationId = Guid.NewGuid().ToString();
        
        protected void Application_Start()
        {
            //注册SignalR
            SignalRConfiguration.RegisterHub(RouteTable.Routes);

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new DropCreateDatabaseIfModelChanged());

            //资源文件打包功能开关
            //BundleTable.EnableOptimizations = true;
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null)
                return;

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var iDentity = new FormsIdentity(ticket);

            string[] roles = ticket.UserData.Split(',');

            //这里的rols就是这个用户拥有的角色
            var principal = new GenericPrincipal(iDentity, roles);
            Context.User = principal;
            Thread.CurrentPrincipal = principal;
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {

        }

        //设置语言
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var c = Request.Cookies["lang"];
            if (c == null || c.Value == "auto") return;

            var l = c.Value;
            var ci = new CultureInfo(l);

            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture =CultureInfo.CreateSpecificCulture(ci.Name);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            // Log the exception.
            Response.Clear();

            var httpException = exception as HttpException;

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");

            #region 所有到达Application_Error的异常，都导向到ErrorController
            /*
            if (httpException == null)
            {
                routeData.Values.Add("action", "Index");
            }
            else //It's an Http Exception, Let's handle it.
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // Page not found.
                        routeData.Values.Add("action", "HttpError404");
                        break;
                    case 505:
                        // Server error.
                        routeData.Values.Add("action", "HttpError505");
                        break;

                    // Here you can handle Views to other error codes.
                    // I choose a General error template  
                    default:
                        routeData.Values.Add("action", "Index");
                        break;
                }
            }
            */
            #endregion

            // Pass exception details to the target error View.
            routeData.Values.Add("error", exception);

            // Clear the error on server.
            Server.ClearError();

            // Call target Controller and pass the routeData.
            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }
    }
}