using System;

namespace QuickSort
{
    public class Program
    {
        public static Random rdn = new Random();
        public static void Main(string[] args)
        {
            int[] test = new int[] { 1, 4, 3, 5, 7, 10, 2, 99, 55, 2, 90, 2030, 25366, 142, 535, 57, 8, 9, 9, 9, 9, 10, 11 };

            QuickSort(test, 0, test.Length - 1);

            Console.WriteLine(test[0]);
        }

        public static void QuickSort(int[] t, int a, int b)
        {
            if (a == b)
            {
                return;
            }
            int debut = b;
            int fin = a;
            if (a < b)
            {
                debut = a;
                fin = b;
            }
            int pivot = rdn.Next(debut, fin);
            Swap(t, debut, pivot);
            int i = debut;

            for (int j = debut + 1; j <= fin; j++)
            {
                if (t[j] < t[debut])
                {
                    if (j != i + 1)
                    {
                        Swap(t, j, i + 1);
                    }
                    i++;
                }


            }
            Swap(t, debut, i);

            if (i == debut)
            {
                QuickSort(t, debut + 1, fin);
                return;
            }
            if (i == fin)
            {
                QuickSort(t, debut, fin - 1);
                return;
            }
            QuickSort(t, debut, i - 1);
            QuickSort(t, i + 1, fin);
            return;



        }

        public static void Swap(int[] t, int a, int b)
        {
            int temp;
            if (a == b)
            {
                return;
            }
            temp = t[a];
            t[a] = t[b];
            t[b] =temp;
        }
    }
}
