using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions
{
    class Program
    {
        public static void Sum(int a, int b)
        {
            Console.WriteLine(a + b);
        }
        public static void Subtract(int a, int b)
        {
            Console.WriteLine(a - b);
        }

        static void Main(string[] args)
        {
            Action<int, int> arithmeticOperation = Sum;
            arithmeticOperation += Subtract;
            arithmeticOperation(10, 5);
        }
    }
}