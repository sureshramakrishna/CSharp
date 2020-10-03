using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedLists
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedList<int, string> sl = new SortedList<int, string>();
            sl.Add(1, "One");
            sl.Add(2, "Two");
            sl.Remove(2);
            sl.TrimExcess(); // Sets the capacity to the actual number of elements in the list, if that number is less than 90 percent of current capacity.
        }

    }
}
