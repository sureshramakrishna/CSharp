using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace StackAndQueue
{
    class Program
    {
        static Stack<int> stack = new Stack<int>();
        static Queue<int> queue = new Queue<int>();
        static void StackOps()
        {
            stack.Push(1);
            stack.Push(2);
            int topItem = stack.Peek();
            int[] arr = new int[stack.Count];
            stack.CopyTo(arr, 0); // arr = {2, 1}
            int pop = stack.Pop();
        }
        static void QueueOps()
        {
            queue.Enqueue(1);
            queue.Enqueue(2);
            int firstItem = queue.Peek();
            int[] arr = new int[queue.Count];
            queue.CopyTo(arr, 0); // arr = {1, 2}
            int pop = queue.Dequeue();
        }
        static void Main(string[] args)
        {
            StackOps();
            QueueOps();
            Console.WriteLine("Hello World!");
        }
    }
}
