using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Files
{
    class Program
    {
        static string filePath = @"D:\Test.txt";

        static void FileInfoOperation()
        {
            char[] buffer = new char[10];
            StreamReader streamReader = new StreamReader("D:\\Temp\\Test.txt");
            Encoding encoding = streamReader.CurrentEncoding; //Gets current character encoding.
            bool endOfStream = streamReader.EndOfStream; //Gets a value that indicates if current position is end of stream.
            int nextChar = streamReader.Peek(); //Returns the next available character but does not consume it.
            int read = streamReader.Read();
            Task<int> readAsync = streamReader.ReadAsync(buffer, 0, 10); //Reads a specified maximum number of characters from the current stream asynchronously and writes the data to a buffer, beginning at the specified index.
            streamReader.ReadBlock(buffer, 0, 10); //reads 10 chars from file and writes to char array starting from 0.
            Task<int> readBlockAsync = streamReader.ReadBlockAsync(buffer, 0, 10); //same as ReadAsync
            string line = streamReader.ReadLine();
            Task<string> readLineAsync = streamReader.ReadLineAsync(); //Reads a line of characters asynchronously from the current stream and returns the data as a string.
            string readToEnd = streamReader.ReadToEnd();//read from current position to end of file;
            Task<string> readToEndAsync = streamReader.ReadToEndAsync();
            streamReader.BaseStream.Position = 0; //sets position of the stream.
            streamReader.Close(); //Closes the stream.


            FileInfo fileInfo = new FileInfo("D:\\Temp\\Test.txt");
            var streamWriter = fileInfo.AppendText();
            FileInfo newFile = fileInfo.CopyTo("C:\\Temp\\Test.txt", overwrite: true); //Overwrite replaces dest file if exists.
            FileStream fs = fileInfo.Create(); //Creates a new file.
            StreamWriter createText = File.CreateText("D:\\Temp\\Test.txt"); //Clears existing content of a file if it exists.
            fileInfo.Encrypt(); //Encrypts a file so that only the account used to encrypt the file can decrypt it.
            fileInfo.Decrypt();
            fileInfo.Delete();
            bool exists = fileInfo.Exists;
            fileInfo.MoveTo("C:\\Temp\\Test.txt"); //fails if dest file already exists 
            FileStream fs2 = fileInfo.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite);
            string ext = fileInfo.Extension;
            bool IsReadOnly = fileInfo.IsReadOnly;
            long size = fileInfo.Length;
            fileInfo.Replace(@"D:\test2.txt", @"D:\test3.txt"); //Replaces the contents of a specified file with the file described by the current FileInfo object.
        }
        static void FileOperation()
        {
            File.AppendAllLines(filePath, new List<string> { "Hello", "World" });    //Creates a file if it does not exist, but it doesn't create folder and throws error if folder doesn't exist.
            File.AppendAllText(filePath, "Hello World");
            StreamWriter fileStream = File.AppendText(filePath); //returns a stream writer which can be used to append text.
            File.Copy(sourceFileName: filePath, destFileName: @"D:\Test2.txt", overwrite: true); //Overwrite replaces dest file if exists.
            File.Create(filePath);                                           //Creates or Overwrites the specified file.
            StreamWriter createText = File.CreateText(filePath);             //Clears existing content of a file if it exists.
            File.Encrypt(filePath);                                          //Encrypts a file so that only the account used to encrypt the file can decrypt it.
            File.Decrypt(filePath);
            File.Delete(filePath);
            File.Exists(filePath);
            File.Move(sourceFileName: filePath, destFileName: @"D:\Test.txt");            //fails if dest file already exists 
            FileStream fs = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            byte[] byteData = File.ReadAllBytes(filePath);             //Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
            string[] linesArray = File.ReadAllLines(filePath);
            string lines = File.ReadAllText(filePath);
            IEnumerable<string> lineByLine = File.ReadLines(filePath);             //Reads line by line
            File.WriteAllBytes(filePath, new byte[10]);
            File.WriteAllLines(filePath, new string[10]);
            File.WriteAllText(filePath, "");
            File.Replace(filePath, @"D:\Test2.txt", @"D:\Test3.txt"); //copies test2 to test3, test to test2, deletes test
        }
        static void Main(string[] args)
        {
           
        }
    }
}
