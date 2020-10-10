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
        static int[] nums = Enumerable.Range(0, 10).ToArray();
        static CancellationTokenSource cts = new CancellationTokenSource();
        static ParallelOptions po = new ParallelOptions { CancellationToken = cts.Token, MaxDegreeOfParallelism = 3 };
        static void Main(string[] args)
        {
            Parallel.For(0, 100, PerformTaskHere);            // Using a named method.
            Parallel.For(0, 100, delegate (int index)         // Using an anonymous method.
            {
                Console.WriteLine(index);
            });
            Parallel.For(0, 100, index =>                     // Using a lambda expression.
            {
                Console.WriteLine(index);
            });

            long total = 0;
            Parallel.For<long>(0, nums.Length, po, () => 10, (k, l, localVariable) =>             //With Thread local variable
            {
                localVariable += nums[k];
                po.CancellationToken.ThrowIfCancellationRequested();
                return localVariable;
            },
            (x) => { Interlocked.Add(ref total, x); });
            //() => 10 initializes thread local variable value to 10 for every thread.
            //(k, l, localVariable)
            //k is the current index in the loop. 
            //l is ParallelLoopState which is shared with all the iterations across threads and this can be used to stop loop execution
            //localVariable is the thread variable and is shared between the iterations executing in the same thread.
            //since MaxDegreeOfParallelism is 3, 3 threads can be created and 3 locaVariable will be created as well for each thread and initialized all 3 localvariable to 10. 
            //For<long> determines the type of local variable.
            //x – output of fourth parameter. This statement executes at the end of each thread not on each iteration. 
            //Let's say Parallel.For was executed using 2 threads, then these statement gets executed at the end of those 2 threads.
        }
        static void PerformTaskHere(int index)
        {
            Console.WriteLine(index);
        }

    }
}
