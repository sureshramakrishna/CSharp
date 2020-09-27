using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventWaitHandles
{
    using System;
    using System.Threading;

    public class Program
    {
        private static EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static void Main()
        {
            for (int i = 0; i <= 4; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(ThreadProc));
                t.Start(i);
            }
            Thread.Sleep(500);
            for (int i = 0; i <= 4; i++)
            {
                Console.WriteLine("Press ENTER to release a waiting thread.");
                Console.ReadLine();
                ewh.Set();
                Thread.Sleep(100);
            }
            
            // Create a ManualReset EventWaitHandle.
            ewh = new EventWaitHandle(false, EventResetMode.ManualReset);
            for (int i = 0; i <= 4; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(ThreadProc));
                t.Start(i);
            }
            Thread.Sleep(500);

            // Because the EventWaitHandle was created with ManualReset mode, signaling it releases all the waiting threads.
            Console.WriteLine("Press ENTER to release the waiting threads.");
            Console.ReadLine();
            ewh.Set();
            Thread.Sleep(500);
        }

        public static void ThreadProc(object data)
        {
            int index = (int)data;
            Console.WriteLine("Thread {0} blocks.", data);
            ewh.WaitOne();
            Console.WriteLine("Thread {0} exits.", data);
        }
    }
}
