using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgoSF.DataStructures;

namespace AlgoSF.GraphShortestPaths
{
    public class Week3
    {
            public static void SolveMedianMaintenance()
            {
                var heapLow = new MaxHeap<int>();
                var heapHigh = new MinHeap<int>{ };
                long result = 0;
                string input;

                while (!string.IsNullOrEmpty(input = Console.ReadLine()))
                {
                    int i = Convert.ToInt32(input);
                    if (i < heapHigh.Peek())
                    {
                        heapLow.Add(i);
                    }
                    else
                    {
                        heapHigh.Add(i);
                    }

                    if (heapHigh.Size > heapLow.Size + 1)
                    {
                        int swapInt = heapHigh.Poll();
                        heapLow.Add(swapInt);
                    }
                    else if (heapHigh.Size + 1 < heapLow.Size)
                    {
                        int swapInt = heapLow.Poll();
                        heapHigh.Add(swapInt);
                    }

                    if ((heapHigh.Size + heapLow.Size) % 2 == 0)
                    {
                        result += heapLow.Peek();
                    }
                    else
                    {
                        if (heapHigh.Size > heapLow.Size)
                        {
                            result += heapHigh.Peek();
                        }
                        else
                        {
                            result += heapLow.Peek();
                        }
                    }

                }
                Console.WriteLine(result);
            }
    }
    
}
