using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Mutexs
{
    class Program
    {
        static Mutex mutex = new Mutex(false, "MutexName"); //true to give the calling thread initial ownership of the named system mutex
        static void Main(string[] args)
        {
            Mutex m = Mutex.OpenExisting("MutexName");
            mutex.WaitOne(); //Blocks the current thread until the current System.Threading.WaitHandle receives a signal.
            mutex.ReleaseMutex();
            mutex.Dispose(); //Releases all resources used by the current instance of the System.Threading.WaitHandle class
        }
    }
}
