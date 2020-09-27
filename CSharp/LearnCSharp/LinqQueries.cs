using System;
using System.Linq;
using System.Collections.Generic;

namespace LinqQueries
{
    class Program
    {
        static List<string> collection = new List<string> { "Aman", "Bman", "Cman", "Demon", "AnotherAman" };
        static void Main(string[] args)
        {
            var fromQuery = from str in collection
                            select str;
            var whereQuery = from str in collection
                             where str.StartsWith("B")
                             select str;
            var selectQuery = from str in collection
                              select str[0]; //selects only first char in each string
            var groupQuery = from str in collection
                             group str by str[0]; //return will be like dictionary, where key is the str[0] and value is collection which contain matches
            var intoQuery = from str in collection
                            group str by str[0] into letterGroup
                            where letterGroup.Count() >= 2 //its collection count not total group count
                            select new { FirstLetter = letterGroup.Key, Words = letterGroup.Count() };
            var orderByQuery = from str in collection
                               orderby str
                               select str;

            var letQuery = from str in collection
                           let words = str.ToLower()
                           where words[0] == 'a'
                           select words;
        }
    }
}
