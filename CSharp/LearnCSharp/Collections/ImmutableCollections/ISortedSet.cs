using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace ISortedSet
{
    class Program
    {
        static void Main(string[] args)
        {
            ImmutableSortedSet<int> iss = ImmutableSortedSet.Create<int>();
            iss = iss.Add(1);
            iss = iss.Add(2);
            iss = iss.Remove(2);
            ImmutableSortedSet<int>.Builder issBuilder = iss.ToBuilder();
            issBuilder.Add(10); //adds to original SortedSet. returns void.

            ImmutableSortedSet<int>.Builder builder = ImmutableSortedSet.CreateBuilder<int>();
            builder.Add(1);
            iss = builder.ToImmutable();
        }
    }
}
