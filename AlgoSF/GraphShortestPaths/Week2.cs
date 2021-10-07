using AlgoSF.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoSF.GraphShortestPaths
{
    class Week2
    {
        public static class SolveDijkstra
        {
            public static void Solve()
            {
                var nodes = new List<List<Verticies>>();
                List<string> node;
                string input;

                System.IO.StreamReader file =
                    new System.IO.StreamReader(@"C:\Users\valto\source\repos\StanfordAlgo\DijkstraAlgo\dijkstraInput.txt");

                while (!string.IsNullOrEmpty(input = file.ReadLine()))
                {
                    node = input.Split("\t").ToList();

                    var nodeToAdd = new List<Verticies> { };

                    for (var i = 1; i < node.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(node[i]))
                        {
                            int idNode = Convert.ToInt32(node[i].Split(",")[0]);
                            int valPath = Convert.ToInt32(node[i].Split(",")[1]);
                            nodeToAdd.Add(new Verticies(idNode, valPath));
                        }
                    }
                    nodes.Add(nodeToAdd);
                }
                Console.WriteLine(ComputeShortestPath(1, 7, nodes));
                Console.WriteLine(ComputeShortestPath(1, 37, nodes));
                Console.WriteLine(ComputeShortestPath(1, 59, nodes));
                Console.WriteLine(ComputeShortestPath(1, 82, nodes));
                Console.WriteLine(ComputeShortestPath(1, 99, nodes));
                Console.WriteLine(ComputeShortestPath(1, 115, nodes));
                Console.WriteLine(ComputeShortestPath(1, 133, nodes));
                Console.WriteLine(ComputeShortestPath(1, 165, nodes));
                Console.WriteLine(ComputeShortestPath(1, 188, nodes));
                Console.WriteLine(ComputeShortestPath(1, 197, nodes));


                file.Close();
            }

            private static int ComputeShortestPath(int u, int v, List<List<Verticies>> nodes)
            {
                var verticiesMinPath = new Dictionary<int,Verticies>();
                var minHeap = new MinHeapWithDeletion<Verticies>();
                minHeap.Add(new Verticies(u, 0));

                while (minHeap.Size > 0 && !verticiesMinPath.ContainsKey(v))
                {
                    var minNode = minHeap.Poll();
                    verticiesMinPath.Add(minNode.GetId(),minNode);

                    foreach (var n in nodes[minNode.GetId() - 1])
                    {
                        if (!verticiesMinPath.ContainsKey(n.GetId()))
                        {
                            var newScore = n.GetScore() + minNode.GetScore();
                            var nodeAlreadyAddToHeap = minHeap.TryGetNode(n.GetId(), out var existingNode);
                            var existingScore = nodeAlreadyAddToHeap ? existingNode.GetScore() : int.MaxValue;

                            if (existingScore > newScore)
                            {
                                minHeap.RemoveNode(n.GetId());
                                minHeap.Add(new Verticies(n.GetId(), newScore));
                            }
                        }
                    }
                }

                return verticiesMinPath.TryGetValue(v, out var result) ? result.GetScore() : -1;
            }
        }
    }
}
