using System;

namespace RefVsOut
{
    class Program
    {

        static void Main(string[] args)
        {
            int a = 10; //throws error if a is not assigned. 
            int b;
            RefTest(ref a, out b);
        }
        static void RefTest(ref int a, out int b)
        {
            b = 10; //throws error if b is not assigned inside this method.
        }
    }
}
