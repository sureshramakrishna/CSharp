using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TaskBasedAsynchronousPattern
{
    public class Program
    {
        public delegate bool Prime(int number);
        static void Main(string[] args)
        {
            Stream sr = new FileStream("C:\\File.txt", FileMode.Open);
            var task = sr.ReadTask(new byte[int.MaxValue], 0, int.MaxValue, 0);
            task.Wait();
        }
    }
    public static class ExtensionMethods
    {
        public static Task<int> ReadTask(this Stream stream, byte[] buffer, int offset, int count, object state)
        {
            var tcs = new TaskCompletionSource<int>();
            stream.BeginRead(buffer, offset, count, ar =>
            {
                try { tcs.SetResult(stream.EndRead(ar)); }
                catch (Exception exc) { tcs.SetException(exc); }
            }, state);
            return tcs.Task;
        }
    }
}
