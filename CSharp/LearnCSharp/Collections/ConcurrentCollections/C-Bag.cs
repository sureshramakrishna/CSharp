using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Bag
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentBag<int> cb = new ConcurrentBag<int>();
            Parallel.For(0, 100, (i) =>
            {
                cb.Add(i);
            });
            cb.TryPeek(out int peekedItem); //attempts to remove an item without removing it.
            cb.TryTake(out int result); //result stores the removed item. item will be random item form the list
        }
    }
}
