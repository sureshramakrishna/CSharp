using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace BlockingCollections
{
    class Program
    {
        static BlockingCollection<int> blockingCollection = new BlockingCollection<int>(10);
        static void Main(string[] args)
        {
            blockingCollection.Add(1); //blocks if collection is full and waits till an item is removed from collection
            blockingCollection.TryAdd(1, 10); //blocks for 10 milliseconds and returns flase if item was not added
            int re = blockingCollection.Take();
            bool success = blockingCollection.TryTake(out _);

            Task producerThread = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10; ++i)
                    blockingCollection.Add(i);
                blockingCollection.CompleteAdding(); //Marks that it will not add any more to the collection.
            });
            Task consumerThread = Task.Factory.StartNew(() =>
            {
                while (!blockingCollection.IsCompleted) //It returns true when IsAddingCompleted is true and the BlockingCollection is empty.
                {
                    int item = blockingCollection.Take();
                    Console.WriteLine(item);
                }
            });

        }
    }
}
