using System;
using AlgoSF.DataStructures;

namespace AlgoSF
{
    class Program
    {
        static void Main(string[] args)
        {
            AlgoSF.Helpers.Recorder.Start();
            AlgoSF.GraphShortestPaths.Week2.SolveDijkstra.Solve();
            AlgoSF.Helpers.Recorder.Stop();
        }
    }
}
