using System;
using System.Diagnostics;
using System.Threading;
using Core;
using Timer = System.Timers.Timer;

namespace ConsoleApplication
{
    public class Program
    {
        private const string EventSource = "XQ-Wacher";

        public static void Main(string[] args)
        {
            var timer = new Timer(1000);
            Console.WriteLine("Main" + Thread.CurrentThread.ManagedThreadId + Thread.CurrentThread.IsThreadPoolThread);
            timer.Elapsed += (sender, e) =>
            {
                Console.WriteLine("Timer" + Thread.CurrentThread.ManagedThreadId + Thread.CurrentThread.IsThreadPoolThread);
            };
            timer.Enabled = true;
            var i = 0;
            while (true)
            {
                if (i++ != 100000000) continue;
                i = 0;
                Console.WriteLine("While" + Thread.CurrentThread.ManagedThreadId + Thread.CurrentThread.IsThreadPoolThread);
            }

            var openAm = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 9, 35, 0);
            var closeAm = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 11, 35, 0);
            var openPm = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 13, 5, 0);
            var closePm = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 15, 5, 0);
            if (!EventLog.SourceExists(EventSource)) EventLog.CreateEventSource(EventSource, "Application");
            while (true)
            {
                try
                {
                    var watcher = new Watcher();
                    watcher.ConstructRequest();
                    watcher.QueryData();
                    if ((DateTime.Now < openAm || (DateTime.Now > closeAm && DateTime.Now < openPm) || DateTime.Now > closePm)
                        && watcher.IsUpdated)
                    {
                        watcher.SendMail();
                    }
                }
                catch (Exception exception)
                {
                    EventLog.WriteEntry(EventSource, exception.ToString(), EventLogEntryType.Error);
                }
                finally
                {
                    EventLog.WriteEntry(EventSource, "Finish", EventLogEntryType.Information);
                    Thread.Sleep(30000);
                }
            }
        }
    }
}