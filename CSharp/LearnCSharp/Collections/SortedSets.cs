using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedSets
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedSet<int> sd = new SortedSet<int>();
            sd.Add(1);
            sd.Add(2);
            sd.Remove(2);
            sd.RemoveWhere(x => x < 2);
        }
    }
}
