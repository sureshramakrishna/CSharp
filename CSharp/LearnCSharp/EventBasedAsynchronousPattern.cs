using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading;

namespace AsyncOperationManagerExample
{
    public class PrimeNumberCalculatorMain
    {
        private PrimeNumberCalculator primeNumberCalculator;
        private int progressCounter;
        private int progressInterval = 100;
        public PrimeNumberCalculatorMain()
        {
            primeNumberCalculator = new PrimeNumberCalculator();
            this.primeNumberCalculator.CalculatePrimeCompleted += new CalculatePrimeCompletedEventHandler(primeNumberCalculator1_CalculatePrimeCompleted);
            this.primeNumberCalculator.ProgressChanged += new ProgressChangedEventHandler(primeNumberCalculator1_ProgressChanged);
        }


        public Guid startAsyncButton_Click()
        {
            Random rand = new Random();
            int testNumber = rand.Next(200000);
            Guid taskId = Guid.NewGuid();
            this.primeNumberCalculator.CalculatePrimeAsync(testNumber, taskId);
            return taskId;
        }

        public void cancelButton_Click(Guid taskId)
        {
            this.primeNumberCalculator.CancelAsync(taskId);
        }

        private void primeNumberCalculator1_ProgressChanged(ProgressChangedEventArgs e)
        {
            if (this.progressCounter++ % this.progressInterval == 0)
            {
                Guid taskId = (Guid)e.UserState;

                if (e is CalculatePrimeProgressChangedEventArgs)
                {
                    CalculatePrimeProgressChangedEventArgs cppcea = e as CalculatePrimeProgressChangedEventArgs;
                }
                else
                {
                }
            }
            else if (this.progressCounter > this.progressInterval)
            {
                this.progressCounter = 0;
            }
        }

        private void primeNumberCalculator1_CalculatePrimeCompleted(object sender, CalculatePrimeCompletedEventArgs e)
        {
            Guid taskId = (Guid)e.UserState;
            if (e.Cancelled)
            {
                Console.WriteLine("{0} Cancelled", taskId);
            }
            else if (e.Error != null)
            {
                Console.WriteLine("{0} Error", taskId);
            }
            else
            {
                bool result = e.IsPrime;
                Console.WriteLine("{0} Result: {1}", taskId, result);
            }
        }

    }
    class Program
    {
        public static void Main()
        {
            PrimeNumberCalculatorMain pncm = new PrimeNumberCalculatorMain();
            pncm.startAsyncButton_Click();
        }
    }


    public delegate void ProgressChangedEventHandler(ProgressChangedEventArgs e);
    public delegate void CalculatePrimeCompletedEventHandler(object sender, CalculatePrimeCompletedEventArgs e);

    public class PrimeNumberCalculator : Component
    {
        private delegate void WorkerEventHandler(int numberToCheck, AsyncOperation asyncOp);

        private SendOrPostCallback onProgressReportDelegate;
        private SendOrPostCallback onCompletedDelegate;

        private HybridDictionary userStateToLifetime = new HybridDictionary();

        private System.ComponentModel.Container components = null;

        public event ProgressChangedEventHandler ProgressChanged;
        public event CalculatePrimeCompletedEventHandler CalculatePrimeCompleted;

        public PrimeNumberCalculator(IContainer container)
        {
            container.Add(this);
            InitializeComponent();

            InitializeDelegates();
        }

        public PrimeNumberCalculator()
        {
            InitializeComponent();

            InitializeDelegates();
        }

        protected virtual void InitializeDelegates()
        {
            onProgressReportDelegate = new SendOrPostCallback(ReportProgress);
            onCompletedDelegate = new SendOrPostCallback(CalculateCompleted);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        public virtual void CalculatePrimeAsync(int numberToTest, object taskId)
        {
            AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(taskId);

            lock (userStateToLifetime.SyncRoot)
            {
                if (userStateToLifetime.Contains(taskId))
                {
                    throw new ArgumentException(
                        "Task ID parameter must be unique",
                        "taskId");
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
                    int n = 0;
                    ProgressChangedEventArgs eventArgs = null;
                    while (n < int.MaxValue && !TaskCanceled(asyncOp.UserSuppliedState))
                    {
                        float percentage = (float)n * 100 / int.MaxValue;
                        if ((percentage % 1) > 0.000001)
                        {
                            eventArgs = new CalculatePrimeProgressChangedEventArgs(n, (int)((float)n / (float)numberToTest * 100), asyncOp.UserSuppliedState);
                            asyncOp.Post(this.onProgressReportDelegate, eventArgs);
                            Thread.Sleep(0);
                        }
                        n++;
                    }
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }

            this.CompletionMethod(numberToTest, firstDivisor, isPrime, e, TaskCanceled(asyncOp.UserSuppliedState), asyncOp);
        }



        // This method is invoked via the AsyncOperation object,
        // so it is guaranteed to be executed on the correct thread.
        private void CalculateCompleted(object operationState)
        {
            CalculatePrimeCompletedEventArgs e =
                operationState as CalculatePrimeCompletedEventArgs;

            OnCalculatePrimeCompleted(e);
        }

        // This method is invoked via the AsyncOperation object,
        // so it is guaranteed to be executed on the correct thread.
        private void ReportProgress(object state)
        {
            ProgressChangedEventArgs e =
                state as ProgressChangedEventArgs;

            OnProgressChanged(e);
        }

        protected void OnCalculatePrimeCompleted(
            CalculatePrimeCompletedEventArgs e)
        {
            if (CalculatePrimeCompleted != null)
            {
                CalculatePrimeCompleted(this, e);
            }
        }

        protected void OnProgressChanged(ProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(e);
            }
        }

        private void CompletionMethod(int numberToTest, int firstDivisor, bool isPrime, Exception exception, bool canceled, AsyncOperation asyncOp)
        {
            if (!canceled)
            {
                lock (userStateToLifetime.SyncRoot)
                {
                    userStateToLifetime.Remove(asyncOp.UserSuppliedState);
                }
            }

            CalculatePrimeCompletedEventArgs e = new CalculatePrimeCompletedEventArgs(isPrime, exception, canceled, asyncOp.UserSuppliedState);
            asyncOp.PostOperationCompleted(onCompletedDelegate, e);
            // Note that after the call to OperationCompleted, 
            // asyncOp is no longer usable, and any attempt to use it
            // will cause an exception to be thrown.
        }



        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

    }

    public class CalculatePrimeProgressChangedEventArgs : ProgressChangedEventArgs
    {
        public int LatestPrimeNumber { get; } = 1;
        public CalculatePrimeProgressChangedEventArgs(int latestPrime, int progressPercentage, object userToken) : base(progressPercentage, userToken)
        {
            this.LatestPrimeNumber = latestPrime;
        }

    }

    public class CalculatePrimeCompletedEventArgs : AsyncCompletedEventArgs
    {
        private bool isPrimeValue;

        public CalculatePrimeCompletedEventArgs(bool isPrime, Exception e, bool canceled, object state) : base(e, canceled, state)
        {
            this.isPrimeValue = isPrime;
        }

        public bool IsPrime
        {
            get
            {
                RaiseExceptionIfNecessary();
                return isPrimeValue;
            }
        }
    }
}