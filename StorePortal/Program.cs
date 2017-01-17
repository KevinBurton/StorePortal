using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Controllers;
using Models;

namespace StorePortal
{
    class Program
    {
        private static void QuitEventFired(object sender, EventArgs e)
        {
            Console.WriteLine("Quiting . . .");
        }
        private static void ErrorEventFired(object sender, EventArgs e)
        {
            Console.WriteLine($"Error . . . {(e is ErrorEventArgs ? ((ErrorEventArgs) e).Message : "Undefined")}");
        }
        private static void SaleEventFired(object sender, EventArgs e)
        {
            Console.WriteLine("Sale . . .");
        }
        private static void ReportEventFired(object sender, EventArgs e)
        {
            Console.WriteLine("Report . . .");
        }
        static void Main(string[] args)
        {
            var console = new RealConsole();
            var userHandler = new UserHandler();
            userHandler.Quit += QuitEventFired;
            userHandler.Error += ErrorEventFired;
            userHandler.Sale += SaleEventFired;
            userHandler.Report += ReportEventFired;
            var controller = new ConsoleInputController(console, userHandler);
            controller.ProcessEvents();
            userHandler.Quit -= QuitEventFired;
            userHandler.Error -= ErrorEventFired;
            userHandler.Sale -= SaleEventFired;
            userHandler.Report -= ReportEventFired;
        }
    }
}
