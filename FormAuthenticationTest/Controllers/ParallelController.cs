using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FormAuthenticationTest.Models;

namespace FormAuthenticationTest.Controllers
{
    public class ParallelController : AsyncController
    {
        /*
        《》
        Here are the steps for handling an asynchronous request . ASP.NET grabs a thread from
        the thread pool and executes it to handle the incoming request. After invoking the
        ASP.NET MVC action asynchronously, it returns the thread to the thread pool so it
        can handle other requests. The asynchronous operation executes on a different thread;
        when it’s done it notifies ASP.NET. ASP.NET grabs a thread (which may be different
        than the original thread) and invokes it to finish processing the request. This includes
        rendering the process (output).
         */

        public async Task<ActionResult> Index()
        {
            var i = Thread.CurrentThread.ManagedThreadId;
            var books = await Search();
            ViewBag.ImagePath = @"/UploadImage/";
            return View(books);
        }

        private async Task<List<Book>> Search()
        {
            var i = Thread.CurrentThread.ManagedThreadId;
            using (var ctx = new EFDbContext())
            {
                return ctx.Books.Take(10).ToList();
            }
        }
    }
}
