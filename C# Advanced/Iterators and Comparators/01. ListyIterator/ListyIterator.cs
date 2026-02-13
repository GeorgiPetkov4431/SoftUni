using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListyIterator
{
    public class ListyIterator<T> : IEnumerator<T>
    {
        private List<T> data;
        private int index;

        public ListyIterator(List<T> inputData)
        {
            data = inputData;
            index = 0;
        }

        public T Current => data[index];

        object IEnumerator.Current => Current;

        public void Create(params T[] data)
        {
            this.data = new List<T>(data);
        }

        public void Dispose()
        {
            //
        }

        public void Print()
        {
            if (data.Count == 0) throw new InvalidOperationException("Invalid Operation!");
            else Console.WriteLine(Current);
        }

        public bool MoveNext()
        {
            if (index < data.Count - 1)
            {
                index++;
                return true;
            }

            return false;
        }

        public bool HasNext()
        {
            return index + 1 < data.Count;
        }

        public void Reset()
        {
            index = 0;
        }
    }
}
