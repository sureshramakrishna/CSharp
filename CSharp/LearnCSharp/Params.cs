using System;

namespace Params
{
    class Program
    {
        static void PrintUs(params string[] list)
        {
            foreach (var s in list)
                Console.WriteLine(s);
        }
        static void Main(string[] args)
        {
            PrintUs("Hello World!", "FSPA");
            PrintUs(new string[] { "Hello World!", "FSAP"});
        }
    }
}
