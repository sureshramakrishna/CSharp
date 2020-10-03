using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paths
{
    class Program
    {
        static void Main(string[] args)
        {
            string newExt = Path.ChangeExtension("D:\\Test.txt", "rtf");
            string path = Path.Combine("D:\\Temp", "Test.txt");
            string dir = Path.GetDirectoryName("D:\\Temp\\Test.txt"); //returns D:\Temp
            string ext = Path.GetExtension("D:\\Temp\\Test.txt");
            string fileName = Path.GetFileName("D:\\Temp\\Test.txt");
            string fileNameWithOutExt = Path.GetFileNameWithoutExtension("D:\\Temp\\Test.txt");
            string randFolder = Path.GetRandomFileName(); //returns random text for folder/file name
            bool hasExtenstion = Path.HasExtension("D:\\Temp\\Test.txt");
        }
    }
}
