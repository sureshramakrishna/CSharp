using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedDictionaries
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<int, string> sd = new SortedDictionary<int, string>();
            sd.Add(1, "One");
            sd.Add(2, "Two");
            sd.Remove(2);
        }
    }
}
