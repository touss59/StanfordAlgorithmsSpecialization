using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSF.GraphShortestPaths
{
    class Week1
    {
        public static Dictionary<int, int> fLabel = new();

        public static List<int> ComputeSCCs()
        {

            Dictionary<int, List<int>> graph = new();
            Dictionary<int, List<int>> reverseGraph = new();

            using (var reader = new StreamReader(@"C:\Users\valto\source\repos\StanfordAlgo\AlgoSF\GraphShortestPaths\SccsInput.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var tail = Convert.ToInt32(line.Split()[0]);
                    var head = Convert.ToInt32(line.Split()[1]);

                    if (graph.ContainsKey(tail))
                    {
                        graph[tail].Add(head);
                    }
                    else
                    {
                        graph.Add(tail, new List<int> { head });
                    }
                }
            }

            //Reverse Graph
            foreach (var (tail, values) in graph)
            {
                foreach (var head in values)
                {
                    if (reverseGraph.ContainsKey(head))
                    {
                        reverseGraph[head].Add(tail);
                    }
                    else
                    {
                        reverseGraph.Add(head, new List<int> {tail});
                    }
                }
            }

            DFSfirstLoop(reverseGraph);
            return DFSsecondLoop(graph);
        }

        public static void DFSfirstLoop(Dictionary<int, List<int>> reverseGraph)
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

        public static List<int> DFSsecondLoop(Dictionary<int, List<int>> graph)
        {
            HashSet<int> nodesExplored = new ();

            List<int> bigerGroups = new() {0, 0, 0, 0, 0};

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

            return bigerGroups;
        }

    }
}
