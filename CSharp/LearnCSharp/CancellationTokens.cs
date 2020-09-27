using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokens
{
    public class Example
    {
        public static void CallBackMethod()
        {
            Console.WriteLine("Call back method!");
        }
        public static void Main()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            token.Register(() => { CallBackMethod(); }); //Registers a call back method that executes on cancel

            Random rnd = new Random();

            TaskFactory factory = new TaskFactory(token);
            Task task = factory.StartNew(() => {
                Thread.Sleep(1000);
                while (rnd.Next(0, 100) != 0)
                    source.Cancel();
            }, token);

            try
            {
                Task<int> fTask = factory.ContinueWhenAll(new[] { task },
                                                             (results) => {
                                                                 Console.WriteLine("Made it even with Cancelling.");
                                                                 return 1;
                                                             }, token);
                int result = fTask.Result; //throws exception as ftask was cancelled.
                Console.WriteLine("Completed!");
            }
            catch (AggregateException ae)
            {
                foreach (Exception e in ae.InnerExceptions)
                {
                    if (e is TaskCanceledException)
                        Console.WriteLine(((TaskCanceledException)e).Message);
                    else
                        Console.WriteLine("Exception: " + e.GetType().Name);
                }
            }
            finally
            {
                source.Dispose();
            }
        }
    }
}
