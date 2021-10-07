using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSF.DivideAndConquer
{
    public class Week1
    {
        public static BigInteger RecursiveMultiplication(BigInteger x, BigInteger y)
        {
            var size = Math.Max(y.ToString().Length, x.ToString().Length) / 2;
            if (size < 1) return x * y;
            var a = x / BigInteger.Pow(10, size);
            var b = x % BigInteger.Pow(10, size);
            var c = y / BigInteger.Pow(10, size);
            var d = y % BigInteger.Pow(10, size);

            return BigInteger.Pow(10, size * 2) * RecursiveMultiplication(a, c) +
                   BigInteger.Pow(10, size) * (RecursiveMultiplication(a, d) + RecursiveMultiplication(b, c)) +
                   RecursiveMultiplication(b, d);
        }

        public static BigInteger KaratsubaMultiplication(BigInteger x, BigInteger y)
        {
            var size = Math.Max(y.ToString().Length, x.ToString().Length) / 2;
            if (size < 1) return x * y;
            var a = x / BigInteger.Pow(10, size);
            var b = x % BigInteger.Pow(10, size);
            var c = y / BigInteger.Pow(10, size);
            var d = y % BigInteger.Pow(10, size);

            var ac = KaratsubaMultiplication(a, c);
            var bd = KaratsubaMultiplication(b, d);
            var gaussTricks = KaratsubaMultiplication(a + b, c + d) - ac - bd;

            return BigInteger.Pow(10, size * 2) * ac + BigInteger.Pow(10, size) * gaussTricks + bd;
        }
    }
}
