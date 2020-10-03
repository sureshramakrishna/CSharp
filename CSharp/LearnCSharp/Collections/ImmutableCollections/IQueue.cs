using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace IQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            ImmutableQueue<int> iq = ImmutableQueue.Create<int>();
            iq = iq.Enqueue(1);
            iq = iq.Dequeue(out int poppedItem);
        }
    }
}
