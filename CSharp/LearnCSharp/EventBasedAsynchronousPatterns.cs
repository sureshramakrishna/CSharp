using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;

namespace EventBasedAsynchronousPatterns
{
    public delegate void ProgressChangedEventHandler(ProgressChangedEventArgs e);
    public delegate void TaskCompletedEventHandler(object sender, CalculatePrimeCompletedEventArgs e);

    class Program
    {
        private static PrinterTask _printerTask =  new PrinterTask();
        private static void ProgressChanged_Callback(ProgressChangedEventArgs e)
        {
            File.WriteAllLines(@"D:\" + (Guid)e.UserState + "Output.txt", new List<string> { e.ProgressPercentage.ToString() });
        }
        private static void TaskCompleted_Callback(object sender, CalculatePrimeCompletedEventArgs e)
        {
            Guid taskId = (Guid)e.UserState;
            if (e.Cancelled)
                Console.WriteLine("{0} Cancelled", taskId);
            else if (e.Error != null)
                Console.WriteLine("{0} Error", taskId);
            else
            {
                e.RaiseExceptionIfAny();
                Console.WriteLine("{0} Completed", taskId);
            }
        }
        public static void Main()
        {
            List<Guid> guids = new List<Guid>();
            _printerTask.CalculatePrimeCompleted += new TaskCompletedEventHandler(TaskCompleted_Callback); //Event Handlers
            _printerTask.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged_Callback);

            while (true)
            {
                Console.WriteLine("Choose an Option:");
                Console.WriteLine("1: Create task. 2: Cancel task. 3: Exit.");
                int option = Convert.ToInt32(Console.ReadLine());
                switch(option)
                {
                    case 1:

                        Guid taskId = Guid.NewGuid();
                        _printerTask.StartTask(taskId);
                        guids.Add(taskId);
                        Console.WriteLine("Task Added!");
                        break;
                    case 2:
                        if (guids.Count > 0)
                        {
                            var id = guids[0];
                            guids.Remove(id);
                            _printerTask.CancelAsync(id);
                        }
                        else
                            Console.WriteLine("No tasks to cancel");
                        break;
                    case 3:
                        foreach(var guid in guids)
                            _printerTask.CancelAsync(guid);
                        Thread.Sleep(2000);
                        return;
                }
            }
        }

    }

    public class PrinterTask
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp);
        private HybridDictionary userStateToLifetime = new HybridDictionary();

        public event ProgressChangedEventHandler ProgressChanged;
        public event TaskCompletedEventHandler CalculatePrimeCompleted;


        public virtual void StartTask(object taskId)
        {
            AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(taskId);

            lock (userStateToLifetime.SyncRoot)
            {
                if (userStateToLifetime.Contains(taskId))
                {
                    throw new ArgumentException("Task ID parameter must be unique", "taskId");
                }
                userStateToLifetime[taskId] = asyncOp;
            }

            WorkerEventHandler workerDelegate = new WorkerEventHandler(CalculateWorker);
            workerDelegate.BeginInvoke(asyncOp, null, null);
        }
        private void CalculateWorker(AsyncOperation asyncOp)
        {
            Exception exception = null;
            int n = 0;
            if (!TaskCanceled(asyncOp.UserSuppliedState))
            {
                try
                {
                    ProgressChangedEventArgs eventArgs = null;
                    while (n <= 100 && !TaskCanceled(asyncOp.UserSuppliedState))
                    {
                        eventArgs = new CalculatePrimeProgressChangedEventArgs(n, asyncOp.UserSuppliedState);
                        asyncOp.Post(this.ReportProgress, eventArgs);
                        Thread.Sleep(1000);
                        n++;
                    }
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            }

            if (!TaskCanceled(asyncOp.UserSuppliedState))
            {
                lock (userStateToLifetime.SyncRoot)
                {
                    userStateToLifetime.Remove(asyncOp.UserSuppliedState);
                }
            }
            CalculatePrimeCompletedEventArgs evt = new CalculatePrimeCompletedEventArgs(exception, TaskCanceled(asyncOp.UserSuppliedState), asyncOp.UserSuppliedState);
            asyncOp.PostOperationCompleted(CalculateCompleted, evt);
            // Note that after the call to OperationCompleted, asyncOp is no longer usable, and any attempt to use it will cause an exception to be thrown.
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
        private bool TaskCanceled(object taskId)
        {
            return (userStateToLifetime[taskId] == null);
        }
        private void ReportProgress(object state)
        {
            ProgressChangedEventArgs e = state as ProgressChangedEventArgs;
            ProgressChanged(e);
        }
        private void CalculateCompleted(object operationState)
        {
            CalculatePrimeCompletedEventArgs e = operationState as CalculatePrimeCompletedEventArgs;
            CalculatePrimeCompleted(this, e);
        }

    }

    public class CalculatePrimeProgressChangedEventArgs : ProgressChangedEventArgs
    {
        public CalculatePrimeProgressChangedEventArgs(int progressPercentage, object userToken) : base(progressPercentage, userToken)
        {
        }
    }

    public class CalculatePrimeCompletedEventArgs : AsyncCompletedEventArgs
    {
        public CalculatePrimeCompletedEventArgs(Exception e, bool canceled, object state) : base(e, canceled, state)
        {
        }
        public void RaiseExceptionIfAny()
        {
            RaiseExceptionIfNecessary();
        }
    }
}