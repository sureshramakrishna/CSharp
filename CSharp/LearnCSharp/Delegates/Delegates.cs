using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class Program
    {
        public delegate void sumsubtract(int a, int b);
        public static void sum(int a, int b)
        {
            Console.WriteLine(a + b);
        }
        public static void subtract(int a, int b)
        {
            Console.WriteLine(a - b);
        }
        static void Main(string[] args)
        {
            sumsubtract ss = new sumsubtract(sum);
            ss += subtract;
            ss.Invoke(5, 3); // prints 8, 2
            ss -= subtract;
            ss.Invoke(5, 3); // prints 8
        }
    }
}
