using System;
using UnityEngine;

namespace MyDataStructures
{
    public class DynamicArray<T>
    {
        protected T[] Data;
        protected int CountValue;

        public int Count => CountValue;
        public int Capacity => Data.Length;

        public DynamicArray()
        {
            Data = new T[4];
            CountValue = 0;
        }

        public DynamicArray(int capacity)
        {
            if (capacity < 1)capacity = 1;

            Data = new T[capacity];
            CountValue = 0;
        }

        public void Add(T item)
        {
            if (CountValue == Data.Length)
            {
                Resize(Data.Length * 2);
            }

            Data[CountValue++] = item;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= CountValue)
                throw new IndexOutOfRangeException();

            for (int i = index; i < CountValue - 1; i++)
                Data[i] = Data[i + 1];

            CountValue--;

            if (CountValue > 0 && CountValue <= Data.Length / 4)
                Resize(Data.Length / 2);
        }

        public T Get(int index)
        {
            if (index < 0 || index >= CountValue)
                throw new IndexOutOfRangeException();
            return Data[index];
        }

        public void Set(int index, T value)
        {
            if (index < 0 || index >= CountValue)
                throw new IndexOutOfRangeException();
            Data[index] = value;
        }

        protected void Resize(int newCapacity)
        {
            if (newCapacity < 1) newCapacity = 1;
            T[] newData = new T[newCapacity];
            for (int i = 0; i < CountValue; i++)
                newData[i] = Data[i];
            Data = newData;
        }

        public void Print()
        {
            for (int i = 0; i < CountValue; i++)
                Debug.Log(Data[i]);
        }
    }
}
