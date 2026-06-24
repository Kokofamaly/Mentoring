using System;
using System.Collections.Generic;

namespace Tasks
{
    public class DoublyLinkelListEnumerator<T> : IEnumerator<T>
    {
        Node<T> head;
        Node<T> current;

        public T Current
        {
            get
            {
                if(current == null)
                {
                    throw new InvalidOperationException();
                }
                return current.Value;
            }
        }

        public DoublyLinkelListEnumerator(Node<T> head)
        {
            this.head = head;
            current = null;
        }

        public bool MoveNext()
        {
            if(current == null)
            {
                current = head;
            }
            else
            {
                current = current.Next;
            }
            return current != null;
        }

        object System.Collections.IEnumerator.Current => Current;        
        
        public void Dispose()
        {
            
        }
        public void Reset()
        {
            current = null;
        }
    }
}