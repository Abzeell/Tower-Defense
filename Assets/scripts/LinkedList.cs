using UnityEngine;

    public class LinkedList<T>
    {
        public Node<T> Head { get; private set; }
        public Node<T> Tail { get; private set; }

        public LinkedList()
        {
            Head = null;
            Tail = null;
        }

        // Add a new node at the end
        public void AddLast(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (Tail == null)
            {
                Head = Tail = newNode;
            }
            else
            {
                Tail.Next = newNode;
                Tail = newNode;
                Tail.Next = null;
            }
        }

    public void AddFirst(T value)
    {
        Node<T> newNode = new Node<T>(value);

        if (Head == null)
        {
            Head = Tail = newNode;
        }
        else
        {
            newNode.Next = Head;
            Head = newNode;
        }
    }

    
    public void PrintAllNodes()
        {
            Node<T> current = Head;
            while (current != null)
            {
                Debug.Log(current.Value);
                current = current.Next;
            }
        }
    }

