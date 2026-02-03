using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDoublyLinkedList
{
    public class DoublyLinkedList
    {
        private ListNode head;
        private ListNode tail;

        public int Count { get; private set; }

        public void AddFirst(int value)
        {
            ListNode newHead = new ListNode(value);

            if (Count == 0)
            {
                this.head = newHead;
                this.tail = newHead;

                Count++;

                return;
            }

            head.Previous = newHead;
            newHead.Next = head;
            head = newHead;
            Count++;
        }

        public void AddLast(int value)
        {
            ListNode newTail = new ListNode(value);

            if (Count == 0)
            {
                this.head = newTail;
                this.tail = newTail;

                Count++;

                return;
            }

            tail.Next = newTail;
            newTail.Previous = tail;
            tail = newTail;
            Count++;
        }

        public int RemoveFirst()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("List is empty.");
            }

            int removedValue = head.Value;

            head = head.Next;

            if (head != null)
            {
                head.Previous = null;
            }
            else
            {
                tail = null;
            }

            Count--;

            return removedValue;
        }

        public int RemoveLast()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("List is empty.");
            }

            int removedValue = tail.Value;

            tail = tail.Previous;

            if (tail != null)
            {
                tail.Next = null;
            }
            else
            {
                head = null;
            }

            return removedValue;
        }

        public void ForEach(Action<int> action)
        {
            ListNode currentNode = head;

            while (currentNode != null)
            {
                action(currentNode.Value);
                currentNode = currentNode.Next;
            }
        }

        public int[] ToArray()
        {
            int[] result = new int[Count];

            ListNode currentNode = head;

            int counter = 0;
            while (currentNode != null)
            {
                result[counter] = currentNode.Value;
                currentNode = currentNode.Next;
                counter++;
            }

            return result;
        }
    }
}
