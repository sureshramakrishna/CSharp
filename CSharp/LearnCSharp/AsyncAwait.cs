using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public class Program
    {
        public static void Main()
        {
            Task t = GetPageSizeAsync("https://google.com");
            Console.WriteLine("Waiting..");
            t.Wait();
        }

        private static async Task GetPageSizeAsync(string url)
        {
            var client = new HttpClient();
            var uri = new Uri(Uri.EscapeUriString(url));
            byte[] urlContents = await client.GetByteArrayAsync(uri);
            Console.WriteLine($"{url}: {urlContents.Length / 2:N0} characters");
        }
    }
}
