using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YieldTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Test1();
            Test2();

            Console.ReadKey();
        }

        #region 
        private static void Test1()
        {
            Console.Write(Fun());
            Console.Write(Fun());
            Console.Write(Fun());

            Console.WriteLine();

            foreach (var i in FunYield())
            {
                Console.Write(i);
            }
        }

        private static int Fun()
        {
            return 1;
            return 2;
            return 3;
        }

        private static IEnumerable FunYield()
        {
            for (var i = 0; i < 3; i++)
            {
                if (i == 1) yield break;
                yield return i;
            }
        }
        #endregion

        private static void Test2()
        {
            foreach (var i in new HelloCollection())
            {
                Console.Write(i);
            }
        }

        public class HelloCollection : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return "Hello";
                yield return "World";
            }
        }

        //public class HelloCollection : IEnumerable
        //{
        //    public IEnumerator GetEnumerator()
        //    {
        //        Enumerator enumerator = new Enumerator(0);
        //        return enumerator;
        //    }
        //    public class Enumerator : IEnumerator, IDisposable
        //    {
        //        private int state;
        //        private object current;
        //        public Enumerator(int state) { this.state = state; }
        //        public bool MoveNext()
        //        {
        //            switch (state)
        //            {
        //                case 0: 
        //                    current = "Hello";
        //                    state = 1;
        //                    return true;
        //                case 1: 
        //                    current = "World";
        //                    state = 2;
        //                    return true;
        //                case 2: 
        //                    break;
        //            }
        //            return false;
        //        }
        //        public void Reset() { throw new NotSupportedException(); }
        //        public object Current { get { return current; } }
        //        public void Dispose() { }
        //    }
        //}
    }
}
