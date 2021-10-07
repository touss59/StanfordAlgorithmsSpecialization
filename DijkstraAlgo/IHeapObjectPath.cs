using System;
using System.Collections.Generic;

namespace DijkstraAlgo
{
    interface IHeapObjectPath<T>
    {
        int GetId();
        int GetScore();
        void SetScore(int score);
        int CompareTo(T other, bool IsMin);
    }

    class HeapPath<T> where T : IHeapObjectPath<T>
    {
        public int Size = 0;
        private int Capacity = 100;
        private T[] HeapNodes = new T[100];
        private Dictionary<int, int> IndexValueNodes = new Dictionary<int, int> { };
        bool IsMin { get; set; }

        public HeapPath(bool isMin)
        {
            IsMin = isMin;
        }

        private int GetLeftChildIndex(int parentIndex) { return 2 * parentIndex + 1; }
        private int GetRightChildIndex(int parentIndex) { return 2 * parentIndex + 2; }
        private int GetParentIndex(int chilIndex) { return (chilIndex - 1) / 2; }

        private bool HasLeftChild(int index) { return GetLeftChildIndex(index) < Size; }
        private bool HasRightChild(int index) { return GetRightChildIndex(index) < Size; }
        private bool HasParent(int index) { return GetParentIndex(index) >= 0; }

        private T LeftChild(int index) { return HeapNodes[GetLeftChildIndex(index)]; }
        private T RightChild(int index) { return HeapNodes[GetRightChildIndex(index)]; }
        private T Parent(int index) { return HeapNodes[GetParentIndex(index)]; }

        private void Swap(int indexOne, int indexTwo)
        {
            T temp = HeapNodes[indexOne];
            IndexValueNodes[HeapNodes[indexOne].GetId()] = indexTwo;
            IndexValueNodes[HeapNodes[indexTwo].GetId()] = indexOne;
            HeapNodes[indexOne] = HeapNodes[indexTwo];
            HeapNodes[indexTwo] = temp;
        }

        public int GetScoreNode(int idNode)
        {
            int index = IndexValueNodes[idNode];
            return HeapNodes[index].GetScore();
        }

        private void EnsureExtraCapacity()
        {
            if (Size == Capacity)
            {
                Array.Resize(ref HeapNodes, Capacity * 2);
                Capacity *= 2;
            }
        }

        public T Peek()
        {
            return HeapNodes[0];
        }

        public T Poll()
        {
            T node = HeapNodes[0];
            IndexValueNodes.Remove(node.GetId());
            HeapNodes[0] = Size - 1 == 0 ? default : HeapNodes[Size - 1];
            if (HeapNodes[0] != null)
            {
                IndexValueNodes[HeapNodes[0].GetId()] = 0;
            }
            HeapNodes[Size - 1] = default;
            Size--;
            HeapifyDown(0);
            return node;
        }

        public void RemoveNode(int idNode)
        {
            if (Size != 0)
            {
                int index = IndexValueNodes[idNode];
                IndexValueNodes.Remove(idNode);
                T node = HeapNodes[Size - 1];
                IndexValueNodes[node.GetId()] = index;
                HeapNodes[index] = HeapNodes[Size - 1];
                HeapNodes[Size - 1] = default;
                Size--;
                HeapifyDown(index);
            }
        }

        public void Add(T node)
        {
            EnsureExtraCapacity();
            HeapNodes[Size] = node;
            if (IndexValueNodes.ContainsKey(node.GetId()))
            {
                IndexValueNodes[node.GetId()] = Size;
            }
            else
            {
                IndexValueNodes.Add(node.GetId(), Size);
            }
            Size++;
            HeapifyUp();
        }

        private void HeapifyUp()
        {
            int index = Size - 1;
            while (HasParent(index) && Parent(index).CompareTo(HeapNodes[index], IsMin) > 0)
            {
                Swap(GetParentIndex(index), index);
                index = GetParentIndex(index);
            }
        }

        private void HeapifyDown(int index = 0)
        {
            while (HasLeftChild(index))
            {
                int smallerChildIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && RightChild(index).CompareTo(LeftChild(index), IsMin) < 0)
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }

                if (HeapNodes[index].CompareTo(HeapNodes[smallerChildIndex], IsMin) < 0)
                {
                    break;
                }
                else
                {
                    Swap(index, smallerChildIndex);
                }
                index = smallerChildIndex;
            }
        }

        public bool ContainNode(int id)
        {
            if (IndexValueNodes.ContainsKey(id))
            {
                return true;
            }
            return false;
        }

    }
}