using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgoSF.DataStructures;

namespace AlgoSF.GreedyAlgo_MST_DM
{
    public class Week2
    {
        public static int ComputeMaxSpacingKClustering()
        {

            var (nbEdges, edgesWithCost) = Week1.GetEdgesFromTXT(@"C:\Users\valto\source\repos\StanfordAlgo\AlgoSF\GreedyAlgo_MST_DM\clustering1.txt");

            var unionFind = new SimpleUnionFind(nbEdges);
            edgesWithCost = edgesWithCost.OrderBy(e => e.cost).ToList();

            var watch = System.Diagnostics.Stopwatch.StartNew();


            var index = 0; // use to avoid waste of work
            while (unionFind.GetCount() > 4)
            {
                var minEdge = edgesWithCost.Skip(index).Where(e => !unionFind.IsConnected(e.pointA - 1, e.pointB - 1)).First();
                index = edgesWithCost.IndexOf(minEdge)+1;
                unionFind.Merge(minEdge.pointA - 1, minEdge.pointB - 1);
            }


            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            var t = CreateArrayBitMask2();

            return edgesWithCost.Skip(index).Where(e => !unionFind.IsConnected(e.pointA - 1, e.pointB - 1)).First().cost;
        }

        public static int ComputeLargestKforMaxSpacing()
        {
            var path = @"C:\Users\valto\source\repos\StanfordAlgo\AlgoSF\GreedyAlgo_MST_DM\clustering_big.txt";

            var nodes = new List<int>();

            System.IO.StreamReader file = new System.IO.StreamReader(path);

            var info = file.ReadLine().Split(' ').Select(Int32.Parse).ToList();

            var nbNodes = info[0];

            var bitmasks = CreateArrayBitMask1();
            bitmasks.AddRange(CreateArrayBitMask2());

            for (var i = 0; i < nbNodes; i++){
                var line = file.ReadLine();
                var numList = line.Split(' ').AsEnumerable().Take(info[1]).Select(Int32.Parse).ToList();
                nodes.Add(GetIntegerFromArrayBit(numList));
            }

            nodes = nodes.Distinct().ToList();

            var dic = new Dictionary<int, int>();

            for(var i=0; i< nodes.Count; i++)
            {
                dic.Add(nodes[i], i);
            }

            var unionFind = new SimpleUnionFind(nodes.Count);

            foreach (var mask in bitmasks)
            {
                foreach(var node in nodes)
                {
                    var res = node ^ mask;

                    var exist = dic.TryGetValue(res, out var otherNode);

                    if (exist)
                    {
                        var n = dic[node];
                        unionFind.Merge(n, otherNode);
                    }
                }
            }

            return unionFind.GetCount();

        }


        public static int GetIntegerFromArrayBit(List<int> bits)
        {
            var index = 0;
            return bits.Aggregate(0, (total, next) => {
                total += Convert.ToInt32(Math.Pow(2, index)) * next;
                index++;
                return total;
            }); 
        }

        public static List<int> CreateArrayBitMask1()
        {
            var result = new List<int>();
            var initialValue = new List<int>();

            for(var i=0; i< 24; i++)
            {
                initialValue.Add(0);
            }

            for(var i=0; i < 24; i++)
            {
                var newList = initialValue.ToList();
                newList[i] = 1;
                result.Add(GetIntegerFromArrayBit(newList));
            }

            return result;

        }

        public static List<int> CreateArrayBitMask2()
        {
            var result = new List<int>();
            var initialValue = new List<int>();
            var bitsToWork = new Queue<List<int>>();

            for (var i = 0; i < 25; i++)
            {
                initialValue.Add(0);
            }

            bitsToWork.Enqueue(initialValue);
            while (bitsToWork.Count != 0)
            {
                var actualBitToWork = bitsToWork.Dequeue();
                var indexToCHange = actualBitToWork.Last();
                var noChange = actualBitToWork.ToList();
                var change = actualBitToWork.ToList();
                noChange[noChange.Count - 1] += 1;
                change[change.Count - 1] += 1;
                change[indexToCHange] = 1;


                if((change.Sum() - indexToCHange - 1) == 2)
                {
                    result.Add(GetIntegerFromArrayBit(change.AsEnumerable().Take(24).ToList()));
                }
                else if (indexToCHange + 1 < change.Count - 1)
                {
                    bitsToWork.Enqueue(change);
                }

                if(indexToCHange + 1 < change.Count - 1)
                {
                    bitsToWork.Enqueue(noChange);
                }
            }

            return result;


        }



    }
}
