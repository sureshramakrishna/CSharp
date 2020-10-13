using System;
using System.Threading;

namespace Semaphores
{
    class Program
    {
        private static Semaphore _pool;
        public static void Main()
        {
            _pool = new Semaphore(initialCount: 0, maximumCount: 3); //InitialCount is 0, meaning all 3-semaphore count is owned by current thread.
            for (int i = 1; i <= 5; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(Worker));
                t.Start(i);
            }
            Thread.Sleep(500);
            Console.WriteLine("Main thread calls Release(3).");
            _pool.Release(3); //Releases 3 times.
            Thread.Sleep(10000);
        }

        private static void Worker(object num)
        {
            Console.WriteLine("Thread {0} begins and waits for the semaphore.", num);
            _pool.WaitOne();
            Console.WriteLine("Thread {0} enters the semaphore.", num);
            Thread.Sleep(1000 + Convert.ToInt32(num) * 50);
            Console.WriteLine("Thread {0} releases the semaphore.", num);
            Console.WriteLine("Thread {0} previous semaphore count: {1}", num, _pool.Release()); //_pool.Release() releases 1 semaphore
        }

    }
}