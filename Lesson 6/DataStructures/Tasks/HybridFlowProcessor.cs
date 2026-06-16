using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        DoublyLinkedList<T> doublyList = new DoublyLinkedList<T>();
        public T Dequeue()
        {
            if(doublyList.Length == 0) throw new InvalidOperationException();   
            return doublyList.RemoveAt(0);
        }

        public void Enqueue(T item)
        {
            doublyList.Add(item);
        }

        public T Pop()
        {
            if(doublyList.Length == 0) throw new InvalidOperationException();   
            return doublyList.RemoveAt(doublyList.Length - 1);
        }

        public void Push(T item)
        {
            doublyList.Add(item);
        }
    }
}
