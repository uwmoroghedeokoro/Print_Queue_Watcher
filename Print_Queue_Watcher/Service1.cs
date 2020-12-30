using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Print_Queue_Watcher
{
    public partial class Service1 : ServiceBase
    {
        static Timer timer = new Timer(10);
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        protected override void OnStop()
        {

        }
        private static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                // Console.WriteLine("L");
                PrintServer ps = new PrintServer();
                // PrintServer ps = new PrintServer(System.Printing.PrintSystemDesiredAccess.AdministrateServer);
                // PrintQueueCollection pQueue = ps.GetPrintQueues();
                PrintQueueCollection pQueue = ps.GetPrintQueues();
                // new PrintQueue(ps, printer.printer_name, PrintSystemDesiredAccess.AdministratePrinter)
                int x = 1;
                foreach (PrintQueue pq in pQueue)
                {
                    //  Console.WriteLine(pq.Name);

                    foreach (var job in ps.GetPrintQueue(pq.Name).GetPrintJobInfoCollection())
                    {
                        job.Pause();
                        // Console.WriteLine("Paused - " + job.Name);
                    }
                    //  Console.WriteLine("----");
                }


            }
            catch (Exception ex)
            {

            }


        }
    }
}
