using System;

namespace Sealed
{
    sealed class B //Inheriting class B throws error.
    {

    }
    class A
    {
        public virtual void FirstMethod()
        {

        }
    }
    class C : A
    {
        sealed public override void FirstMethod() //Any class which derives C and tries to override this method throws error.
        {
        }
        public void SecondMethod() { } //cannot be sealed because it is not an override.
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
