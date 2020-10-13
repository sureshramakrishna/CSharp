using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TasksFactory
{
    class Program
    {
        static Action<object> action = (object obj) =>
        {
            Console.WriteLine("Task={0}, obj={1}, Thread={2}", Task.CurrentId, obj, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
        };
        static void Main(string[] args)
        {
            // Construct a started task using Task.Factory.StartNew. Task is already in started state.
            Task t3 = Task.Factory.StartNew(action, "beta");
            Task.Factory.ContinueWhenAll(new[] { t3 }, action);
        }
    }
}
