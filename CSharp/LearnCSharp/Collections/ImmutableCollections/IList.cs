using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace IList
{
    class Program
    {
        static void Main(string[] args)
        {
            ImmutableList<int> il = ImmutableList.Create<int>();
            il = il.Add(1);
            il = il.Add(2);
            il = il.Remove(2);
            ImmutableList<int>.Builder issBuilder = il.ToBuilder();
            issBuilder.Add(10); //adds to original List. returns void.

            ImmutableList<int>.Builder builder = ImmutableList.CreateBuilder<int>();
            builder.Add(1);
            il = builder.ToImmutable();
        }
    }
}
