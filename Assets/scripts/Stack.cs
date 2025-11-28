using System;
using UnityEngine;

namespace MyDataStructures
{
    public class Stack<T> : DynamicArray<T>
    {
        public Stack(int capacity = 4) : base(capacity) { }

        public void Push(T item)
        {
            Add(item);
        }

        public T Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException("Stack is empty.");

            T item = Data[Count - 1];
            CountValue--;

            if (CountValue > 0 && CountValue <= Data.Length / 4)
                Resize(Data.Length / 2);

            return item;
        }

        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("Stack is empty.");
            return Data[Count - 1];
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public void PrintStack()
        {
            for (int i = 0; i < Count; i++)
                Debug.Log(Data[i]);
        }
    }
}
