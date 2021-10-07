using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSF.DivideAndConquer
{
    public class Week3
    {
        private static readonly Random rdn = new ();

        public static int[] QuickSort(int[] t, int startIndex, int endIndex)
        {
            if (startIndex < endIndex)
            {
                int pivotIndex = partition(t, startIndex, endIndex);

                QuickSort(t, startIndex, pivotIndex - 1);
                QuickSort(t, pivotIndex + 1, endIndex);

            }

            return t;

            static int partition(int[] t, int startIndex, int endIndex)
            {
                int pivot = rdn.Next(startIndex, endIndex);
                Swap(t, startIndex, pivot);
                int i = startIndex;

                for (int actualIndex = startIndex + 1; actualIndex <= endIndex; actualIndex++)
                {
                    if (t[actualIndex] < t[startIndex])
                    {
                        if (actualIndex != i + 1)
                        {
                            Swap(t, actualIndex, i + 1);
                        }
                        i++;
                    }


                }
                Swap(t, startIndex, i);
                return i;
            }
        }

        public static int Select(int[] t, int startIndex, int endIndex, int order)
        {
            int result = 0;
            if (startIndex == endIndex)
            {
                return t[startIndex];
            }

            Swap(t, startIndex, rdn.Next(startIndex, endIndex));
            int pivotIndex = startIndex;

            for (int j = startIndex + 1; j <= endIndex; j++)
            {
                if (t[j] < t[startIndex])
                {
                    if (j != pivotIndex + 1)
                    {
                        Swap(t, j, pivotIndex + 1);
                    }
                    pivotIndex++;
                }


            }
            Swap(t, startIndex, pivotIndex);

  
            if (pivotIndex == order)
            {
                result = t[pivotIndex];
            }
            else if (pivotIndex > order)
            {
                result = Select(t, startIndex, pivotIndex - 1, order);
            }
            else if (pivotIndex < order)
            {
                result = Select(t, pivotIndex + 1, endIndex, order);
            }

            return result;
        }

        private static void Swap(int[] t, int a, int b)
        {
            if (a != b)
            {
                int temp = t[a];
                t[a] = t[b];
                t[b] = temp;
            }

        }
    }
}
