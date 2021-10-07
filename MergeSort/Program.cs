using System;

namespace MergeSort
{
   public class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[] { 1,4,1000,2,999,87,4,34,76,5,7,100,8900,46,23,89,1};

            int[] c = SplitSort(a);

            for (int i=0; i < c.Length; i++)
            {
                Console.WriteLine(c[i]);
            }
        }

        public static int[] SplitSort(int[] input)
        {
            if (input.Length == 1)
            {
                return input;
            }
            (int[] a, int[] b) = Split(input);
            return Merge(SplitSort(a), SplitSort(b));
        }

        public static int[] Merge(int[] a, int[] b)
        {
            int sizeA = a.Length;
            int sizeB = b.Length;
            int[] result = new int[sizeA + sizeB];
            int indexA = 0;
            int indexB = 0;

            for(var i=0; i< result.Length; i++)
            {
                if (sizeA == indexA)
                {
                    result[i] = b[indexB];
                    indexB++;
                }
                else if (sizeB == indexB)
                {
                    result[i] = a[indexA];
                    indexA++;
                }
                else if (a[indexA] < b[indexB])
                {
                    result[i] = a[indexA];
                    indexA++;
                }
                else
                {
                    result[i] = b[indexB];
                    indexB++;
                }
            }
            return result;
        }

        public static (int[] a, int[] b) Split(int[] input)
        {
            int[] b = new int[input.Length/2];
            int[] a = new int[input.Length - input.Length / 2];

            int countb = 0;

            for(var i=0;i< input.Length - input.Length / 2; i++)
            {
                a[i] = input[i];
            }

            for (var i = input.Length - input.Length / 2; i < input.Length ; i++)
            {
                b[countb] = input[i];
                countb++;
            }

            return (a, b);
        }
    }
}
