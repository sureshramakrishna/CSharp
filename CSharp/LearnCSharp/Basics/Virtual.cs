using System;

namespace Virtual
{
    class Base
    {
        public virtual void Print()
        {
            Console.WriteLine("Base Class Print Method");
        }
    }
    class Derived : Base
    {
        public override void Print()
        {
            Console.WriteLine("Derived Class Print Method");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Base bc = new Base();
            bc.Print(); //Prints base class
            bc = new Derived(); //Cannot assign base class to derived class, but derived class can be assigned to base class.
            bc.Print(); //will now invoke Derived class’s Print(). Without virtual and overridden, BaseClass.Test() will get invoked.
            Derived dc = new Derived(); 
            dc.Print(); //Prints derived class
        }
    }
}
