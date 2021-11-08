using System;
using System.Collections.Generic;
using System.Text;

namespace zz.Interfaces
{
    public interface ICustomCollection<T>
    {
        T this[int index] { get; set; }

        void Reset();
        void Next();
        T Current();
        int Count { get; }
        void Add(T item);
        void Remove(T item);
        void RemoveCurrent();

    }
}
