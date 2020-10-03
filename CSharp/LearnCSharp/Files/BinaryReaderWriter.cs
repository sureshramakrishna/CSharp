using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BinaryReaderWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            Binary_Reader();
            Binary_Writer();
        }
        static void Binary_Reader()
        {
            BinaryReader br = new BinaryReader(File.OpenRead("D:\\Test.txt"));
            br.ReadInt32();
            br.ReadInt64();
            br.ReadDouble();
            int r = br.Read(); //reads 1 char from the stream
            br.ReadByte();
            br.BaseStream.Seek(0, SeekOrigin.Begin);
        }
        static void Binary_Writer()
        {
            BinaryWriter bw = new BinaryWriter(File.OpenWrite("D:\\Test.txt"));
            bw.BaseStream.Seek(0, SeekOrigin.Begin);
            bw.Write(true);
            bw.Write('c');
            bw.Write(10.0);
            bw.Write(10);
            bw.Write("Test");
        }
    }
}
