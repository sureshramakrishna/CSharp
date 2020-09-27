using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadCancellation
{
    class CancelByPolling
    {
        public static void NestedLoops(CancellationToken token)
        {
            for (int x = 0; x < 1000 && !token.IsCancellationRequested; x++)
            {
                for (int y = 0; y < 500; y++)
                {
                    Thread.SpinWait(5000);
                    Console.Write("{0},{1} ", x, y);
                }
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Press any key to exit.");
                    break;
                }
            }
        }
    }
    class CancelWithCallback
    {
        static WebClient webClient = new WebClient();
        public static void CallBackMethod()
        {
            webClient.CancelAsync();
        }
        public static void DownloadAsync()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            Task t = Task.Run(async () =>
            {
                token.Register(() => CallBackMethod()); //Registering callback that would cancel downloading

                var data = await webClient.DownloadDataTaskAsync("http://www.youtube.com/");
            }, token);
            Thread.Sleep(10);
            cts.Cancel();
            t.Wait();
        }
    }
    class CancelWhenThreadWaitsOnEvents
    {
        static ManualResetEvent mre = new ManualResetEvent(false);
        public static void Start()
        {
            var cts = new CancellationTokenSource();
            Task.Run(() => DoWork(cts.Token), cts.Token);
            bool goAgain = true;
            while (goAgain)
            {
                char ch = Console.ReadKey(true).KeyChar;
                switch (ch)
                {
                    case 'c': //cancel
                        cts.Cancel();
                        break;
                    case 'p': //pause
                        mre.Reset();
                        break;
                    case 's': //start
                        mre.Set();
                        break;
                    default:
                        goAgain = false;
                        break;
                }
                Thread.Sleep(100);
            }
            cts.Dispose();
        }

        static void DoWork(CancellationToken token)
        {
            while (true)
            {
                int eventThatSignaledIndex = WaitHandle.WaitAny(new WaitHandle[] { mre, token.WaitHandle }, new TimeSpan(0, 0, 20));
                if (eventThatSignaledIndex == 1) //Cancelled while waiting
                {
                    Console.WriteLine("The wait operation was canceled.");
                    throw new OperationCanceledException(token);
                }
                else if (token.IsCancellationRequested) //Cancelled after WaitAny
                {
                    Console.WriteLine("I was canceled while running.");
                    token.ThrowIfCancellationRequested();
                }
                else if (eventThatSignaledIndex == WaitHandle.WaitTimeout)
                {
                    Console.WriteLine("I timed out.");
                    break;
                }
                else
                {
                    Console.Write("Working... ");
                    Thread.SpinWait(5000);
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var tokenSource = new CancellationTokenSource();
            Task.Run(() => CancelByPolling.NestedLoops(tokenSource.Token), tokenSource.Token);
            Console.WriteLine("Press 'c' to cancel");
            if (Console.ReadKey(true).KeyChar == 'c')
            {
                tokenSource.Cancel();
                Console.WriteLine("Press any key to exit.");
            }
            Console.ReadKey();
            tokenSource.Dispose();

            CancelWithCallback.DownloadAsync();

            CancelWhenThreadWaitsOnEvents.Start();
        }
    }
}
