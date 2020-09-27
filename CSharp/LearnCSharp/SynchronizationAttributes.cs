using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SynchronizationAttributes
{
    [System.Runtime.Remoting.Contexts.Synchronization()]
    public class SampleSynchronized
    {
        public void Print()
        {
            for (int i = 0; i < 100; i++)
                Console.Write(Thread.CurrentThread.ManagedThreadId);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            SampleSynchronized s = new SampleSynchronized();
            for(int i = 0; i<10;i++)
            {
                Thread t = new Thread(new ThreadStart(s.Print));
                t.Start();
            }
        }
    }
}
