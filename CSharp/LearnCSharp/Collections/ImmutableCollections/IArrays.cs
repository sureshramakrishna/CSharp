using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IArrays
{
    class Program
    {
        static void Main(string[] args)
        {
            ImmutableArray<int> ia = ImmutableArray.Create<int>();
            ia = ia.Add(1);
            ia = ia.Insert(0, 2);
            ia = ia.Remove(2);
            ia = ia.AddRange(new int[] { 2, 3 }); //returns a copy of existing array with new items added.
            ImmutableArray<int>.Builder iab = ia.ToBuilder();
            iab.Add(10); //adds to original array. returns void.

            ImmutableArray<int>.Builder builder = ImmutableArray.CreateBuilder<int>();
            builder.Add(1);
            builder.AddRange(new int[] { 2, 3 });
            ia = builder.ToImmutable();
        }
    }
}
