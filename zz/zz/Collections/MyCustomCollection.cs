using System;
using zz.Interfaces;
using _053502_ANKUSHEV_LAB5.Entities;

namespace zz.Collections
{
    public class Node<T>
    {
        public Node(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }
        public T Data { get; set; }
        public Node<T> Next;
        public Node<T> Previous;
    }

    public class MyCustomCollection<T>: ICustomCollection<T>
    {
        Node<T> head; 
        Node<T> tail; 
        Node<T> cursor;
        public  int Count { get; set; }
        public MyCustomCollection()
        {
            Count = 0;
            head = null;
        }
        public T this[int index] {
            get
            {
                try
                {
                    if (index < 0 || index >= Count) 
                        throw new IndexOutOfRangeException();
                    
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Ошибка! Несуществующий индекс!");
                    return default;
                }
                    int counter = 0;
                    Node<T> current = head;
                    while (current != null)
                    {
                        if (counter == index) break;
                        current = current.Next;
                        counter++;
                    }
                    return current.Data;
            }
            set
            {
                    int counter = 0;
                    Node<T> current = head;
                    while (current != null)
                    {
                        if (counter == index) break;
                        current = current.Next;
                        counter++;
                    }
                    current.Data = value;
            }
        }


        public void Add(T item)
        {
            Node<T> node = new Node<T>(item);

            if (head == null)
                head = node;
            else
                tail.Next = node;
                node.Previous = tail;

            tail = node;
            cursor = tail;
            Count++;
        }

        public T Current()
        {
            return cursor.Data;
        }

        public void Next()
        {
            if (cursor.Next != null)
            {
                cursor = cursor.Next;
            }
            else
            {
                Reset();
            }
                
        }

        public void Remove(T item)
        {
            Node<T> current = head;
            try
            {
                // поиск удаляемого узла
                while (current != null)
                {
                    if (current.Data.Equals(item))
                    {
                        break;
                    }
                    current = current.Next;

                    if (current == null) throw new NoItemExistException("Удаляемый элемент отсутствует в данной коллекции!");
                }
            }
            catch (NoItemExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (current != null)
            {
                // если узел не последний
                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    // если последний, переустанавливаем tail
                    tail = current.Previous;
                }

                // если узел не первый
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    // если первый, переустанавливаем head
                    head = current.Next;
                }
                Count--;
            }
        }

        public void RemoveCurrent()
        {
            Remove(cursor.Data);
            cursor = cursor.Previous ?? head;
        }

        public void Reset()
        {
            cursor = head;
        }
    }
}
