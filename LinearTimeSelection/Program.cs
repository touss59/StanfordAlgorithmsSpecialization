using System;

namespace LinearTimeSelection
{
    class Program
    {
        public static Random rdn = new Random();
        static void Main(string[] args)
        {
            int[] test = new int[] {2,7,3,10,4,30,22,23,24};

            Console.WriteLine(Select(test,0,test.Length-1,4));
        }

        public static int Select(int[] t, int debut, int fin,int order)
        {
            int result = 0;
            if (debut == fin)
            {
                return t[debut];
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
            if (i == order)
            {
                result = t[i];
            }
            if (i > order)
            {
             result = Select(t, debut,i-1,order);
            }
            if (i < order)
            {
                result = Select(t, i+1,fin, order);
            }

            return result;
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
            t[b] = temp;
        }
    }
}
