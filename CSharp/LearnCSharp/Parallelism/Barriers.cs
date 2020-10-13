using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Barriers
{
    class Program
    {
static Barrier barrier = new Barrier(2, (b) =>
{
    //Post phase action if any.
});

static void Main(string[] args)
{

    Thread t1 = new Thread(() => GetDataAndStoreData(1));
    Thread t2 = new Thread(() => GetDataAndStoreData(2));
    t1.Start();
    t2.Start();
    Console.ReadLine();
}

static void GetDataAndStoreData(int index)
{
    Console.WriteLine("Getting data from server: " + index);
    Thread.Sleep(TimeSpan.FromSeconds(2));

    barrier.SignalAndWait();

    Console.WriteLine("Send data to Backup server: " + index);

    barrier.SignalAndWait();
}
    }
}
