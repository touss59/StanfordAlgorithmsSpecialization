using System;
using System.Collections.Generic;
using System.Text;

namespace NumberInversion
{
    class Program
    {
        public static void Main(string[] args)
        {

            int[] listNumbers = Array.ConvertAll(numbers.Split('\n'), int.Parse);
            int[] listTest = new int[20000000];
            Random randomTest = new Random();
            for (int i = 0; i < listTest.Length; i++)
            {
                listTest[i] = randomTest.Next(0, 20000000);
            }
            Array.Sort(listTest);
            Recorder.Start();
            //Obj sort = SplitSort(listNumbers);
            //Console.WriteLine(sort.a);
            //Recorder.Stop();
            QuickSort.Program.QuickSort(listNumbers, 0, listNumbers.Length - 1);
            //Array.Sort(listTest);
            //MergeSort.Program.SplitSort(listTest);
            //Recorder.Start();
            //Console.WriteLine(getInvCount(listNumbers));
            Recorder.Stop();
        }

        public static Obj SplitSort(int[] obj)
        {
            if (obj.Length == 1)
            {
                Obj obj1 = new Obj { a = 0, b = obj };
                return obj1;
            }
            (int[] moitieA, int[] moitieB) = Split(obj);
            return Merge(SplitSort(moitieA), SplitSort(moitieB));
        }

        public static Obj Merge(Obj un, Obj deux)
        {
            int sizeA = un.b.Length;
            int sizeB = deux.b.Length;
            Obj result = new Obj { a = un.a + deux.a, b = new int[sizeA + sizeB] };
            int indexA = 0;
            int indexB = 0;

            for (var i = 0; i < result.b.Length; i++)
            {
                if (sizeA == indexA)
                {
                    result.b[i] = deux.b[indexB];
                    indexB++;
                }
                else if (sizeB == indexB)
                {
                    result.b[i] = un.b[indexA];
                    indexA++;
                }
                else if (un.b[indexA] < deux.b[indexB])
                {
                    result.b[i] = un.b[indexA];
                    indexA++;
                }
                else
                {
                    result.b[i] = deux.b[indexB];
                    indexB++;
                    result.a += sizeA - indexA;
                }
            }
            return result;
        }

        public static (int[] a, int[] b) Split(int[] input)
        {
            int[] b = new int[input.Length / 2];
            int[] a = new int[input.Length - input.Length / 2];

            int countb = 0;

            for (var i = 0; i < input.Length - input.Length / 2; i++)
            {
                a[i] = input[i];
            }

            for (var i = input.Length - input.Length / 2; i < input.Length; i++)
            {
                b[countb] = input[i];
                countb++;
            }

            return (a, b);
        }
    }

    public class Obj
    {
        public long a { get; set; }
        public int[] b { get; set; }

    }




}


