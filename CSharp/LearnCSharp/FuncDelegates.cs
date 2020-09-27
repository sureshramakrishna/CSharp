using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncDelegates
{
    class Program
    {
        public static int AddMe(int a, int b)
        {
            return a + b;
        }
        static void Main(string[] args)
        {
            Func<int, int, int> func = AddMe;
            func.Invoke(1, 2);
        }
    }
}
