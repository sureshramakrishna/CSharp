using System;
using System.Collections.Immutable;

namespace ImmutableCollections
{
    class Program
    {
        public delegate void AsyncMethodCaller();

        static void ImmutableArrays()
        {
            ImmutableArray<int> ia = ImmutableArray.Create<int>();
            ia = ia.Add(1);
            ia = ia.Insert(0, 2);
            ia = ia.Remove(1);

            ImmutableArray<int>.Builder builder = ImmutableArray.CreateBuilder<int>();
            builder.Add(1);
            ia = builder.ToImmutable();
        }
        static void Main(string[] args)
        {
            ImmutableArrays();
        }
    }
}
