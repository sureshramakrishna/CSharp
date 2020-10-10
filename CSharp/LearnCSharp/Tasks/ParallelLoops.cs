using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoops
{
    class Program
    {
        static int[] nums = Enumerable.Range(0, 10).ToArray();
        static CancellationTokenSource cts = new CancellationTokenSource();
        static ParallelOptions po = new ParallelOptions { CancellationToken = cts.Token, MaxDegreeOfParallelism = 3};

        static void Main(string[] args)
        {
            Parallel.For(0, 10, (index) => { Console.WriteLine(index); });

            long total = 0;
            Parallel.For<long>(0, nums.Length, po, () => 0, (k, l, localVariable) =>
            {
                localVariable += nums[k];
                po.CancellationToken.ThrowIfCancellationRequested();
                return localVariable;
            }, (x) => { Interlocked.Add(ref total, x); }); 
            //this statement executes at the end of each thread not on each iteration. 
            //Let's say Parallel.For was executed using 2 threads, then these statement gets executed at the end of those 2 threads.


            Parallel.ForEach(Enumerable.Range(0, 10), (item) => { Console.WriteLine(item); });

            total = 0; ;
            Parallel.ForEach<int, long>(nums, () => 0, (item, ps, localVariable) => 
            {
                localVariable += item;
                return localVariable;
            }, (finalResult) => Interlocked.Add(ref total, finalResult));

            Parallel.Invoke(
                            () => { Console.WriteLine("Begin first task..."); },
                            () => { Console.WriteLine("Begin second task...");  }, 
                            () => { Console.WriteLine("Begin third task..."); } 
                         ); //Executes each of the provided actions, possibly in parallel.
        }
    }
}
