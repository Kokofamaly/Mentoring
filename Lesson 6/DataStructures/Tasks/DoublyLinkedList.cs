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
        private int _length = 0;
        public int Length => _length;

        

        public void Add(T e)
        {
            var newNode = new Node<T>(e);
            if(Head == null)
            {
                Head = Tail = newNode;
            }
            else
            {
                Tail.Next = newNode;
                newNode.Previous = Tail;
                Tail = newNode;
            }
                _length++;
        }

        public void AddAt(int index, T e)
        {
            if(index < 0 || index > Length) throw new IndexOutOfRangeException();
            
            var newNode = new Node<T>(e);
            if(Head == null)
            {
                Head = Tail = newNode;
            }
            else if(index == 0)
            {
                newNode.Next = Head;
                Head.Previous = newNode;
                Head = newNode;
            }
            else if(index == Length)
            {
                newNode.Previous = Tail;
                Tail.Next = newNode;
                Tail = newNode;
            }
            else
            {
                var currentNode = Head;
                for(int i = 0; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }
                newNode.Next = currentNode;
                currentNode.Previous.Next = newNode;
                newNode.Previous = currentNode.Previous;
                currentNode.Previous = newNode;
            }
            _length++;
        }

        public T ElementAt(int index)
        {
            if(index < 0 || index >= Length) throw new IndexOutOfRangeException();
            
            var currentNode = Head;
            for(int i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }
            return currentNode.Value;
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
                    if(Head == Tail)
                    {
                        Head = Tail = null;
                    }
                    else if(Head == currentNode)
                    {
                        Head = Head.Next;
                        Head.Previous = null;
                    }
                    else if(Tail == currentNode)
                    {
                        Tail = Tail.Previous;
                        Tail.Next = null;
                    }
                    else
                    {
                        currentNode.Previous.Next = currentNode.Next;
                        currentNode.Next.Previous = currentNode.Previous;
                    } 
                    _length--;
                    return;
                }
                currentNode = currentNode.Next;
            }
        }

        public T RemoveAt(int index)
        {
            if(index < 0 || index >= Length) throw new IndexOutOfRangeException();

            var currentNode = Head;
            for(int i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }

            if(Head == Tail)
            {
                Head = Tail = null;
            }
            else if(Head == currentNode)
            {
                Head = Head.Next;
                Head.Previous = null;
            }
            else if(Tail == currentNode)
            {
                Tail = Tail.Previous;
                Tail.Next = null;
            }
            else
            {
                currentNode.Previous.Next = currentNode.Next;
                currentNode.Next.Previous = currentNode.Previous;
            }
            _length--;
            return currentNode.Value;

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();            
        }
    }
}
