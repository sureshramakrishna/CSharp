using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tuples
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Tuple
            var population = new Tuple<string, int, int, int, int>("New York", 7891957, 7071639, 7322564, 8008278);
            Console.WriteLine("Population of {0} in 2000: {1:N0}", population.Item1, population.Item5);
            population = Tuple.Create("New York", 7891957, 7781984, 7894862, 8008278); //Using Tuple class

            //System.ValueTuple
            var result = QueryCityData("New York City");
            var city = result.Item1;
            var pop = result.Item2;

            (city, pop) = QueryCityData("New York City"); //decouples to city and pop
        }
        private static (string, int) QueryCityData(string name)
        {
            if (name == "New York City")
                return (name, 8175133);
            return ("", 0);
        }
    }
}
