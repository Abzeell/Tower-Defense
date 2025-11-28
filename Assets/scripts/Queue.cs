using System;
using UnityEngine;

namespace MyDataStructures
{
    public class Queue<T> : DynamicArray<T>
    {
        public Queue() : base() { }

        public Queue(int capacity) : base(capacity) { }

        public void Enqueue(T item)
        {
            Add(item);
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("Queue is empty.");

            T item = Data[0];
            RemoveAt(0);
            return item;
        }

        public bool IsEmpty()
        {
            if(Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Clear()
        {
            // Clear references to allow Unity’s GC to clean up (especially if T is a GameObject)
            for (int i = 0; i < CountValue; i++)
                Data[i] = default;

            // Reset count safely
            CountValue = 0;
        }

        public void PrintQueue()
        {
            for (int i = 0; i < Count; i++)
                Debug.Log(Data[i]);
        }
    }
}
