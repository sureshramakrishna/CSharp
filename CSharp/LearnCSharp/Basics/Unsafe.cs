using System;

namespace Unsafe
{
    class Program
    {
        static void Main(string[] args)
        {
            unsafe
            {
                int test = 0;
                int* ptr = &test;
                Console.WriteLine(*ptr);
            }
        }
    }
}
