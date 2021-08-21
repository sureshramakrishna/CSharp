using System;
using System.Threading;
using System.Threading.Tasks;

namespace LearnCSharp.Parallelism
{
    public class ReaderWriterLockSample
    {
        static ReaderWriterLock rwl = new ReaderWriterLock();
        // Define the shared resource protected by the ReaderWriterLock.
        static int _resource;
        static bool _running = true;

        // Statistics.
        static int _readerTimeouts;
        static int _writerTimeouts;
        static int _reads;

        public static void Main()
        {
            Task[] t = new Task[26];
            for (var i = 0; i < 26; i++)
                t[i] = Task.Run(ThreadProc);
            Task.WhenAll(t).Wait();
            Console.ReadLine();
        }

        static void ThreadProc()
        {
            Random rnd = new Random();
            // Randomly select a way for the thread to read and write from the shared resource.
            while (_running)
            {
                var action = rnd.NextDouble();
                if(action < .8)
                    ReadFromResource(10);
                else if(action < .81)
                    ReleaseRestore(rnd, 50);
                else if(action < .90)
                    UpgradeDowngrade(rnd, 100);
                else
                    WriteToResource(rnd, 100);
            }
        }

        // Request and release a reader lock, and handle time-outs.
        static void ReadFromResource(int timeOut)
        {
            try
            {
                rwl.AcquireReaderLock(timeOut);
                try
                {
                    Console.WriteLine($"Reading value: {_reads}");
                }
                finally
                {
                    rwl.ReleaseReaderLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        static void WriteToResource(Random rnd, int timeOut)
        {
            try
            {
                rwl.AcquireWriterLock(timeOut);
                try
                {
                    _resource = rnd.Next(500);
                    Console.WriteLine("writes resource value " + _resource);
                }
                finally
                {
                    rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
                Interlocked.Increment(ref _writerTimeouts);
            }
        }

        static void UpgradeDowngrade(Random rnd, int timeOut)
        {
            try
            {
                rwl.AcquireReaderLock(timeOut);
                try
                {
                    Console.WriteLine($"Reading value: {_reads}");
                    try
                    {
                        LockCookie lc = rwl.UpgradeToWriterLock(timeOut);
                        try
                        {
                            _resource = rnd.Next(500);
                            Console.WriteLine("writes resource value " + _resource);
                        }
                        finally
                        {
                            rwl.DowngradeFromWriterLock(ref lc);
                        }
                    }
                    catch (ApplicationException)
                    {
                        Interlocked.Increment(ref _writerTimeouts);
                    }

                    Console.WriteLine($"Reading value: {_reads}");
                }
                finally
                {
                    rwl.ReleaseReaderLock();
                }
            }
            catch (ApplicationException)
            {
                Interlocked.Increment(ref _readerTimeouts);
            }
        }

        static void ReleaseRestore(Random rnd, int timeOut)
        {
            int lastWriter;

            try
            {
                rwl.AcquireReaderLock(timeOut);
                try
                {
                    // It's safe for this thread to read from the shared resource,
                    // so read and cache the resource value.
                    int resourceValue = _resource;     // Cache the resource value.
                    Console.WriteLine($"Reading value: {_reads}");
                    Interlocked.Increment(ref _reads);

                    // Save the current writer sequence number.
                    lastWriter = rwl.WriterSeqNum;

                    // Release the lock and save a cookie so the lock can be restored later.
                    LockCookie lc = rwl.ReleaseLock();

                    // Wait for a random interval and then restore the previous state of the lock.
                    Thread.Sleep(rnd.Next(250));
                    rwl.RestoreLock(ref lc);

                    // Check whether other threads obtained the writer lock in the interval.
                    // If not, then the cached value of the resource is still valid.
                    if (rwl.AnyWritersSince(lastWriter))
                    {
                        resourceValue = _resource;
                        Interlocked.Increment(ref _reads);
                        Console.WriteLine("resource has changed " + resourceValue);
                    }
                    else
                    {
                        Console.WriteLine("resource has not changed " + resourceValue);
                    }
                }
                finally
                {
                    // Ensure that the lock is released.
                    rwl.ReleaseReaderLock();
                }
            }
            catch (ApplicationException)
            {
                // The reader lock request timed out.
                Interlocked.Increment(ref _readerTimeouts);
            }
        }
    }
}
