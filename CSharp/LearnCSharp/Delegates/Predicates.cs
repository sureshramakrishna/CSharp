using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predicates
{
    class Program
    {
        public static bool IsNegative(int number)
        {
            return number < 0;
        }
        static void Main(string[] args)
        {
            Predicate<int> predicate = IsNegative; //notice that there is no need to define a delegate.
            bool result = predicate.Invoke(10);
        }
    }
}
