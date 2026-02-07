using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniDataStructures
{
    public class SoftUniList
    {
        private int[] internalArray;
        private int index;
        private int capacity;
        private int resizeCount;

        public SoftUniList(int capacity = 4)
        {
            internalArray = new int[capacity];
            resizeCount = 0;
            index = 0;
        }

        public int Count { get { return index; } }

        public int this[int i]
        {
            get 
            { 
                CheckIndex(i);
                return internalArray[i];
            }
            set
            {
                CheckIndex(i);
                internalArray[i] = value;
            }
        }

        public void ForEach(Action<int> action)
        {
            foreach (int item in internalArray)
            {
                action(item);
            }
        }

        public void Add(int value)
        {
            internalArray[index++] = value;

            if (index >= internalArray.Length)
            {
                Resize();
            }
        }

        public void Push(int value)
        {
            Add(value);
        }

        public int Pop()
        {
            int element = internalArray[index - 1];
            RemoveAt(index - 1);
            return element;
        }

        public void RemoveAt(int indexToRemove)
        {
            CheckIndex(indexToRemove);

            for (int i = indexToRemove; i < index; i++)
            {
                internalArray[i] = internalArray[i + 1];
            }

            internalArray[index - 1] = default;
            index--;
        }

        public int GetAt(int indexValue)
        {
            CheckIndex(indexValue);

            return internalArray[indexValue];
        }

        public bool Contains(int element)
        {
            bool contains = false;

            for (int i = 0; i < index; i++)
            {
                if (internalArray[i] == element)
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }

        private void Resize()
        {
            Console.WriteLine($"Count: {Count}. Resizing: {resizeCount++}");

            int[] newArray = new int[internalArray.Length * 2];

            for (int i = 0; i < internalArray.Length; i++)
            {
                newArray[i] = internalArray[i];
            }

            internalArray = newArray;
        }

        private void CheckIndex(int i)
        {
            if (i >= index || i < 0)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}
