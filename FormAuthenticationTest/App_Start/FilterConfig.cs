using System;
using System.Web;
using System.Web.Mvc;
using FormAuthenticationTest.Models;

namespace FormAuthenticationTest
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //注册全局异常处理，默认都返回同样View（Error/ErrorServer），如果是ajax请求，则返回同样json对象
            filters.Add(new CustomHandleErrorAttribute()
                {
                    View = "Error/ErrorServer"
                });


            /*
            filters.Add(new HandleErrorAttribute()
                {
                    View = "NotFound"
                });
            */
            /*
            //order 数字越大，优先级越高。
            filters.Add(new HandleErrorAttribute()
                {
                    ExceptionType = typeof(System.Data.DataException),
                    View = "DataErrorGlobal"
                },2);
            filters.Add(new HandleErrorAttribute(), 1);
            */ 
            filters.Add(new GlobalAuthentication());
        }
    }
}