using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSF.DivideAndConquer
{
    class Week4
    {
        private static Random rdm = new ();

        public static int ComputeMinCut(List<List<int>> input)
        {
            int mincut = int.MaxValue;
            for (var x = 0; x < input.Count; x++) // need to compute the algo several times to find the best solution
            {
                List<List<int>> groupAdjacentList = input.Select(subList => subList.ToList()).OrderBy(subList => subList[0]).ToList();

                while (groupAdjacentList.Count > 2)
                {
                    ExecuteEdgeContraction(groupAdjacentList);
                }
                if (mincut > groupAdjacentList[0].Count - 1)
                {
                    mincut = groupAdjacentList[0].Count - 1;
                }
            }
            return mincut;
        }

        private static void ExecuteEdgeContraction(List<List<int>> adjacentList)
        {
            //choose random edge
            int indexListA = rdm.Next(0, adjacentList.Count - 1);
            int column = rdm.Next(1, adjacentList[indexListA].Count - 1);

            //Fusion
            int verticeA = adjacentList[indexListA][0];
            int verticeB = adjacentList[indexListA][column];

            for (var i = 0; i < adjacentList.Count; i++)
            {
                if (adjacentList[i][0] == verticeB)
                {
                    var indexListB = i;
                    for (var j = 1; j < adjacentList[indexListB].Count; j++)
                    {
                        if (adjacentList[indexListB][j] != verticeA)
                        {
                            adjacentList[indexListA].Add(adjacentList[indexListB][j]);
                        }
                    }

                    adjacentList[indexListA].RemoveAll(v => v == verticeB);

                    for (var x = 0; x < adjacentList.Count; x++)
                    {
                        for (var y = 0; y < adjacentList[x].Count; y++)
                        {
                            if (adjacentList[x][y] == verticeB)
                            {
                                adjacentList[x][y] = verticeA;
                            }
                        }
                    }

                    adjacentList.RemoveAt(indexListB);
                    break;
                }
            }
        }
    }
}
