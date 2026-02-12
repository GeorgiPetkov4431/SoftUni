using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCountMethodStrings
{
    public class BoxList<T> where T : IComparable<T>
    {
        public BoxList()
        {
            Boxes = new List<Box<T>>();
        }

        public List<Box<T>> Boxes { get; set; }

        public void Add(Box<T> box)
        {
            Boxes.Add(box);
        }

        public void Swap(int firstIndex, int secondIndex)
        {
            Box<T> temp = Boxes[firstIndex];
            Boxes[firstIndex] = Boxes[secondIndex];
            Boxes[secondIndex] = temp;
        }

        public int Compare(Box<T> element)
        {
            List<Box<T>> boxes = Boxes.Where(b => b.CompareTo(element) > 0).ToList();

            return boxes.Count;
        }
    }
}
