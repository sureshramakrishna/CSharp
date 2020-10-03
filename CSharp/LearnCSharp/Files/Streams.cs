using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Streams
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream_Reader();
            Stream_Writer();
            File_Stream();
        }
        static void Stream_Writer()
        {
            char[] buffer = new char[10];
            StreamWriter streamWriter = new StreamWriter("D:\\Temp\\Test.txt");
            Encoding encoding = streamWriter.Encoding; //Gets current character encoding.
            streamWriter.Flush(); //Write doesn’t actually write data directly to a file, it writes to a buffer. Flush or Close causes buffer contents to be written to file.
            streamWriter.AutoFlush = true; //sets whether stream writer should flush the buffer to the stream after each Write call. 
            Task flush = streamWriter.FlushAsync();
            streamWriter.Write('a');
            Task writeAsync = streamWriter.WriteAsync(buffer, 0, 10);
            streamWriter.WriteLine(); //writes new line char.
            streamWriter.WriteLine("Test"); //writes line followed by new line char
            Task writeLineAsync = streamWriter.WriteLineAsync("Test");
            streamWriter.Close(); //Closes the stream.
        }
        static void Stream_Reader()
        {
            char[] buffer = new char[10];
            StreamReader streamReader = new StreamReader("D:\\Temp\\Test.txt");
            Encoding encoding = streamReader.CurrentEncoding; //Gets current character encoding.
            bool endOfStream = streamReader.EndOfStream; //Gets a value that indicates if current position is end of stream.
            int nextChar = streamReader.Peek(); //Returns the next available character but does not consume it.
            int read = streamReader.Read();
            Task<int> readAsync = streamReader.ReadAsync(buffer, 0, 10); //Reads a specified number of characters from the stream asynchronously and writes the data to a buffer, beginning at the specified index.
            streamReader.ReadBlock(buffer, 0, 10); //reads 10 chars from file and writes to char array starting from 0.
            Task<int> readBlockAsync = streamReader.ReadBlockAsync(buffer, 0, 10); //same as ReadAsync
            string line = streamReader.ReadLine();
            Task<string> readLineAsync = streamReader.ReadLineAsync(); //Reads a line of characters asynchronously from the current stream and returns the data as a string.
            string readToEnd = streamReader.ReadToEnd(); //read from current position to end of file;
            Task<string> readToEndAsync = streamReader.ReadToEndAsync();
            streamReader.BaseStream.Position = 0; //sets position of the stream.
            streamReader.Close(); //Closes the stream.
        }
        static void File_Stream()
        {
            byte[] b = new byte[10];
            FileStream fs = new FileStream("D:\\Test.txt", FileMode.OpenOrCreate);
            fs.Read(b, offset: 0, 10);
            int rb = fs.ReadByte();
            Task<int> ra = fs.ReadAsync(b, offset: 0, 10);
            //Asynchronously writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written. Starts replacing characters.
            fs.Write(b, 0, 10);
            fs.WriteByte(0); //Writes a byte to the current position in the file stream.
            Task wa = fs.WriteAsync(b, offset: 0, 10);
            fs.Seek(10, SeekOrigin.Begin);//Sets the current position of this stream to the given value
            fs.Flush();
            fs.FlushAsync();
            fs.Close();
        }
    }
}
