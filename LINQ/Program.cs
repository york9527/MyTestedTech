using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FormAuthenticationTest.Models;

namespace LINQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            UsingEFInOtherProject();

        }

        [Fact]
        static void UsingEFInOtherProject()
        {
            /*
            using (var ctx = new EFDbContext())
            {
                IQueryable<User> x = ctx.Users.Where(m => m.UserName != "");
                var y = from c in x
                        where c.UserName == "asdf"
                        select c;

                Console.Write(x.First().Email);
            }*/
            Expression<Func<int, int, int>> ex = (a, b) => a + b;
            string s = "";
            object obj = s;
        }

        [Fact]
        void ExtentionTest()
        {
            var ani = new Animal() {Height = 1.2};
            Console.WriteLine(ani.SayHeight());
        }

        [Fact]
        void LinqToObjects()
        {
            var arrStr = new string[] { "Hello", "ExploreLinq", "Object", "Xml", "Entity" };
            /*
            var result = arrStr.Where(s => s.StartsWith("H"));
            foreach (var s in result)
            {
                Console.WriteLine(s);
            }
            */

            /*
            string[] numbers = { "0042", "010", "9", "27" };
            var nums = numbers.Where((s, i) => i == 3).ToArray();
            foreach (var num in nums)
                Console.WriteLine(num);
            */

            /*
            var nums = arrStr.SelectMany(s => s.ToCharArray());
            foreach (var num in nums)
            {
                Console.Write(num + ",");
            }*/

            /*
            var nums = arrStr.TakeWhile(s => s.Length < 6);
            foreach (var num in nums)
                Console.Write(num + ",");
            */

            /*
            var nums = arrStr.SkipWhile(s => s.StartsWith("H"));
            foreach (var num in nums)
                Console.Write(num + ",");
            */

            /*
            using (var ctx = new EFDbContext())
            {
                //var userNames = ctx.Users.Select(s => s.UserName).Reverse();
                var userNames = (from o in ctx.Users
                                 select o.UserName).Reverse();
            }
            */

            /*
            var list = arrStr.ToList();
            Dictionary<int, string> dic = arrStr.ToDictionary(t=>t.Length);
            */

            
            /*
            Func<EFDbContext, string, IQueryable<User>> getUserByName = CompiledQuery
                .Compile<EFDbContext, string, IQueryable<User>>(
                    (ctx, name) =>
                            from u in ctx.Users
                                        where u.UserName == name
                                        select u;
                        )

            */
        }

        [Fact]
        void ExtentionInterfaceTest()
        {
            /*
            var mc = new MyClass();
            mc.SayHello("Jhon");//调用实现的接口的方法
            mc.SayGoodBye("jhon");//可以直接调用接口的扩展方法，具体类没有提供该方法实现。
            
            //当然，可以直接调用扩展类的扩展（静态）方法。
            MyExtention.SayGoodBye(mc, "Smith");//这种调用可以转化为mc.SayGoodBye("Smith")
            */

            /*
            //接口的explicit实现和implicit实现
            MyClass mc=new MyClass();
            mc.SayHello("class");
            IExtention ie=new MyClass();
            ie.SayHello("interface");
            */

            IExtention cl2 = new MyClass2();
            cl2.SayHello("york");
            cl2.SayGoodBye("smith");


        }
        
    }

    #region 扩展方法
    public class Animal
    {
        public double Height;
        public string Breathing()
        {
            return "Breath one time.";
        }
    }

    /// <summary>
    /// 1.扩展方法所在类必须为静态类
    /// 2.扩展方法必须为静态方法，第一个参数必须是this，加上要扩展的类型，然后一个变量，例如：this Animal a
    /// 3.扩展方法所在类不能是嵌套类
    /// </summary>
    public static class AnimalExtention
    {
        public static string SayHeight(this Animal a)
        {
            return "I'm " + a.Height + " metre height.";
        }
    }

    /***************************扩展接口**************************/
    public interface IExtention
    {
        void SayHello(string name);
    }

    public interface IExtention2:IExtention{}

    public static class MyExtention
    {
        //此方法可以被所有实现了IExtention接口的类的实例调用
        public static void SayGoodBye(this IExtention e, string name)
        {
            Console.WriteLine("GoodBye {0}", name);
        }

        
        public static void SayGoodBye(this IExtention2 e, string name)
        {
            Console.WriteLine("IExt2 GoodBye" + name);
        }
    }

    public class MyClass:IExtention
    {
        /// <summary>
        /// implicit implimentation,隐式实现，接口和类都可以调用
        /// </summary>
        /// <param name="name"></param>
        public void SayHello(string name)
        {
            Console.WriteLine("Hello {0}", name);
        }
        
        /// <summary>
        /// 1.explicit implimentation,显示实现的方法，只能通过接口调用:((IExtention)new MyClass()).SayHello();
        /// 2.当一个类实现的多个接口中具有相同的方法时，用显式方式来专门实现某个接口的方法时就显得非常有用！
        /// 3.当类实现一个接口时，通常使用隐式接口实现，这样可以方便的访问接口方法和类自身具有的方法和属性。 
        /// 4.当类实现多个接口时，并且接口中包含相同的方法签名，此时使用显式接口实现。即使没有相同的方法签名，仍推荐使用显式接口，因为可以标识出哪个方法属于哪个接口。
        /// 5.隐式接口实现，类和接口都可访问接口中方法。显式接口实现，只能通过接口访问。
        /// </summary>
        /// <param name="n"></param>
        void IExtention.SayHello(string n)
        {
            Console.WriteLine(n);
        }
    }

    /// <summary>
    /// 1.类必须实现它继承的所有接口的方法，包括更上级的接口
    /// 2.接口B继承自接口A，类C实现接口B，那么累C的实例可以调用接口A的扩展方法
    /// 3.如果接口A和B都有扩展方法Fun，接口B继承自接口A，类C实现接口B，那么在类C的实例上调用Fun，会调用接口B的扩展方法。
    ///   如果接口B没有扩展Fun，则调用接口A的Fun
    /// 4.更进一步的测试表明，不管类实C现了多少接口（横向或纵向），如果这些接口有同样的扩展方法，那么类C的实例调用的方法，
    ///   永远是它当前所属类型（某一个接口）的扩展方法
    /// </summary>
    public class MyClass2:IExtention2
    {
        public void SayHello(string name)
        {
            Console.WriteLine("MyClass2 " + name);
        }
    }



    #endregion
}
