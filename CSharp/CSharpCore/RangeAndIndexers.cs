using System;
using System.Linq;

namespace CSharpCore
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = Enumerable.Range(1, 10).ToArray();
            var lastElement = array[^1]; //Same as array[array.Length - 1]
            int[] range = array[0..2]; //Set range to 1, 2

            int i = 0, j = 2;
            range = array[i..j];
            range = array[0..^0]; //range from 1 to 10. Note that ^0 does not throw error bcz last range is exclusive.  
            range = array[0..^j]; //range from 1 to 8 
        }
    }
}
