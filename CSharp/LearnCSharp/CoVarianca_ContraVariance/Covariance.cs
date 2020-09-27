using System;
using System.Collections.Generic;

namespace Covariance
{
    class Program
    {
        /// <summary>
        /// An object that is instantiated with a derived type argument
        /// is assigned to an object instantiated with a base type argument.
        /// Assignment compatibility is preserved.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IEnumerable<string> strings = new List<string>();
            IEnumerable<object> collections = strings;

            object[] array = new string[10];
            // The following statement produces a run-time exception.  Assignment compatibility is preserved.
            // array[0] = 10; 
        }
    }
}
