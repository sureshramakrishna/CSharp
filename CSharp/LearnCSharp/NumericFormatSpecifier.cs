using System;

namespace Discards
{
    class Program
    {
        static (int, int, int) CompareValues()
        {
            int a = 3;
            int b = 1;
            int c = a - b;
            return (a, b, c);
        }
        static void Main(string[] args)
        {
           var (_, _, sum) = CompareValues();
            Console.WriteLine(sum);
        }
    }
}
