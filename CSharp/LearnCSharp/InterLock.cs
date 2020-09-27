using System;
using System.Threading;
namespace InterLock
{
    class Simulate
    {
        private static int usingResource = 0;
        static void Main()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread myThread = new Thread(new ThreadStart(MyThreadProc));
                myThread.Name = String.Format("Thread{0}", i + 1);
                myThread.Start();
            }
        }
        private static void MyThreadProc()
        {
            for (int i = 0; i < 2; i++)
            {
                int oldValue = Interlocked.Exchange(ref usingResource, 1);
                Interlocked.Increment(ref usingResource);
                Interlocked.Decrement(ref usingResource);
                Interlocked.Add(ref usingResource, 5);
            }
        }
    }
}
