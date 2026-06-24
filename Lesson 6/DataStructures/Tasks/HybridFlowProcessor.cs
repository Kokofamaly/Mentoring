using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        Node<T> Head{ get; set; }
        Node<T> Tail { get; set; }        
        public T Dequeue()
        {
            var tempNode = Head;

            if(Head == null)
            {
                throw new InvalidOperationException();
            }
            else if(Head == Tail)
            {
                Head = Tail = null;
            }
            else
            {
                Head = Head.Next;
                Head.Previous = null;
            }

            return tempNode.Value;
        }

        public void Enqueue(T item)
        {
            var newNode = new Node<T>(item);
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
        }

        public T Pop()
        {
            var tempNode = Tail;
            if(Tail == null) throw new InvalidOperationException();
            if(Tail == Head)
            {
                Tail = Head = null;
            }
            else
            {
                Tail = Tail.Previous;
                Tail.Next = null;
            }
            return tempNode.Value;
        }

        public void Push(T item)
        {
            var newNode = new Node<T>(item);
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
        }
    }
}
