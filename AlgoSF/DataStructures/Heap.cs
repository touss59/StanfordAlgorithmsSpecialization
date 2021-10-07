using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSF.DataStructures
{
    public abstract class BaseHeap<T> where T : IComparable<T>
    {
        public int Size = 0;
        protected int Capacity = 100;
        protected T[] HeapNodes = new T[100];
        protected int GetLeftChildIndex(int parentIndex) { return 2 * parentIndex + 1; }
        protected int GetRightChildIndex(int parentIndex) { return 2 * parentIndex + 2; }
        protected int GetParentIndex(int chilIndex) { return (chilIndex - 1) / 2; }

        protected bool HasLeftChild(int index) { return GetLeftChildIndex(index) < Size; }
        protected bool HasRightChild(int index) { return GetRightChildIndex(index) < Size; }
        protected bool HasParent(int index) { return GetParentIndex(index) >= 0; }

        protected T LeftChild(int index) { return HeapNodes[GetLeftChildIndex(index)]; }
        protected T RightChild(int index) { return HeapNodes[GetRightChildIndex(index)]; }
        protected T Parent(int index) { return HeapNodes[GetParentIndex(index)]; }

        protected virtual void Swap(int indexOne, int indexTwo)
        {
            T temp = HeapNodes[indexOne];
            HeapNodes[indexOne] = HeapNodes[indexTwo];
            HeapNodes[indexTwo] = temp;
        }

        protected void EnsureExtraCapacity()
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

        public virtual T Poll()
        {
            T node = HeapNodes[0];
            HeapNodes[0] = Size - 1 == 0 ? default : HeapNodes[Size - 1];
            HeapNodes[Size - 1] = default;
            Size--;
            HeapifyDown(0);
            return node;
        }

        public virtual void Add(T node)
        {
            EnsureExtraCapacity();
            HeapNodes[Size] = node;
            Size++;
            HeapifyUp();
        }

        protected abstract void HeapifyUp(int index = int.MaxValue);

        protected abstract void HeapifyDown(int index = 0);
    }


    public class MinHeap<T> : BaseHeap<T> where T : IComparable<T>
    {
        protected override void HeapifyUp(int index = int.MaxValue)
        {
            index = index == int.MaxValue ? Size - 1 : index;
            while (HasParent(index) && Parent(index).CompareTo(HeapNodes[index]) > 0)
            {
                Swap(GetParentIndex(index), index);
                index = GetParentIndex(index);
            }
        }

        protected override void HeapifyDown(int index = 0)
        {
            while (HasLeftChild(index))
            {
                int smallerChildIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && RightChild(index).CompareTo(LeftChild(index)) < 0)
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }

                if (HeapNodes[index].CompareTo(HeapNodes[smallerChildIndex]) < 0)
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

    public class MaxHeap<T> : BaseHeap<T> where T : IComparable<T>
    {
        protected override void HeapifyUp(int index = int.MaxValue)
        {
            index = index == int.MaxValue ? Size - 1 : index;
            while (HasParent(index) && Parent(index).CompareTo(HeapNodes[index]) < 0)
            {
                Swap(GetParentIndex(index), index);
                index = GetParentIndex(index);
            }
        }

        protected override void HeapifyDown(int index = 0)
        {
            while (HasLeftChild(index))
            {
                int smallerChildIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && RightChild(index).CompareTo(LeftChild(index)) > 0)
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }

                if (HeapNodes[index].CompareTo(HeapNodes[smallerChildIndex]) > 0)
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
