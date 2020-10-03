using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedLists
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddFirst(10);
            list.AddLast(30);
            list.AddAfter(list.Find(10), 15);
            list.AddBefore(list.Find(10), 0);
            list.Remove(0);
            list.RemoveLast();
        }
    }
}
