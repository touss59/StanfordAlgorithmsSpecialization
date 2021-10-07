using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSF.DataStructures
{
    public class SimpleUnionFind
    {
        private readonly int[] ids;
        private readonly Dictionary<int, List<int>> leaderChildsPair = new();

        public SimpleUnionFind(int N)
        {
            ids = new int[N];

            for (var i = 0; i < N; i++)
            {
                ids[i] = i;
                leaderChildsPair.Add(i, new List<int> {i});
            }
        }

        public int Find(int p)
        {
            return ids[p];
        }

        public void Merge(int x, int y)
        {
            var i = Find(x);
            var j = Find(y);

            if (i == j) return;

            var (keepThisLeader, looseThisLeader) =
                leaderChildsPair[i].Count < leaderChildsPair[j].Count ? (j, i) : (i, j);

            var childsToMove = leaderChildsPair[looseThisLeader];
            childsToMove.ForEach(c => ids[c] = keepThisLeader);

            leaderChildsPair[keepThisLeader].AddRange(childsToMove);
            leaderChildsPair.Remove(looseThisLeader);
        }

        public bool IsConnected(int x, int y)
        {
            return Find(x) == Find(y);
        }

        public int GetCount()
        {
            return leaderChildsPair.Count;
        }
    }
}
