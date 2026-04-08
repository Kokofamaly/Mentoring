using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        Node<T> Head{ get; set; }
        Node<T> Tail { get; set; }
        private int _length;
        public int Length => _length;

        

        public void Add(T e)
        {
            if(Head == null)
            {
                Head = new Node<T>(e);
                _length = 1;
            }
            else if(Head.Next == null)
            {
                var node = new Node<T>(e);
                Head.Next = node;
                node.Previous = Head;
                Tail = node;
                _length = 2;
            }
            else
            {
                var node = new Node<T>(e);
                Tail.Next = node;
                node.Previous = Tail;
                Tail = node;
                _length++;
            }
        }

        public void AddAt(int index, T e)
        {
            if(index < 0 || index >= Length) throw new IndexOutOfRangeException();

            var currentNode = Head;
            for(int i = 0; i < Length; i++)
            {
                if(i == index)
                {
                    var newNode = new Node<T>(e);
                    newNode.Next = currentNode;
                    currentNode.Previous.Next = newNode;
                    newNode.Previous = currentNode.Previous;
                    currentNode.Previous = newNode;
                    _length++;
                    break;
                }
                currentNode = currentNode.Next;

            }
        }

        public T ElementAt(int index)
        {
            if(index < 0 || index >= Length) throw new IndexOutOfRangeException();
            
            var currentNode = Head;
            for(int i = 0; i < Length; i++)
            {
                if(i == index)
                {
                    return currentNode.Value;
                }
                currentNode = currentNode.Next;

            }
            return default;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkelListEnumerator<T>(Head);
        }

        public void Remove(T item)
        {
            var currentNode = Head;
            for(int i = 0; i < Length; i++)
            {
                if(EqualityComparer<T>.Default.Equals(item, currentNode.Value))
                {
                    currentNode.Previous.Next = currentNode.Next;
                    currentNode.Next.Previous = currentNode.Previous;
                    break;
                }
                currentNode = currentNode.Next;
            }
        }

        public T RemoveAt(int index)
        {
            if(index < 0 || index >= Length) throw new IndexOutOfRangeException();

            var currentNode = Head;
            for(int i = 0; i < Length; i++)
            {
                if(i == index)
                {
                    currentNode.Previous.Next = currentNode.Next;
                    currentNode.Next.Previous = currentNode.Previous;
                    return currentNode.Value;
                }
                currentNode = currentNode.Next;

            }
            return default;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();            
        }
    }
}
