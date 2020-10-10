using System;
using System.Threading;
namespace AutoResetEvents
{
    class Program
    {
        //false sets initial state to non-signaled; true sets the initial state to signaled. 
        //When set to true, first thread's WaitOne continues without waiting becuase AutoResetEvent is already in signaled state.
        private static AutoResetEvent event_1 = new AutoResetEvent(false);
        static void Main()
        {
            for (int i = 1; i < 4; i++)
            {
                Thread t = new Thread(ThreadProc);
                t.Start();
            }
            Thread.Sleep(250);

            for (int i = 0; i < 3; i++)
            {
                Console.ReadLine(); //Press Enter to release a thread.
                event_1.Set();
            }
        }
        static void ThreadProc()
        {
            Console.WriteLine("{0} waits on AutoResetEvent event_1.", Thread.CurrentThread.ManagedThreadId);
            event_1.WaitOne();
            Console.WriteLine("{0} is released from AutoResetEvent event_1.", Thread.CurrentThread.ManagedThreadId);
        }
    }
}