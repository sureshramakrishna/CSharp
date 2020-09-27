using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    class Program
    {
        static Action<object> action = (object obj) =>
        {
            Console.WriteLine("Task={0}, obj={1}, Thread={2}", Task.CurrentId, obj, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
        };
        static void Main()
        {
            Task t1 = new Task(action, "alpha");
            t1.Start();
            t1.Wait();

            Task t2 = new Task(action, "betta"); //If ContinueWith is appended here then t2 will refer to ContinueWith Task. We cannot directly start ContinueWith Task without starting the main Task.
            Task continueWithTask = t2.ContinueWith(cw => { Console.WriteLine("In continuation!"); });
            t2.Start();
            t2.Wait(); //Note that t2.Wait only waits for t2 to complete and does not wait for continueWithTask.
            continueWithTask.Wait(); 

            // Construct a started task using Task.Factory.StartNew. Task is already in started state.
            Task t3 = Task.Factory.StartNew(action, "beta"); 
            t3.Wait();

            // Construct a started task using Task.Run.
            var taskData = "delta";
            Task t4 = Task.Run(() =>
            {
                Console.WriteLine("Task={0}, obj={1}, Thread={2}", Task.CurrentId, taskData, Thread.CurrentThread.ManagedThreadId);
            });
            t4.Wait();

            Task t5 = new Task(action, "epsilon");
            t5.RunSynchronously();
            t5.Wait();

            Stopwatch sw = Stopwatch.StartNew();
            Task taskDelay = Task.Delay(1000); //Difference between Task.Delay and Thread.Sleep is Task.Delay executes in a seperate thread and Thread.Sleep executes in main thread.
            Task taskContinueWith = taskDelay.ContinueWith(cw => { sw.Stop(); return sw.ElapsedMilliseconds; });
            taskDelay.Wait();
            taskContinueWith.Wait();

            Task.Run(async delegate
            {
                for (int i = 0; i < 1000000; i++)
                {
                    await Task.Yield(); // returns control to main thread and forks the continuation to a different thread.
                }
            });

        }
    }
}
