using System;
using System.Threading;
namespace WaitHandles
{
    public class WaitHandles
    {
        private static readonly EventWaitHandle Ewh = new EventWaitHandle(false, EventResetMode.AutoReset);
        private static long _threadCount = 0;
        private static readonly EventWaitHandle ClearCount = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static void Main()
        {
            for (int i = 0; i <= 4; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(ThreadProc));
                t.Start(i);
            }
            while (Interlocked.Read(ref _threadCount) < 5) //wait until all threads are blocked.
            {
                Thread.Sleep(500);
            }
            while (Interlocked.Read(ref _threadCount) > 0)
            {
                Console.WriteLine("Press ENTER to release a waiting thread.");
                Console.ReadLine();
                WaitHandle.SignalAndWait(Ewh, ClearCount); //signals ewh and waits for signal on clearCount.
            }
            Ewh.Set();
        }

        public static void ThreadProc(object data)
        {
            int index = (int)data;
            Console.WriteLine("Thread {0} blocks.", data);
            Interlocked.Increment(ref _threadCount);
            if (index == 0)
                WaitHandle.WaitAny(new WaitHandle[] { Ewh });
            else if (index == 1)
                WaitHandle.WaitAll(new WaitHandle[] { Ewh });
            else
                Ewh.WaitOne();
            Console.WriteLine("Thread {0} exits.", data);
            Interlocked.Decrement(ref _threadCount);
            ClearCount.Set(); //signals clearCount
        }
    }
}
