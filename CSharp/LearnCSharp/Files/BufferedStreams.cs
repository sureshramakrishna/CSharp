using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BufferedStreams
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Stream fileStream = new FileStream("D:\\Test.txt", FileMode.Open), bufStream = new BufferedStream(fileStream, 100))
            {
                // Check whether the underlying stream supports seeking.
                Console.WriteLine("FileStream {0} seeking.\n", bufStream.CanSeek ? "supports" : "does not support");

                byte[] b = new byte[10];
                // Send and receive data.
                if (bufStream.CanRead)
                {
                    bufStream.Read(b, 0, 10);
                    bufStream.ReadByte();
                }
                if (bufStream.CanWrite)
                {
                    bufStream.Write(b, 0, 10);
                }
                // When bufStream is closed, fileStream is in turn closed.
                bufStream.Close();
            }
        }
    }
}
