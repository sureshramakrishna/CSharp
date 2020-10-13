using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ThreadPool
{
    class Program
    {
        static void ThreadProc(object state)
        {
            Console.WriteLine("Print Me!");
        }
        public static void Main()
        {
            int workerThreads = 0;
            int ioThreads = 0;
            int maxWorkerThreads = 0;
            int maxIOThreads = 0;

            System.Threading.ThreadPool.GetAvailableThreads(out workerThreads, out ioThreads);
            System.Threading.ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxIOThreads);
            System.Threading.ThreadPool.QueueUserWorkItem(ThreadProc);
            System.Threading.ThreadPool.SetMaxThreads(100, 100);
        }
    }
}
