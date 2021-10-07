using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSF.DataStructures
{
    public class MinHeapWithDeletion<T> : MinHeap<T> where T : IHeapObjectPath, IComparable<T>
    {
        private readonly Dictionary<int, int> IndexValueNodes = new();

        protected override void Swap(int indexOne, int indexTwo)
        {
            T temp = HeapNodes[indexOne];
            IndexValueNodes[HeapNodes[indexOne].GetId()] = indexTwo;
            IndexValueNodes[HeapNodes[indexTwo].GetId()] = indexOne;
            HeapNodes[indexOne] = HeapNodes[indexTwo];
            HeapNodes[indexTwo] = temp;
        }

        public override T Poll()
        {
            T node = HeapNodes[0];
            if (node != null)
            {
                IndexValueNodes.Remove(node.GetId());
                HeapNodes[0] = Size - 1 == 0 ? default : HeapNodes[Size - 1];
                if (HeapNodes[0] != null)
                {
                    IndexValueNodes[HeapNodes[0].GetId()] = 0;
                }
                HeapNodes[Size - 1] = default;
                Size--;
                HeapifyDown(0);
            }
            return node;
        }

        public override void Add(T node)
        {
            EnsureExtraCapacity();
            HeapNodes[Size] = node;
            IndexValueNodes.Add(node.GetId(), Size);
            Size++;
            HeapifyUp();
        }

        public int GetScoreNode(int idNode)
        {
            int index = IndexValueNodes[idNode];
            return HeapNodes[index].GetScore();
        }

        public void RemoveNode(int idNode)
        {
            if (Size != 0 && ContainNode(idNode))
            {
                int index = IndexValueNodes[idNode];
                IndexValueNodes.Remove(idNode);
                T node = HeapNodes[Size - 1];
                HeapNodes[Size - 1] = default;
                Size--;

                if (index < Size)
                {

                    IndexValueNodes[node.GetId()] = index;
                    HeapNodes[index] = node;

                    if (HasParent(index) && Parent(index).CompareTo(HeapNodes[index]) > 0)
                    {
                        HeapifyUp(index);
                    }
                    else
                    {
                        HeapifyDown(index);
                    }
                }
            }
        }

        public bool ContainNode(int id)
        {
            return IndexValueNodes.ContainsKey(id);
        }

        public T GetNode(int id)
        {
            return HeapNodes[IndexValueNodes[id]];
        }

        public bool TryGetNode(int id, out T node)
        {
            var exist = IndexValueNodes.TryGetValue(id, out var indexNode);
            node = exist ? HeapNodes[indexNode] : default;
            return exist;
        }


    }

    public interface IHeapObjectPath
    {
        int GetId();
        int GetScore();
        void SetScore(int score);
    }


    public class Verticies : IHeapObjectPath, IComparable<Verticies>
    {
        private readonly int id;
        private int score;

        public Verticies(int id, int score)
        {
            this.id = id;
            this.score = score;
        }

        public int CompareTo(Verticies other)
        {
            if (other == null)
            {
                return 1;
            }
            return this.score.CompareTo(other.score);
        }

        public int GetId() => id;

        public int GetScore() => score;

        public void SetScore(int score)
        {
            this.score = score;
        }

    }

}
