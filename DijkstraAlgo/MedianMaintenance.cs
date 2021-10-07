using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DijkstraAlgo
{
    static class MedianMaintenance
    {
        public static void Solve()
        {
            Heap<int> heapLow = new Heap<int>(false) { };
            Heap<int> heapHigh = new Heap<int>(true) { };
            long result = 0;
            string input;

            while(!string.IsNullOrEmpty(input=Console.ReadLine()))
            {
                int i = Convert.ToInt32(input);
                if (i < heapHigh.Peek())
                {
                    heapLow.Add(i);
                }
                else
                {
                    heapHigh.Add(i);
                }

                if(heapHigh.Size > heapLow.Size + 1)
                {
                    int swapInt = heapHigh.Poll();
                    heapLow.Add(swapInt);
                }
                else if (heapHigh.Size + 1 < heapLow.Size)
                {
                    int swapInt = heapLow.Poll();
                    heapHigh.Add(swapInt);
                }

                if ((heapHigh.Size + heapLow.Size) % 2 == 0)
                {
                    result += heapLow.Peek();
                }
                else
                {
                    if (heapHigh.Size > heapLow.Size)
                    {
                        result += heapHigh.Peek();
                    }
                    else
                    {
                        result += heapLow.Peek();
                    }
                }

            }
            Console.WriteLine(result);
        }
    }

    class Heap<T> where T : IComparable<T>
    {
        public int Size = 0;
        private int Capacity = 10;
        private T[] HeapNodes = new T[10];
        bool IsMin { get; set; }

        public Heap(bool isMin)
        {
            IsMin = isMin;
        }

        private int CompareHeap(int result)
        {
            if (IsMin)
            {
                return result;
            }
            return -result;
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
            HeapNodes[indexOne] = HeapNodes[indexTwo];
            HeapNodes[indexTwo] = temp;
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
            HeapNodes[0] = Size - 1 == 0 ? default : HeapNodes[Size - 1];
            HeapNodes[Size - 1] = default;
            Size--;
            HeapifyDown(0);
            return node;
        }

        public void Add(T node)
        {
            EnsureExtraCapacity();
            HeapNodes[Size] = node;
            Size++;
            HeapifyUp();
        }

        private void HeapifyUp()
        {
            int index = Size - 1;
            while (HasParent(index) && CompareHeap(Parent(index).CompareTo(HeapNodes[index])) > 0)
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
                if (HasRightChild(index) && CompareHeap(RightChild(index).CompareTo(LeftChild(index))) < 0)
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }

                if (CompareHeap(HeapNodes[index].CompareTo(HeapNodes[smallerChildIndex])) < 0)
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
    }
}
