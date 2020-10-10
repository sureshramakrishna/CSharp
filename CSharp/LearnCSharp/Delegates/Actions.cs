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
            Action<int, int> arithmeticOperation = Sum; //Notice that there is no need to define a delegate. Purpose of Action is to save you from defining delegates. 
            arithmeticOperation += Subtract;
            arithmeticOperation(10, 5);

            Action delegateAction = delegate () { Sum(10, 20); }; //Note that Action points to delegate which in turn calls Sum, hence it's an Action with 0 parameters.
            delegateAction.Invoke();
            Action lambdaAction = () => { Sum(10, 20); };
            lambdaAction.Invoke();
        }
    }
}