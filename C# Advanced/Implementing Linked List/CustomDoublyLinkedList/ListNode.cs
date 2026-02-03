using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDoublyLinkedList
{
    public class ListNode
    {
        public ListNode Previous { get; set; }
        public ListNode Next { get; set; }
        public int Value { get; set; }

        public ListNode(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"{Previous?.Value} <- {Value} -> {Next?.Value}";
        }
    }
}
