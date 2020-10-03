using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentDictionary<int, int> cd = new ConcurrentDictionary<int, int>();
            cd.TryAdd(1, 1);
            cd.GetOrAdd(2, 2);
            cd.TryUpdate(2, 3, 2);
            Parallel.For(0, 10000, i =>
            {
                // Initial call will set cd[4] = 1.
                // Ensuing calls will set cd[4] = cd[1] + 1
                cd.AddOrUpdate(4, 1, (key, oldValue) => oldValue + 1);
            });
        }
    }
}
