using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSF.GreedyAlgo_MST_DM
{
    public class Week4
    {

        public static void SolveKnapSackProblem()
        {
           
            var(bagSize, nbItems, input)= Week4.GetData(@"C:\Users\valto\source\repos\StanfordAlgo\AlgoSF\GreedyAlgo_MST_DM\knapsack_big.txt") ;


            var result = new int[nbItems + 1, bagSize + 1];

            for (var i = 0; i <= nbItems; i++)
            {
                result[i, 0] = 0;
            }

            for (var y = 0; y <= bagSize; y++)
            {
                result[0, y] = 0;
            }



            for (var i = 1; i <=  nbItems; i++)
            {
                for(var y = 1; y <= bagSize; y++)
                {
                    var (val, wt) = input[i - 1];
                    result[i, y] = wt > y ? result[i - 1, y] :
                        Math.Max(result[i - 1, y], result[i - 1, y - wt] + val);
                    
                }
            }

            Console.WriteLine(result[nbItems, bagSize]);
        }

        public static void SolveKnapSackProblemSpaceOpti()
        {

            var (bagSize, nbItems, input) = Week4.GetData(@"C:\Users\valto\source\repos\StanfordAlgo\AlgoSF\GreedyAlgo_MST_DM\knapsack_big.txt");


            var array1 = new int[bagSize + 1];

            var array2 = new int[bagSize + 1];

            for (var i = 0; i <= bagSize; i++)
            {
                array1[i] = 0;
            }


            for (var i = 1; i <= nbItems; i++)
            {
                for (var y = 1; y <= bagSize; y++)
                {
                    var (val, wt) = input[i - 1];
                    array2[y] = wt > y ? array2[y] = array1[y] : Math.Max(array1[y], array1[y - wt] + val);
                }

                var temp = array1;
                array1 = array2;
                array2 = temp;
            }

            Console.WriteLine(array2[bagSize]);
        }



        public static (int size, int nbVal, List<(int val,int wt)>) GetData(string path)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(path);

            int[] info = file.ReadLine().Split().AsEnumerable().Select(x => Convert.ToInt32(x)).ToArray();

            var input = new List<(int val, int wt)>();

            for (var i = 0; i < info[1]; i++)
            {
                int[] itemInfo = file.ReadLine().Split().AsEnumerable().Select(x => Convert.ToInt32(x)).ToArray();
                input.Add((itemInfo[0], itemInfo[1]));
            }

            input.Sort();

            return (info[0], info[1], input);
        }
    }
}
