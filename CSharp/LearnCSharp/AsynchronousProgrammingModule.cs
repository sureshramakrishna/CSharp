using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousProgrammingModule
{
    class Program
    {
        public delegate string AsyncMethodCaller();
        public static string DoThis()
        {
            Thread.Sleep(5000);
            return "Hello";
        }
        public static void CallBackMethod(IAsyncResult ar)
        {
            AsyncResult result = (AsyncResult)ar;
            AsyncMethodCaller caller = (AsyncMethodCaller)result.AsyncDelegate;
            string returnValue = caller.EndInvoke(result);
            Console.WriteLine(returnValue);
        }

        static void Main(string[] args)
        {
            AsyncMethodCaller caller = new AsyncMethodCaller(DoThis);
            IAsyncResult result = caller.BeginInvoke(new AsyncCallback(CallBackMethod), null);
            result.AsyncWaitHandle.WaitOne();
        }
    }
}
