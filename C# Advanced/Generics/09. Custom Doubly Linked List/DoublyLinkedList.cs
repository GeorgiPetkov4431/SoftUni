using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDoublyLinkedList
{
    public class DoublyLinkedList<T>
    {
        private class ListNode
        {
            public ListNode Previous { get; set; }
            public ListNode Next { get; set; }
            public T Value { get; set; }

            public ListNode(T value)
            {
                Value = value;
            }
        }

        private ListNode head;
        private ListNode tail;

        public int Count { get; private set; }

        public void AddFirst(T value)
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

        public void AddLast(T value)
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

        public T RemoveFirst()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("List is empty.");
            }

            T removedValue = head.Value;

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

        public T RemoveLast()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("List is empty.");
            }

            T removedValue = tail.Value;

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

        public void ForEach(Action<T> action)
        {
            ListNode currentNode = head;

            while (currentNode != null)
            {
                action(currentNode.Value);
                currentNode = currentNode.Next;
            }
        }

        public T[] ToArray()
        {
            T[] result = new T[Count];

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
