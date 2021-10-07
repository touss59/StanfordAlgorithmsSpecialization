using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SCCs
{
    class Program
    {
        public static Dictionary<int, List<int>> graph = new Dictionary<int, List<int>> { };

        public static Dictionary<int, int> fLabel = new Dictionary<int, int> { };

        public static Dictionary<int, List<int>> reverseGraph = new Dictionary<int, List<int>> { };

        public static List<int> bigerGroups = new List<int> { 0, 0, 0, 0, 0 };

        static void Main(string[] args)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"C:\Users\valto\source\repos\StanfordAlgo\SCCs\SccsInput.txt");
            while ((line = file.ReadLine()) != null)
            {
                int nowNode = Convert.ToInt32(line.Split()[0]);
                int nextNode = Convert.ToInt32(line.Split()[1]);

                if (graph.ContainsKey(nowNode))
                {
                    graph[nowNode].Add(nextNode);
                }
                else
                {
                    graph.Add(nowNode, new List<int> { nextNode });
                }
                counter++;
            }

            foreach (var node in graph)
            {
                foreach (var nextNode in node.Value)
                {
                    if (reverseGraph.ContainsKey(nextNode))
                    {
                        reverseGraph[nextNode].Add(node.Key);
                    }
                    else
                    {
                        reverseGraph.Add(nextNode, new List<int> { node.Key });
                    }
                }
            }



            file.Close();
            DFSfirstLoop();
            DFSsecondLoop();
            Console.WriteLine("There were {0} lines.", counter);
        }

        public static void DFSfirstLoop()
        {
            int count = 1;
            HashSet<int> nodesExplored = new HashSet<int> { };

            foreach (var node in reverseGraph)
            {
                if (!nodesExplored.Contains(node.Key))
                {
                    Stack<int> nodeStack = new Stack<int> { };
                    nodeStack.Push(node.Key);
                    nodesExplored.Add(node.Key);

                    while (nodeStack.Count() > 0)
                    {
                        int nodeValue = nodeStack.Peek();
                        var listNextNode = reverseGraph.ContainsKey(nodeValue) != false ? reverseGraph[nodeValue].Where(x => !nodesExplored.Contains(x)).ToList() : null;
                        if (listNextNode == null || listNextNode.Count == 0)
                        {
                            var sinkNode = nodeStack.Pop();
                            fLabel.Add(sinkNode, count);
                            count += 1;
                        }
                        else
                        {
                            foreach (var nextNode in listNextNode)
                            {
                                nodeStack.Push(nextNode);
                                nodesExplored.Add(nextNode);
                            }
                        }
                    }

                }
            }
        }

        public static void DFSsecondLoop()
        {
            HashSet<int> nodesExplored = new HashSet<int> { };

            fLabel = fLabel.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            foreach (var node in fLabel)
            {
                if (!nodesExplored.Contains(node.Key))
                {
                    int count = 1;
                    Stack<int> nodeStack = new Stack<int> { };
                    nodeStack.Push(node.Key);
                    nodesExplored.Add(node.Key);

                    while (nodeStack.Count > 0)
                    {
                        var keynewNode = nodeStack.Pop();
                        if (graph.ContainsKey(keynewNode))
                        {
                            var newNode = graph[keynewNode];

                            foreach (var nextNode in newNode)
                            {
                                if (!nodesExplored.Contains(nextNode))
                                {
                                    nodeStack.Push(nextNode);
                                    nodesExplored.Add(nextNode);
                                    count++;
                                }
                            }
                        }
                    }
                    bigerGroups = bigerGroups.OrderBy(x => x).ToList();
                    if (bigerGroups[0] < count)
                    {
                        bigerGroups[0] = count;
                    }
                }
            }
        }

    }
}
