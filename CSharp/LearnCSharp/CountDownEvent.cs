using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CountDownEvent
{
    class Program
    {
        static async void CountDownMethod()
        {
            ConcurrentQueue<int> queue = new ConcurrentQueue<int>(Enumerable.Range(0, 10000));
            CountdownEvent cde = new CountdownEvent(10000); // initial count = 10000

            Action consumer = () =>
            {
                int local;
                while (queue.TryDequeue(out local))
                    cde.Signal(); // decrement CDE count once for each element consumed from queue
            };

            // Now empty the queue with a couple of asynchronous tasks
            Task t1 = Task.Factory.StartNew(consumer);
            Task t2 = Task.Factory.StartNew(consumer);

            cde.Wait(); // will return when cde count reaches 0
            await Task.WhenAll(t1, t2); // Proper form is to wait for the tasks to complete, even if you that their work is done already.
            cde.Reset(10); // Resetting will cause the CountdownEvent to un-set, and resets InitialCount/CurrentCount to the specified value
        }
        static void Main()
        {
            CountDownMethod();
        }
    }
}
