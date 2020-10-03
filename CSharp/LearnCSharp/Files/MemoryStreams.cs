using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryStreams
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryStream ms = new MemoryStream(capacity: 100);
            byte[] b = new byte[10];
            ms.Write(b, 0, 10);
            ms.Read(b, 0, 10);
            Task<int> t = ms.ReadAsync(b, 0, 10);
            ms.Seek(0, SeekOrigin.Begin);
        }
    }
}
