using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentQueue<int> cq = new ConcurrentQueue<int>();
            cq.Enqueue(1);
            cq.Enqueue(2);
            cq.TryDequeue(out int result);
        }
    }
}
