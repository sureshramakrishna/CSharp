using System;

namespace Abstract
{
    abstract class BaseClass
    {
        public void Print()
        {
            Console.WriteLine("Prints Me");
        }
        public abstract void OverRideMe();
        public abstract int Counter { get; set; }
    }
    class Derived : BaseClass
    {
        public override int Counter { get; set; }
        public override void OverRideMe()
        {
            throw new NotImplementedException();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Derived d = new Derived();
        }
    }
}
