using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Parallel_For
{
    class Program
    {
        static CancellationTokenSource cts = new CancellationTokenSource();
        static ParallelOptions po = new ParallelOptions { CancellationToken = cts.Token, MaxDegreeOfParallelism = 3 };
        static long total = 0;
        static void Main(string[] _)
        {
            // Using a named method.
            Parallel.For(0, 100, PerformTaskHere);

            // Using an anonymous method.
            Parallel.For(0, 100, delegate (int index)
            {
                Console.WriteLine(index);
            });

            // Using a lambda expression.
            Parallel.For(0, 100, index =>
            {
                Console.WriteLine(index);
            });

            //With Thread local variable
            Parallel.For<long>(0, 10, po, () => 10, (index, parallelLoopState, localVariable) =>
            {
                localVariable += index;
                po.CancellationToken.ThrowIfCancellationRequested();
                return localVariable;
            },
            (x) => { Interlocked.Add(ref total, x); });
            //() => 10 initializes thread local variable value to 10 for every thread.
            //localVariable is the thread variable and is shared between the iterations executing in the SAME THREAD.
            //If 3 threads are created, then 3 locaVariable will be created as well for each thread and initializes all 3 localvariable to 10. 
            //For<long> determines the type of local variable.
            //x – output of fourth parameter. This statement executes at the end of each THREAD not on each iteration. 
            //Let's say Parallel.For was executed using 2 threads, then these statement gets executed at the end of those 2 threads.
        }
        static void PerformTaskHere(int index)
        {
            Console.WriteLine(index);
        }
    }
}
