using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SpinLocks
{
    class SpinLockDemo
    {
        static void Simulate()
        {
            SpinLock sl = new SpinLock();
            Action action = () =>
            {
                bool gotLock = false;
                for (int i = 0; i < 10; i++)
                {
                    if(!sl.IsHeldByCurrentThread)
                        sl.Enter(ref gotLock); //acquires the lock
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                }
                if (gotLock)
                    sl.Exit(); //releases the lock
            };
            Parallel.Invoke(action, action, action);
        }
        public static void Main()
        {
            Simulate();
        }
    }
}
