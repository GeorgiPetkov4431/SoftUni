using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    public class CustomStack<T> : IEnumerable<T>
    {
        private List<T> data;

        public CustomStack()
        {
            data = new List<T>();
        }

        public void Push(params T[] item)
        {
            data.AddRange(item);
        }

        public T Pop()
        {
            if (data.Count == 0) throw new InvalidOperationException("No elements");

            T topItem = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);
            return topItem;
        }

        public T Peek()
        {
            return data[data.Count - 1];
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = data.Count - 1; i >= 0; i--)
            {
                yield return data[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
