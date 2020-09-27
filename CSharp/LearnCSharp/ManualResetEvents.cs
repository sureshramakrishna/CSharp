using System;
using System.Threading;
namespace ManualResetEvents
{
    public class Program
    {
        private static ManualResetEvent mre = new ManualResetEvent(false);
        static void Main()
        {
            for (int i = 1; i < 4; i++)
            {
                Thread t = new Thread(ThreadProc);
                t.Name = "Thread_" + i;
                t.Start();
            }
            Thread.Sleep(250);

            Console.WriteLine("Press Enter to release all thread.");
            Console.ReadLine();
            mre.Set();
            Thread.Sleep(250);
            mre.Reset(); //If reset is not invoked, then ManualResetEvent will remain in signaled state. Future WaitOne doesn't wait when MRE is in signalled state
        }

        private static void ThreadProc()
        {
            string name = Thread.CurrentThread.Name;
            Console.WriteLine(name + " starts and calls mre.WaitOne()");
            mre.WaitOne();
            Console.WriteLine(name + " ends.");
        }
    }
}
