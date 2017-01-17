using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace StorePortalTests
{
    [TestClass]
    public class StoreTestClass
    {
        private Stack<string> ReportStack { get; set; }
        private decimal SaleAmount { get; set; }
        private string SaleStateCode { get; set; }
        private void QuitEventFired(object sender, EventArgs e)
        {
            Console.WriteLine("Quiting . . .");
        }
        private void ErrorEventFired(object sender, EventArgs e)
        {
            Console.WriteLine($"Error . . . {(e is ErrorEventArgs ? ((ErrorEventArgs)e).Message : "Undefined")}");
        }
        private void SaleEventFired(object sender, SaleEventArgs e)
        {
            this.SaleAmount = e.SaleAmount;
            this.SaleStateCode = e.StateCode;
        }
        private void ReportEventFired(object sender, ReportEventArgs e)
        {
            Console.WriteLine("Report . . .");
        }
        [TestMethod]
        public void TestSale()
        {
            var console = new TestingConsole();
            console.InputStack.Push("R");
            console.InputStack.Push("12.34");
            console.InputStack.Push("CA");
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
            Assert.IsTrue(this.SaleAmount > 12.33995m && this.SaleAmount < 12.3405m);
            Assert.IsTrue(this.SaleStateCode == "CA");
        }
        [TestMethod]
        public void TestReport()
        {
            var console = new TestingConsole();
            console.InputStack.Push("R");
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
            Assert.IsTrue(this.ReportStack.Count > 0);
        }
    }
}
