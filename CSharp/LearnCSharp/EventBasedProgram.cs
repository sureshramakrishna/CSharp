using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading;


namespace EventBasedPrograms
{
    // This form tests the PrimeNumberCalculator component.

    public delegate void ProgressChangedEventHandler(ProgressChangedEventArgs e);

    public delegate void CalculatePrimeCompletedEventHandler(object sender, CalculatePrimeCompletedEventArgs e);

    class EventBasedProgram
    {
        private static void primeNumberCalculator1_ProgressChanged(ProgressChangedEventArgs e) { }
        private static void primeNumberCalculator1_CalculatePrimeCompleted(object sender, CalculatePrimeCompletedEventArgs e)
        {
            if (e.Cancelled) { }
            else if (e.Error != null) { }
        }
        static void Main(string[] args)
        {
            PrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator();
            primeNumberCalculator.ProgressChanged += new ProgressChangedEventHandler(primeNumberCalculator1_ProgressChanged);
            primeNumberCalculator.CalculatePrimeCompleted += new CalculatePrimeCompletedEventHandler(primeNumberCalculator1_CalculatePrimeCompleted);
            Random rand = new Random();
            int testNumber = rand.Next(200000);
            Guid taskId = Guid.NewGuid();
            primeNumberCalculator.CalculatePrimeAsync(testNumber, taskId);
            Thread.Sleep(Int32.MaxValue);
            primeNumberCalculator.CancelAsync(taskId);
        }
    }


    public class PrimeNumberCalculator
    {
        private delegate void WorkerEventHandler(int numberToCheck, AsyncOperation asyncOp);
        private SendOrPostCallback onProgressReportDelegate;
        private SendOrPostCallback onCompletedDelegate;
        private HybridDictionary userStateToLifetime = new HybridDictionary();
        public event ProgressChangedEventHandler ProgressChanged;
        public event CalculatePrimeCompletedEventHandler CalculatePrimeCompleted;

        public PrimeNumberCalculator()
        {
            InitializeDelegates();
        }

        protected virtual void InitializeDelegates()
        {
            onProgressReportDelegate = new SendOrPostCallback(ReportProgress);
            onCompletedDelegate = new SendOrPostCallback(CalculateCompleted);
        }

        public virtual void CalculatePrimeAsync(int numberToTest, object taskId)
        {
            AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(taskId);
            lock (userStateToLifetime.SyncRoot)
            {
                if (userStateToLifetime.Contains(taskId))
                {
                    throw new ArgumentException( "Task ID parameter must be unique", "taskId");
                }
                userStateToLifetime[taskId] = asyncOp;
            }
            WorkerEventHandler workerDelegate = new WorkerEventHandler(CalculateWorker);
            workerDelegate.BeginInvoke(numberToTest, asyncOp, null, null);
        }
        private bool TaskCanceled(object taskId)
        {
            return (userStateToLifetime[taskId] == null);
        }

        public void CancelAsync(object taskId)
        {
            AsyncOperation asyncOp = userStateToLifetime[taskId] as AsyncOperation;
            if (asyncOp != null)
            {
                lock (userStateToLifetime.SyncRoot)
                {
                    userStateToLifetime.Remove(taskId);
                }
            }
        }

        private void CalculateWorker(int numberToTest, AsyncOperation asyncOp)
        {
            bool isPrime = false;
            int firstDivisor = 1;
            Exception e = null;

            if (!TaskCanceled(asyncOp.UserSuppliedState))
            {
                try
                {
                    ArrayList primes = BuildPrimeNumberList(numberToTest, asyncOp);
                    isPrime = IsPrime(primes, numberToTest, out firstDivisor);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }
            if (!TaskCanceled(asyncOp.UserSuppliedState))
            {
                lock (userStateToLifetime.SyncRoot)
                {
                    userStateToLifetime.Remove(asyncOp.UserSuppliedState);
                }
            }
            CalculatePrimeCompletedEventArgs eventArgs = new CalculatePrimeCompletedEventArgs(e, TaskCanceled(asyncOp.UserSuppliedState), asyncOp.UserSuppliedState);
            asyncOp.PostOperationCompleted(onCompletedDelegate, eventArgs);

        }

        private ArrayList BuildPrimeNumberList(int numberToTest, AsyncOperation asyncOp)
        {
            ProgressChangedEventArgs e = null;
            ArrayList primes = new ArrayList();
            int firstDivisor;
            int n = 5;

            primes.Add(2);
            primes.Add(3);

            while (n < numberToTest && !TaskCanceled(asyncOp.UserSuppliedState))
            {
                if (IsPrime(primes, n, out firstDivisor))
                {
                    e = new CalculatePrimeProgressChangedEventArgs(n, (int)(n / (float)numberToTest * 100), asyncOp.UserSuppliedState);
                    asyncOp.Post(this.onProgressReportDelegate, e);
                    primes.Add(n);
                    Thread.Sleep(0);
                }
                n += 2;
            }
            return primes;
        }
        private void CalculateCompleted(object operationState)
        {
            CalculatePrimeCompletedEventArgs e = operationState as CalculatePrimeCompletedEventArgs;
            CalculatePrimeCompleted?.Invoke(this, e);
        }
        private void ReportProgress(object state)
        {
            ProgressChangedEventArgs e = state as ProgressChangedEventArgs;
            ProgressChanged?.Invoke(e);
        }
        private bool IsPrime(ArrayList primes, int n, out int firstDivisor)
        {
            bool foundDivisor = false;
            bool exceedsSquareRoot = false;
            int i = 0;
            int divisor = 0;
            firstDivisor = 1;

            while ((i < primes.Count) && !foundDivisor && !exceedsSquareRoot)
            {
                divisor = (int)primes[i++];
                if (divisor * divisor > n)
                    exceedsSquareRoot = true;
                else if (n % divisor == 0)
                {
                    firstDivisor = divisor;
                    foundDivisor = true;
                }
            }
            return !foundDivisor;
        }

    }

    public class CalculatePrimeProgressChangedEventArgs : ProgressChangedEventArgs
    {
        private int latestPrimeNumberValue = 1;

        public CalculatePrimeProgressChangedEventArgs(int latestPrime, int progressPercentage, object userToken) : base(progressPercentage, userToken)
        {
            this.latestPrimeNumberValue = latestPrime;
        }

        public int LatestPrimeNumber
        {
            get
            {
                return latestPrimeNumberValue;
            }
        }
    }
    public class CalculatePrimeCompletedEventArgs : AsyncCompletedEventArgs
    {
        private int numberToTestValue = 0;
        private int firstDivisorValue = 1;
        private bool isPrimeValue;

        public CalculatePrimeCompletedEventArgs(Exception e,  bool canceled, object state) : base(e, canceled, state)
        {
        }
    }
}