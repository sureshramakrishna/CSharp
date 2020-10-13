using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCreateOptions
{
    class Program
    {
        public static void DenyChildAttach()
        {
            var parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Outer task executing.");

                var child = Task.Factory.StartNew(() =>
                {
                    Thread.SpinWait(500000);
                    Console.WriteLine("Nested task Completed.");
                }, TaskCreationOptions.DenyChildAttach); //default option is DenyChildAttach
            });

            parent.Wait(); //doesn't wait for child to complete.
            Console.WriteLine("Outer has completed.");
        }
        public static void AttachedToParent()
        {
            var parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Outer task executing.");

                var child = Task.Factory.StartNew(() =>
                {
                    Thread.SpinWait(500000);
                    Console.WriteLine("Nested task Completed.");
                }, TaskCreationOptions.AttachedToParent); //attaches child to parent
            });

            parent.Wait(); //waits for child to complete.
            Console.WriteLine("Outer has completed.");
        }
        public static void Main()
        {
            DenyChildAttach();
            AttachedToParent();
        }
    }
}
