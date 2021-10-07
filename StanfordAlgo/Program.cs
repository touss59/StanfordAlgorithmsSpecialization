using System;
using System.Numerics;

namespace StanfordAlgo
{
    class Program
    {
        static void Main(string[] args)
        {
            
            BigInteger a=BigInteger.Parse("3141592653589793238462643383279502884197169399375105820974944592");
            BigInteger b = BigInteger.Parse("2718281828459045235360287471352662497757247093699959574966967627");
            Console.WriteLine(RecursiveMultiplication(a, b));
        }
        public static BigInteger RecursiveMultiplication(BigInteger x, BigInteger y)
        {
            int size =Math.Max(y.ToString().Length, x.ToString().Length) / 2;
            if (size<1)
            {
                return x * y;
            }
            BigInteger a = x / BigInteger.Pow(10,size);
            BigInteger b = x % BigInteger.Pow(10,size);
            BigInteger c = y / BigInteger.Pow(10,size);
            BigInteger d = y % BigInteger.Pow(10,size);

            return  BigInteger.Pow(10, size * 2) * RecursiveMultiplication(a, c) + BigInteger.Pow(10, size) * (RecursiveMultiplication(a, d) + RecursiveMultiplication(b, c)) + RecursiveMultiplication(b, d);
        }

        public static BigInteger KaratsubaMultiplication(BigInteger x, BigInteger y)
        {
            int size = Math.Max(y.ToString().Length, x.ToString().Length) / 2;
            if (size < 1)
            {
                return x * y;
            }
            BigInteger a = x / BigInteger.Pow(10, size);
            BigInteger b = x % BigInteger.Pow(10, size);
            BigInteger c = y / BigInteger.Pow(10, size);
            BigInteger d = y % BigInteger.Pow(10, size);

            BigInteger ac = KaratsubaMultiplication(a, c);
            BigInteger bd = KaratsubaMultiplication(b, d);
            BigInteger gaussTricks= KaratsubaMultiplication(a + b, c + d) - ac - bd;

            return BigInteger.Pow(10, size * 2) * ac+ BigInteger.Pow(10, size) * gaussTricks + bd;

        }
    }
}
