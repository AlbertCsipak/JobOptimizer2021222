using System;
using System.Collections;
using System.Collections.Generic;

namespace FélévesFeladat_Z6O9JF
{
    sealed class Lister<T> : IEnumerator<T> where T : IComparable
    {
        LinkedItem<T> starter;
        LinkedItem<T> current;
        public Lister(LinkedItem<T> var1)
        {
            this.starter = var1;
            this.current = new LinkedItem<T>();
            this.current.Next = starter;
        }
        public object Current
        {
            get
            {
                return current.Content;
            }
        }
        T IEnumerator<T>.Current
        {
            get
            {
                return current.Content;
            }
        }
        public bool MoveNext()
        {
            if (current is null)
            {
                return false;
            }
            current = current.Next;
            return current != null;
        }
        public void Reset()
        {
            current = new LinkedItem<T>();
            current.Next = starter;
        }
        public void Dispose() { }
    }
    sealed class LinkedItem<T> where T : IComparable
    {
        public T Content { get; set; }
        public LinkedItem<T> Next { get; set; }
    }
    public sealed class MyLinkedList<T> : IEnumerable<T> where T : IComparable
    {
        LinkedItem<T> starterItem;
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Lister<T>(starterItem);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Lister<T>(starterItem);
        }
        public void ItemInsert(T var)
        {
            //v2
            LinkedItem<T> localItem = new LinkedItem<T>();
            localItem.Content = var;

            if (var is Zoo.Jobs)
            {
                if (starterItem is null)
                {
                    localItem.Next = null;
                    starterItem = localItem;
                }
                else if ((starterItem.Content as Zoo.Jobs).CompareTo(localItem.Content as Zoo.Jobs) < 0)
                {
                    localItem.Next = starterItem;
                    starterItem = localItem;
                }
                else
                {
                    LinkedItem<T> localItem1 = starterItem;
                    LinkedItem<T> localItem2 = null;
                    if ((localItem1.Content as Zoo.Jobs).CompareTo(var as Zoo.Jobs) == 0)
                    {
                        localItem2 = localItem1;
                        localItem1 = localItem1.Next;
                    }
                    else
                    {
                        while (localItem1 != null && ((localItem1.Content as Zoo.Jobs).CompareTo(var as Zoo.Jobs) > 0))
                        {
                            localItem2 = localItem1;
                            localItem1 = localItem1.Next;
                        }
                    }
                    if (localItem1 == null || !localItem1.Content.Equals(var as Zoo.Jobs))
                    {
                        localItem.Next = localItem1;
                        localItem2.Next = localItem;
                    }
                }
            }
            else if (var is Zoo.Employees)
            {
                if (starterItem is null)
                {
                    localItem.Next = null;
                    starterItem = localItem;
                }
                else if ((starterItem.Content as Zoo.Employees).CompareTo(localItem.Content as Zoo.Employees) > 0)
                {
                    localItem.Next = starterItem;
                    starterItem = localItem;
                }
                else
                {
                    LinkedItem<T> localItem1 = starterItem;
                    LinkedItem<T> localItem2 = null;
                    if ((localItem1.Content as Zoo.Employees).CompareTo(var as Zoo.Employees) == 0)
                    {
                        localItem2 = localItem1;
                        localItem1 = localItem1.Next;
                    }
                    else
                    {
                        while (localItem1 != null && ((localItem1.Content as Zoo.Employees).CompareTo(var as Zoo.Employees) < 0))
                        {
                            localItem2 = localItem1;
                            localItem1 = localItem1.Next;
                        }
                    }
                    if (localItem1 == null || !localItem1.Content.Equals(var as Zoo.Employees))
                    {
                        localItem.Next = localItem1;
                        localItem2.Next = localItem;
                    }
                }
            }
        }
    }
}
