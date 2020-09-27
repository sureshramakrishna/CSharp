using System;

namespace TypeParameters
{
    public class Pair<TFirst, TSecond>
    {
        public TFirst First;
        public TSecond Second;
    }

    class Program
    {
        Pair<int, string> pair = new Pair<int, string> { First = 1, Second = "two" };

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
