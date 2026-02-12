using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericSwapMethodIntegers
{
    public class BoxList<T>
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
    }
}
