using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructors
{
    class Program
    {
        static void Main(string[] args)
        {
            Child c = new Child();
        }
    }
    class Parent
    {
        protected static int SomeValue;
        static Parent()
        {
            Console.WriteLine("Parent Static Constructor");
        }
        public Parent()
        {
            Console.WriteLine("Parent Constructor");
        }
    }
    class Child : Parent
    {
        static Child()
        {
            SomeValue = 10; // If this statement is
            Console.WriteLine("Child Static Constructor");
        }
        public Child()
        {
            Console.WriteLine("Child Constructor");
        }
    }
}
