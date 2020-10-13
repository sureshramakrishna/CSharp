using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Threads
{
    class Program
    {
        public static void InstanceMethod()
        {
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine("Thread - working.");
                    Thread.Sleep(100);
                }
            }
            catch (ThreadAbortException e)
            {
                Console.WriteLine("Thread - caught ThreadAbortException - resetting.");
                Console.WriteLine("Exception message: {0}", e.Message);
                Thread.ResetAbort(); //If this method is not invoked, then ThreadAbortException is rethrown until it reaches the main thread.
            }
            Console.WriteLine("Thread - still alive and working.");
            Thread.Sleep(1000);
            Console.WriteLine("Thread - finished working.");
        }

        public static bool SleepSwitch;
        static void InstanceMethod(object message)
        {
            Console.WriteLine((string)message);
            while (!SleepSwitch)
            {
                Thread.SpinWait(10000000);
            }
            try
            {
                Console.WriteLine("newThread going to sleep.");
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine("newThread cannot go to sleep - " + "interrupted by main thread.");
            }
        }

        [Obsolete]
        static void Main(string[] args)
        {
            Thread instanceCaller = new Thread(new ThreadStart(InstanceMethod));
            instanceCaller.Priority = ThreadPriority.Highest;
            instanceCaller.Start();
            instanceCaller.Suspend();
            Thread.Sleep(100);
            instanceCaller.Resume();
            if (instanceCaller.ThreadState != ThreadState.Suspended)
                instanceCaller.Abort(); //abort this thread, meaning ThreadAbortException is thrown inside InstanceMethod. Throws error when thread is suspended
            instanceCaller.Join(); //waits until InstanceCaller is completed.

            Thread parameterizedThread = new Thread(new ParameterizedThreadStart(InstanceMethod));
            parameterizedThread.Start("Print Me!");
            parameterizedThread.Interrupt(); //When next time thread goes to sleep, wait or join state, thread throws ThreadInterruptedException exception.
            SleepSwitch = true;
            while (parameterizedThread.ThreadState != ThreadState.Stopped) ;

        }
    }
}
