using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormAuthenticationTest.Controllers
{
    //本控制器用来制造异常，模拟异常发生
    public class ErrorExecuteController : Controller
    {
        //
        // GET: /ErrorHandle/

        //返回异常测试页面
        public ActionResult Index()
        {
            return View();
        }

        //抛出一般异常，这个由HandleErrorAttribute处理，因为是控制器内部异常
        public ActionResult MakeError()
        {
            throw new Exception("Something went wrong.");
        }

        //HandleErrorAttribute处理
        //HandleError也可用于Controller，指定异常的返回页面
        [HandleError(ExceptionType = typeof(DataException),View = "DataError")]
        public ActionResult MakeError2()
        {
            throw new DataException();
        }

        //HandleErrorAttribute处理
        //通过GlobalFilterCollection注册的全局Filter来捕获异常
        public ActionResult MakeError3()
        {
            throw new DataException();
        }

        //HandleErrorAttribute处理
        //测试优先级:Order越大，优先级越高。
        [HandleError(ExceptionType = typeof(DataException),View = "DataError",Order = 3)]
        [HandleError(ExceptionType = typeof(Exception),View ="DataErrorGlobal",Order = 2)]
        public ActionResult MakeError4()
        {
            throw new DataException();
        }

        //Application_Error处理，因为HandleErrorAttribute只处理Http Status Code为500的，也就是服务器错误
        public ActionResult MakeError5()
        {
            throw new HttpException(404,"没有找到资源！");
        }

        //Application_Error处理，ajax 异常，不能返回页面，要返回json对象
        public ActionResult ExceptionWithAjaxRequest()
        {
            throw new HttpException(404,"throwed by ajax 404");
        }

        //HandleErrorAttribute处理，ajax 异常，不能返回页面，要返回json对象
        public ActionResult ExceptionWithAjaxRequest2()
        {
            throw new HttpException("throwed by ajax 500");
        }

    }
}
