using System;
using System.Collections.Generic;
using System.Linq;

namespace test2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> a = new List<int> { 2 };
            addl(a);
            addT(a);
            Console.WriteLine(a[0]);
        }

        public static void addl(List<int> a)
        {
            a[0] += 1;
        }

        public static void addT(List<int> a)
        {
            List<int> b = a.ToList();
            b[0] += 1;
        }
    }
}
