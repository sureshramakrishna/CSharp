using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structs
{
    struct Book
    {
        public int Id { get; set;}
        public string Name { get; set;}
    }
    class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book { Id = 1, Name = "Trust!" };
        }
    }
}
