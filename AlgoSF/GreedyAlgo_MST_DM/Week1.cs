using System;
using System.Collections.Generic;
using System.Linq;
using AlgoSF.DataStructures;

namespace AlgoSF.GreedyAlgo_MST_DM
{
    public static class Week1
    {
        public static long CalculateMinCostJobWithDiff() 
            => CalculateCostOfJobs(GetJobsInputFromTXT().OrderByDescending(j => j.w - j.l).ThenByDescending(j => j.w).ToList());
        

        public static long CalculateMinCostJobWithRatio() 
            => CalculateCostOfJobs(GetJobsInputFromTXT().OrderByDescending(j => Convert.ToDecimal(j.w) / Convert.ToDecimal(j.l)).ToList());


        public static long CalculateMSTWithSpeedPrim()
        {
            var (nbV, edges) = GetEdgesFromTXT(@"C:\Users\valto\source\repos\StanfordAlgo\AlgoSF\GreedyAlgo_MST_DM\edges.txt");
            var verticies = GetVerticies(nbV);
            var verticiesInMST = new HashSet<int>() { 1 };
            var heap = new MinHeapWithDeletion<Verticies>();
            var globalCost = 0;

            for(var i = 2; i <= nbV; i++)
            {
                var actualVertice = verticies[i - 1];
                var minEdge = edges.Where(e => (e.pointA == i && e.pointB == 1) || (e.pointA == 1 && e.pointB == i)).OrderBy(e => e.cost).FirstOrDefault();
                if (!minEdge.Equals(default))
                {
                    actualVertice.SetScore(minEdge.cost);
                }
                heap.Add(verticies[i-1]);
            }

            while(heap.Size != 0)
            {
                var minVerticies = heap.Poll();

                verticiesInMST.Add(minVerticies.GetId());
                globalCost += minVerticies.GetScore();
                edges.RemoveAll(e => verticiesInMST.Contains(e.pointA) && verticiesInMST.Contains(e.pointB));

                var newEdges = edges.Where(e => e.pointA == minVerticies.GetId() || e.pointB == minVerticies.GetId()).ToList();

                foreach(var newEdge in newEdges)
                {
                    var concernV = verticies[newEdge.pointA-1] == minVerticies ? verticies[newEdge.pointB-1] : verticies[newEdge.pointA-1];
                    if(concernV.GetScore() > newEdge.cost)
                    {
                        concernV.SetScore(newEdge.cost);
                        heap.RemoveNode(concernV.GetId());
                        heap.Add(concernV);
                    }
                }

            }

            return globalCost;
            
        }

        public static long CalculateMSTWithSlowPrim()
        {
            var (nbV, edges) = GetEdgesFromTXT(@"C:\Users\valto\source\repos\StanfordAlgo\AlgoSF\GreedyAlgo_MST_DM\edges.txt");

            var verticiesInMST = new HashSet<int>() { 1 };

            var edgesInMST = new List<(int pointA, int pointB, int cost)>();

            while (verticiesInMST.Count != nbV)
            {
                var cheapestEdge = edges.Where(e => (verticiesInMST.Contains(e.pointA) && !verticiesInMST.Contains(e.pointB))
                                                 || (verticiesInMST.Contains(e.pointB) && !verticiesInMST.Contains(e.pointA)))
                                        .OrderBy(e => e.cost)
                                        .First();

                edgesInMST.Add(cheapestEdge);
                verticiesInMST.Add(cheapestEdge.pointA);
                verticiesInMST.Add(cheapestEdge.pointB);
            }

            return edgesInMST.Sum(e => e.cost);
        }

        public static long CalculateCostOfJobs(List<(int w, int l)> input)
        {
            long sumTime = 0;
            long cost = 0;

            foreach (var job in input)
            {
                cost += job.w * (job.l + sumTime);
                sumTime += job.l;
            }

            return cost;
        }

        public static List<Verticies> GetVerticies(int nb)
        {
            var verticies = new List<Verticies>();

            for(var i = 1; i <= nb; i++)
            {
                verticies.Add(new Verticies(i, int.MaxValue));
            }

            return verticies;
        }

        public static List<(int w,int l)> GetJobsInputFromTXT()
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\valto\source\repos\StanfordAlgo\AlgoSF\GreedyAlgo_MST_DM\jobs.txt");

            int numberOfJobs = Convert.ToInt32(file.ReadLine());

            var jobs = new List<(int w, int l)>();

            for (var i = 0; i < numberOfJobs; i++)
            {
                var job = file.ReadLine();
                var jobInfo = job.Split(' ').Select(Int32.Parse).ToList();

                jobs.Add((jobInfo[0], jobInfo[1]));
            }

            return jobs;

        }
        
        public static (int verticies, List<(int pointA, int pointB, int cost)> edges) GetEdgesFromTXT(string path)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(path);

            var info = file.ReadLine().Split(' ').Select(Int32.Parse).ToList();
            var numberOfVerticies = info[0];
            var numberOfEdges = info[1];

            var edges = new List<(int pointA, int pointB, int cost)>();

            for (var i = 0; i < numberOfEdges; i++)
            {
                var edge = file.ReadLine();
                var edgeInfo = edge.Split(' ').Select(Int32.Parse).ToList();

                edges.Add((edgeInfo[0], edgeInfo[1], edgeInfo[2]));
            }

            return (numberOfVerticies, edges);
        }
    }
}
