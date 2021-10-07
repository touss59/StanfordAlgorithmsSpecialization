using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgoSF.DataStructures;

namespace AlgoSF.GreedyAlgo_MST_DM
{
    public class Week3
    {

        public static TreeNode GetHuffmanCodes()
        {
            var q1 = new Queue<TreeNode>(GetDataCodes(@"C:\Users\valto\source\repos\StanfordAlgo\AlgoSF\GreedyAlgo_MST_DM\huffman.txt").Select(d => new TreeNode(d)));

            var q2 = new Queue<TreeNode>();

            while (q1.Count + q2.Count != 1)
            {
                var g1 = findMin();
                var g2 = findMin();
                var mergeGroups = new TreeNode(g1.val + g2.val, g1, g2);
                q2.Enqueue(mergeGroups);
             
            }

            return q1.Count == 0 ? q2.Dequeue() : q1.Dequeue();

            TreeNode findMin()
            {
                if(q1.Count == 0)
                {
                    return q2.Dequeue();
                }

                if(q2.Count == 0)
                {
                    return q1.Dequeue();
                }

                var minNb = q1.Peek().val < q2.Peek().val ? q1.Dequeue() : q2.Dequeue();

                return minNb;
            }

        }

        public static void test()
        {
            var weight = GetDataCodes(@"C:\Users\valto\source\repos\StanfordAlgo\AlgoSF\GreedyAlgo_MST_DM\mwis.txt");
            weight.Insert(0, 0);

            var arr = new long[weight.Count];
            
            arr[0] = 0;
            arr[1] = weight[1];
            for (int j = 2; j < weight.Count; j++)
            {
                arr[j] = Math.Max(arr[j - 1], arr[j - 2] + weight[j]);
            }

            var s1 = new List<int>();

            var i = weight.Count - 1;
            while (i > 1)
            {
                if (arr[i - 1] >= arr[i - 2] + weight[i])
                {
                    i--;
                }
                else
                {
                    s1.Add(i);
                    i -= 2;
                }
            }
            if (i == 1)
            {
                if (arr[i] == weight[i])
                {
                    s1.Add(i);
                }
            }
        }

        public static List<int> GetMaxPath()
        {
            var input = GetDataCodes(@"C:\Users\valto\source\repos\StanfordAlgo\AlgoSF\GreedyAlgo_MST_DM\huffman.txt");

            var solutionsForNPoints = new Dictionary<int, long>() { };

            solutionsForNPoints.Add(0, input[0]);
            solutionsForNPoints.Add(1, input[0] > input[1] ? input[0] : input[1]);

            FindSol(input.Count - 1);

            var resultList = new List<int>();

            for(var i=input.Count - 1; i > 1; i--)
            {
                if(solutionsForNPoints[i - 1] >= (solutionsForNPoints[i - 2] + input[i]))
                {
                    resultList.Add(i);
                    i--;
                }
            }

            return resultList;

            long FindSol(int n)
            {

                if (solutionsForNPoints.ContainsKey(n))
                {
                    return solutionsForNPoints[n];
                }

                var result = Math.Max(FindSol(n - 1), FindSol(n - 2) + input[n]);
                solutionsForNPoints.Add(n, result);

                return result;
            }


        }


        public static List<int> GetLenghtCodes()
        {
            var treeCodes = GetHuffmanCodes();

            var values = new List<int>();

            var queue = new Queue<(int,TreeNode)>();
            queue.Enqueue((0,treeCodes));

            while (queue.Count != 0)
            {
                var actualNode = queue.Dequeue();

                if (actualNode.Item2.left == null)
                {
                    values.Add(actualNode.Item1);
                }
                else
                {
                    queue.Enqueue((actualNode.Item1 + 1, actualNode.Item2.left));
                    queue.Enqueue((actualNode.Item1 + 1, actualNode.Item2.right));
                }
                
            }

            return values;


        }

        public static List<int> GetDataCodes(string path)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(path);

            int nbSymbols = Convert.ToInt32(file.ReadLine());

            var input = new List<int>();

            for (var i = 0; i < nbSymbols; i++)
            {
                input.Add(Convert.ToInt32(file.ReadLine()));
            }

            input.Sort();

            return input;
        }
    }
}
