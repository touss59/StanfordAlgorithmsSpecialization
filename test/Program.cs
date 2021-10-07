using System;
using System.Collections.Generic;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            int s = 2;
            List<int> j = new List<int> { 1 };
            Program program = new Program();
            program.Addition(s);
            program.Addition(j);
            Console.WriteLine($"{s} and {j[0]}");
        }

        void Addition(int c)
        {
            c += 2;
        }

        void Addition(List<int> a)
        {
            a[0] += 2;
        }
    }
}
