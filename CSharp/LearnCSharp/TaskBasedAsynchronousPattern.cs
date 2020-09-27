using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TaskBasedAsynchronousPattern
{
    class Program
    {
        public delegate bool Prime(int number);
        static void Main(string[] args)
        {
            var task = ReadTask("Nothing");
            task.Wait();
        }
        public static Task<bool> ReadTask(object state)
        {
            var tcs = new TaskCompletionSource<bool>();
            Prime prime = new Prime(IsPrime);
            prime.BeginInvoke(2147483647, ar =>
            {
                try
                {
                    AsyncResult result = (AsyncResult)ar; 
                    Prime caller = (Prime)result.AsyncDelegate;
                    tcs.SetResult(caller.EndInvoke(result)); //delegate return value can be accessed using EndInvoke.
                }
                catch (Exception exc)
                {
                    tcs.SetException(exc);
                }
            }, state);
            return tcs.Task;
        }
        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}
