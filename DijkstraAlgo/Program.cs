using NumberInversion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DijkstraAlgo
{
    class Program
    {
        static void Main(string[] args)
        {

            Recorder.Start();
            MedianMaintenance.Solve();
            Recorder.Stop();
        }

        //use heap with deletion

        public static class SolveDijkstra
        {
            public static void Solve()
            {
                List<List<Node>> nodes = new List<List<Node>> { };
                List<string> node;
                string input;

                System.IO.StreamReader file =
                    new System.IO.StreamReader(@"C:\Users\valto\source\repos\StanfordAlgo\DijkstraAlgo\dijkstraInput.txt");

                while (!string.IsNullOrEmpty(input = file.ReadLine()))
                {
                    node = input.Split("\t").ToList();

                    List<Node> nodeToAdd = new List<Node> { };

                    for (var i = 1; i < node.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(node[i]))
                        {
                            int idNode = Convert.ToInt32(node[i].Split(",")[0]);
                            int valPath = Convert.ToInt32(node[i].Split(",")[1]);
                            nodeToAdd.Add(new Node(idNode, valPath));
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

            private static int ComputeShortestPath(int u, int v, List<List<Node>> nodes)
            {
                List<Node> X = new List<Node> { };
                SortedList<int, int> keyValues = new SortedList<int, int> { };
                Node node = new Node(u, 0);
                keyValues.Add(node.Score, node.Id);

                while (keyValues.Count> 0 && !X.Where(x => x.Id == v).Any())
                {
                    (int scoreK, int id) = keyValues.First();
                    keyValues.Remove(scoreK);
                    node = new Node(id, scoreK);
                    X.Add(node);

                    foreach (var n in nodes[node.Id - 1])
                    {
                        if (!X.Where(x => x.Id == n.Id).Any())
                        {
                            int score = n.Score + node.Score;
                            if (keyValues.Any(item => item.Value == n.Id))
                            {
                                var actualScore = keyValues.Where(x => x.Value == n.Id).First();
                                //score = actualScore > score ? score :  actualScore;
                                keyValues.Remove(n.Score);
                            }
                            keyValues.Add(n.Score, n.Id);
                        }
                    }
                }

                return X.Where(x => x.Id == v).FirstOrDefault()?.Score ?? -1;
            }

            private class Node
            {
                public int Id { get; set; }
                public int Score { get; set; }

                public Node(int id, int score)
                {
                    Id = id;
                    Score = score;
                }

                public int GetId()
                {
                    return Id;
                }

                public int GetScore()
                {
                    return Score;
                }

                public void SetScore(int score)
                {
                    Score = score;
                }

                public int CompareTo(Node other, bool isMin)
                {
                    if (other == null)
                    {
                        return 1;
                    }
                    return isMin ? Score.CompareTo(other.Score) : -Score.CompareTo(other.Score);
                }
            }
        }
    }
}
