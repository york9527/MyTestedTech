using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormAuthenticationTest.Models;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace FormAuthenticationTest.Controllers
{
    public class ModelBindingController : Controller
    {
        //
        // GET: /ModelBinding/
        public ActionResult CreatePerson()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult CreatePerson(Person p)
        {
            //if (string.IsNullOrEmpty(p.Name) || p.Name.Length < 6)
            //{
            //    ModelState.AddModelError("Name", "名字必须长度大于6");
            //}
            //ModelState.AddModelError("", "model-level error");
            //if (ModelState.IsValid)
            //{
            //    //这里保存实体到数据库
            //    ViewBag.Result = true;
            //}
            //else
            //{
            //    ViewBag.Result = false;
            //}
            return View("Index");
        }

        #region 模块绑定

        /// <summary>
        /// 引用类型，请求里面不提供值，不会报异常，参数的值就是null。为了保证引用类型不为null，可以在action方法里面加上null的检查代码
        /// 值类型，如果请求里面不提供值，则会报异常。可以使用默认值解决。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ReferencePrarmeterAction(string name, int id = 0)
        {
            ViewBag.Id = id;
            return View("Index");
        }

        /// <summary>
        /// 对于复杂类型，如果没提供，则属性使用对应的默认值
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public ActionResult ComplexParaAction(Book book)
        {
            ViewBag.BookTitle = book.Title;
            return View("Index");
        }

        #endregion

        /*演示绑定的功能：前缀、排除和包含
        [HttpPost]
        public RedirectToRouteResult CreatePerson([Bind(Exclude = "Add,Phone")]Person person)
        {
            return RedirectToAction("ShowPersion",person.Add);
        }

        [HttpPost]
        public ActionResult ShowPersion([Bind(Prefix = "Add")]Address add)
        {
            return View(add);
        }*/

        //[YorkGmail]
        public class Person:IValidatableObject
        {
            public string Name { get; set; }
            //[MustBeTrueAttribute(ErrorMessage = "值必须是True")]
            public string Email { get; set; }
            //[Required(ErrorMessage = "必须填写电话号码")]
            public string Phone { get; set; }
            public Address Add { get; set; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                List<ValidationResult> errorList=new List<ValidationResult>();
                if(Name.Length<6)
                    errorList.Add(new ValidationResult("名字长度必须大于6个字符"));
                if(Phone.Length!=13)
                    errorList.Add(new ValidationResult("电话号码必须13位"));
                return errorList;
            }
        }

        public class Address
        {
            public string Country { get; set; }
            public string City { get; set; }
        }

        #region 自定义的模型绑定演这个属性

        /// <summary>
        /// 创建自己的验证属性
        /// </summary>
        public class MustBeTrueAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                return value is bool && (bool) value;
            }
        }

        /// <summary>
        /// 从内建验证属性继承，在使用内建验证功能的同时，加入需要的验证功能。
        /// </summary>
        public class FutureDateAttribute : RequiredAttribute
        {
            public override bool IsValid(object value)
            {
                return base.IsValid(value) && ((DateTime) value) > DateTime.Now;
            }
        }

        /// <summary>
        /// 模型级别的验证，要求名称为"YORK"的账号，期邮箱必须是"gmail.com"的。
        /// </summary>
        public class YorkGmailAttribute : ValidationAttribute
        {
            public YorkGmailAttribute()
            {
                ErrorMessage = "要求名称为YORK的账号，期邮箱必须是gmail.com";
            }

            public override bool IsValid(object value)
            {
                Person p = value as Person;
                if (p == null || string.IsNullOrEmpty(p.Email) || p.Name == null)
                {
                    return true;
                }
                else
                {
                    return p.Name == "YORK" && p.Email.IndexOf("gmail.com") > 0;
                }
            }
        }
    }

    #endregion

    /*
Html.ValidationSummary() 显示所有验证错误。
Html.ValidationSummary(bool) true：只显示模型级别错误；false：显示所有错误。
Html.ValidationSummary(string) 在所有错误消息的最上面，显示参数指定的字符串。
Html.ValidationSummary(bool,string) 同上类推。
 */
}
