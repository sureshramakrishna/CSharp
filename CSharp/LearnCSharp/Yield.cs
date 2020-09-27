using System;
using System.Collections.Generic;

namespace Yield
{
    class Program
    {
        public static List<int> Items { get; set; } = new List<int> { 0, 1, 2, 3, 4 };
        public static IEnumerable<int> GetItems()
        {
            foreach (var item in Items)
            {
                if (item > 1)
                    yield return item;
                yield break; //Use this to termeniate the yield. 
            }
        }
        public static void Main(string[] args)
        {
            foreach (int i in GetItems())
                Console.WriteLine(i.ToString());
        }
    }

}
