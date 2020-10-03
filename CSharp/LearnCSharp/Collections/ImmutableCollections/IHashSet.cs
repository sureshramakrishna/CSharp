using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace IHashSet
{
    class Program
    {
        static void Main(string[] args)
        {
            ImmutableHashSet<int> il = ImmutableHashSet.Create<int>();
            il = il.Add(1);
            il = il.Add(2);
            il = il.Remove(2);
            ImmutableHashSet<int>.Builder issBuilder = il.ToBuilder();
            issBuilder.Add(10); //adds to original Hashset. returns void.

            ImmutableHashSet<int>.Builder builder = ImmutableHashSet.CreateBuilder<int>();
            builder.Add(1);
            il = builder.ToImmutable();
        }
    }
}
