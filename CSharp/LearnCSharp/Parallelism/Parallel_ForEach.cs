using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_ForEach
{
    class Program
    {
        static IEnumerable<int> nums = Enumerable.Range(0, 10);
        static CancellationTokenSource cts = new CancellationTokenSource();
        static ParallelOptions po = new ParallelOptions { CancellationToken = cts.Token, MaxDegreeOfParallelism = 3 };
        static long total = 0;
        static void Main(string[] _)
        {
            // Using a named method.
            Parallel.ForEach(nums, PerformTaskHere);

            // Using an anonymous method.
            Parallel.ForEach(nums, delegate (int value)
            {
                Console.WriteLine(value);
            });

            // Using a lambda expression.
            Parallel.ForEach(nums, value =>
            {
                Console.WriteLine(value);
            });

            //With Thread local variable
            Parallel.ForEach<int, long>(nums, po, () => 10, (value, parallelLoopState, localVariable) =>
            {
                localVariable += value;
                po.CancellationToken.ThrowIfCancellationRequested();
                return localVariable;
            },
            (x) => { Interlocked.Add(ref total, x); });
        }
        static void PerformTaskHere(int index)
        {
            Console.WriteLine(index);
        }
    }
}
