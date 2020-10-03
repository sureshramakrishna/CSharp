using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentStack<int> cs = new ConcurrentStack<int>();
            cs.Push(1);
            cs.TryPop(out int result);
            cs.PushRange(new int[] { 5, 6 });
            int[] pops = new int[2];
            cs.TryPopRange(pops); //tries remove range of items based on array size.
        }
    }
}
