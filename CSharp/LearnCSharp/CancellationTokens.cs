using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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

            CancellationByPolling(token);
            RegisteringACallBack(token);
            source.Cancel();

        }
        static void CancellationByPolling(CancellationToken token)
        {
            for (int x = 0; x < int.MaxValue && !token.IsCancellationRequested; x++)
                Thread.Sleep(100);
        }
        static void RegisteringACallBack(CancellationToken token)
        {
            WebClient wc = new WebClient();
            // Cancellation on the token will call CancelAsync on the WebClient.
            token.Register(() =>
            {
                wc.CancelAsync();
                Console.WriteLine("Request cancelled!");
            });
            wc.DownloadStringAsync(new Uri("http://www.contoso.com"));
        }
        static ManualResetEvent mre = new ManualResetEvent(false);
        static void CancellationByUsingWaitHandle(CancellationToken token)
        {
            WaitHandle.WaitAny(new[] { mre, token.WaitHandle }); //Wait for resource or until cancelled
        }
    }
}
