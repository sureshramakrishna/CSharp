using System;
namespace PartialClass
{
    public partial class Mathematics
    {
        public int Sub(int a, int b)
        {
            return a - b;
        }
    }
    public partial class Mathematics
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
