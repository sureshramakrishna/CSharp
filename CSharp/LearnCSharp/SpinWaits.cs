using System;
using System.Threading.Tasks;
using System.Threading;
namespace SpinWaits
{
    class SpinWaitDemo
    {
        static void Main()
        {
            bool someBoolean = false;
            int numYields = 0;

            Task t1 = Task.Factory.StartNew(() =>
            {
                SpinWait sw = new SpinWait();
                while (!someBoolean)
                {
                    if (sw.NextSpinWillYield)
                        numYields++;
                    sw.SpinOnce();
                }
                //After some initial spinning, SpinWait.SpinOnce() will yield every time.
                Console.WriteLine("SpinWait called {0} times, yielded {1} times", sw.Count, numYields);
                SpinWait.SpinUntil(() => { return true; });
            });

            Task t2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                someBoolean = true;
            });

            Task.WaitAll(t1, t2);
        }
    }
}
