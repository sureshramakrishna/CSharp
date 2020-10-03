using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace ISortedDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            ImmutableSortedDictionary<int, int> isd = ImmutableSortedDictionary.Create<int, int>();
            isd = isd.Add(1, 1);
            isd = isd.Add(2, 2);
            isd = isd.Remove(2);
            ImmutableSortedDictionary<int, int>.Builder isdBuilder = isd.ToBuilder();
            isdBuilder.Add(10, 10); //adds to original SortedDictionary. returns void.

            ImmutableSortedDictionary<int, int>.Builder builder = ImmutableSortedDictionary.CreateBuilder<int, int>();
            builder.Add(3, 3);
            isd = builder.ToImmutable();
        }
    }
}
