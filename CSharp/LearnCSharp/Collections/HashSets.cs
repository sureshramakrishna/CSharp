using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashSets
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<int> hs = new HashSet<int>();
            hs.Add(0);
            hs.Add(2);
            hs.Remove(3); //Does not throw error if item does not exist.
        }
    }
}
