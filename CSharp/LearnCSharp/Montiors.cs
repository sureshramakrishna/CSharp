using System;
using System.Threading;
namespace Monitors
{
    public class Program
    {
        static object _lock = new Object();
        static void Main()
        {
            Thread threadOne = new Thread(Count);
            threadOne.Start();
            Thread threadTwo = new Thread(Count);
            threadTwo.Start();

            threadTwo.Join();
        }

        static void Count()
        {
            Monitor.Enter(_lock);
            int count = 0;
            while (true)
            {
                Console.WriteLine("Thread: {0}, Count: {1}", Thread.CurrentThread.ManagedThreadId, count++);
                if (count % 10 == 0)
                {
                    Monitor.Pulse(_lock); //does not release the lock, just moves 1 thread from waiting queue to ready queue
                    if(Monitor.IsEntered(_lock))
                        Monitor.Wait(_lock); //release the lock and moves the thread to waiting queue, not ready queue. Should be moved to ready queue inorder to reacquire the lock.
                }
            }
            Monitor.Exit(_lock);
        }
    }
}
