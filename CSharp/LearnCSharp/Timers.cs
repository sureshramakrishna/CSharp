using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;


namespace Timers
{
    class TimerExample
    {
        static void Main()
        {
            System_Threading_Timers.Simulate();
            System_Timers_Timer.Simulate();
        }
    }

    class System_Threading_Timers
    {
        private static int invokeCount = 0;
        public static void Simulate()
        {
            var autoEvent = new AutoResetEvent(false);
            Console.WriteLine(DateTime.Now);

            //Timer callback can accept min of 0  arguments and max of 1
            //callback will be executed after 1000 milliseconds followed by every 250 milliseconds.
            var stateTimer = new System.Threading.Timer(CallBackMethod, autoEvent, 1000, 250);

            autoEvent.WaitOne();
            stateTimer.Change(0, 500); //callback will be executed after 0 milliseconds followed by every 500 milliseconds.

            autoEvent.WaitOne();
            stateTimer.Dispose(); //stops timer.
        }
        public static void CallBackMethod(Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            Console.WriteLine("{0} Count {1}.", DateTime.Now, ++invokeCount);

            if (invokeCount == 10)
            {
                invokeCount = 0;
                autoEvent.Set();
            }
        }
    }

    public class System_Timers_Timer
    {
        private static System.Timers.Timer timer;
        public static void Simulate()
        {
            SetTimer();

            Console.WriteLine("Press the Enter key to exit the application...");
            Console.WriteLine(DateTime.Now);
            Console.ReadLine();
            timer.Stop();
            timer.Dispose();
        }

        private static void SetTimer()
        { 
            timer = new System.Timers.Timer(2000);       // Create a timer with a two second interval.
            timer.Elapsed += OnTimedEvent;              // Hook up the Elapsed event for the timer. 

            timer.AutoReset = true; //A Boolean indicating whether the Timer should raise the Elapsed event only once (false) or repeatedly (true).
            timer.Enabled = true; //Gets or sets a value indicating whether the Timer should raise the Elapsed event.
            //time.Start() //Sets timer.Enabled to true.
            //timer.Stop() //Sets time.Enable to false.
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }
    }
}
