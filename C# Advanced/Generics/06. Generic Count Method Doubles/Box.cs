using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GenericCountMethodDoubles
{
    public class Box<T> : IComparable<Box<T>> where T : IComparable<T>
    {
        public Box(T value)
        {
            Value = value;
        }

        public T Value { get; set; }

        public int CompareTo(Box<T> comparable)
        {
            return Value.CompareTo(comparable.Value);
        }

        public override string ToString()
        {
            return $"{typeof(T).FullName}: {Value}";
        }
    }
}
