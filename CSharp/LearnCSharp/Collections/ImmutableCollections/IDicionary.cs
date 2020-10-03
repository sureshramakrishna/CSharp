using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace IDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            ImmutableDictionary<int, int> id = ImmutableDictionary.Create<int, int>();
            id = id.Add(1, 1);
            id = id.Add(2, 2);
            id = id.Remove(2);
            ImmutableDictionary<int, int>.Builder idBuilder = id.ToBuilder();
            idBuilder.Add(10, 10); //adds to original Dictionary. returns void.

            ImmutableDictionary<int, int>.Builder builder = ImmutableDictionary.CreateBuilder<int, int>();
            builder.Add(3, 3);
            id = builder.ToImmutable();
        }
    }
}
