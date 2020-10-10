using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCSharp.Parallel_Invoke
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.Invoke(
                    () => { Console.WriteLine("Begin first task..."); },
                    () => { Console.WriteLine("Begin second task..."); },
                    () => { Console.WriteLine("Begin third task..."); }
                    );
        }
    }
}
