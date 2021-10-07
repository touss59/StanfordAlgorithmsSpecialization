using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgoSF.Helpers;

namespace AlgoSF.DivideAndConquer
{
    class Week2
    {
        public static long numberOfInversions;

        public static long FindNumberOfInversions()
        {
            var input = ReadFile.ReturnArrayOfIntFromPath(
                Path.GetFullPath("../../../DivideAndConquer/IntegerArray.txt"));

            numberOfInversions = 0;
            SplitSort(input);

            return numberOfInversions;
        }

        public static int[] SplitSort(int[] numbers)
        {
            if (numbers.Length == 1) return numbers;
            var ( leftPart,  rightPart) = Split(numbers);
            return Merge(SplitSort(leftPart), SplitSort(rightPart));
        }

        public static int[] Merge(int[] left, int[] right)
        {
            var sizeA = left.Length;
            var sizeB = right.Length;
            var result = new int[sizeA + sizeB];
            var indexA = 0;
            var indexB = 0;

            for (var i = 0; i < result.Length; i++)
                if (sizeA == indexA)
                {
                    result[i] = right[indexB];
                    indexB++;
                }
                else if (sizeB == indexB)
                {
                    result[i] = left[indexA];
                    indexA++;
                }
                else if (left[indexA] < right[indexB])
                {
                    result[i] = left[indexA];
                    indexA++;
                }
                else
                {
                    result[i] = right[indexB];
                    indexB++;
                    numberOfInversions += sizeA - indexA;
                }

            return result;
        }

        public static (int[] a, int[] b) Split(int[] input)
        {
            var a = new int[input.Length - input.Length / 2];
            var b = new int[input.Length / 2];


            var countb = 0;

            for (var i = 0; i < input.Length - input.Length / 2; i++) a[i] = input[i];

            for (var i = input.Length - input.Length / 2; i < input.Length; i++)
            {
                b[countb] = input[i];
                countb++;
            }

            return (a, b);
        }

    }



}
