using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;


namespace IStack
{
    class Program
    {
        static void Main(string[] args)
        {
            ImmutableStack<int> @is = ImmutableStack.Create<int>();
            @is = @is.Push(1);
            @is = @is.Pop(out int poppedItem);
        }
    }
}
