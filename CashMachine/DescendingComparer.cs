using System;
using System.Collections.Generic;

namespace CashMachine
{
    class DescendingComparer<T> : IComparer<T> where T : IComparable
    {
        public int Compare(T x, T y)
        {
            return y.CompareTo(x);
        }
    }
}
