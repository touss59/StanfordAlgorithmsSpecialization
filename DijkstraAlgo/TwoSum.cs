using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstraAlgo
{
    public static class TwoSum
    {
        public static void Solve()
        {
            string input;
            List<long> numbers = new List<long> { };
            HashSet<long> numHash = new HashSet<long> { };
            int sum = 0;

            System.IO.StreamReader file =
                new System.IO.StreamReader(@"C:\Users\valto\source\repos\StanfordAlgo\DijkstraAlgo\TwoSum.txt");

            while (!string.IsNullOrEmpty(input = file.ReadLine()))
            {
                long n = Convert.ToInt64(input);
                if (!numHash.Contains(n))
                {
                    numbers.Add(n);
                    numHash.Add(n);
                }
            }
            numbers.Sort();
            sum = FastCalculate(-10000, 10000, numHash);
            Console.WriteLine(sum);
            file.Close();
        }

        public static int SlowCalculate(int start, int finish, List<int> numbers, HashSet<long> numHash)
        {
            int sum=0;
            long y;
            for (var i = start; i <= finish; i++)
            {
                foreach (var n in numbers)
                {
                    y = i - n;
                    if (n > i)
                    {
                        break;
                    }
                    if (numHash.Contains(y))
                    {
                        sum++;
                        break;
                    }
                }
            }
            return sum;
        }

        public static int FastCalculate(int start, int finish, HashSet<long> numbers)
        {
            int result = 0;

            long[] numbersArray = new long[numbers.Count];
            numbers.CopyTo(numbersArray);
            Array.Sort(numbersArray);

            Dictionary<long, bool> valueAlreadyCounted = new Dictionary<long, bool>();

            for (var i = 0; i < numbersArray.Length; i++)
            {
                long val = numbersArray[i];
                long maxValue = finish - val;
                long minValue = start - val;

                int indexOfUpperBound = Array.BinarySearch(numbersArray, maxValue);
                int indexOfLowerBound = Array.BinarySearch(numbersArray, minValue);

                if (indexOfUpperBound < 0)
                {
                    indexOfUpperBound = ~indexOfUpperBound - 1;
                }
                if (indexOfLowerBound < 0)
                {
                    indexOfLowerBound = ~indexOfLowerBound;
                }
                for (var j = indexOfLowerBound; j <= indexOfUpperBound; j++)
                {
                    var sum = numbersArray[j] + numbersArray[i];
                    if (!valueAlreadyCounted.ContainsKey(sum) && i != j)
                    {
                        valueAlreadyCounted.Add(sum, true);
                        result++;
                    }
                }
            }
            return result;
        }
    }
}
