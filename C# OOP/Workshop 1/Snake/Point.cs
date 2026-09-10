using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Point
    {
        public Point(int left, int top)
        {
            this.Top = top;
            this.Left = left;
        }

        public int Top { get; set; }

        public int Left { get; set; }

        public override string ToString()
        {
            return $"({this.Left}, {this.Top})";
        }
    }
}
