using System;

namespace CovarianceAndContraVariance
{
    class Program
    {
        static void SetObject(object o) { }
        static void Main(string[] args)
        {
            Action<object> actObject = SetObject;
            Action<string> actString = actObject;
            actString("Hello!");
            //actString(1); throws error. Assignment compatibility is reversed.
        }
    }
}
