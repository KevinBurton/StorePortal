using System;
using System.Linq;
using System.Threading;
using DataAccess;
using Models;

namespace Controllers
{
    public class ConsoleInputController :  IProcessController
    {
        public ConsoleInputController(IConsole console, IUserHandler handler)
        {
            this.Console = console;
            this.Status = StatusEnum.Undefined;
            this.Handler = handler;
        }
        private IConsole Console { get; set; }
        private IUserHandler Handler { get; set; }
        private StatusEnum Status { get; set; }
        private void ProcessSale(object sender, SaleEventArgs e)
        {
            using (var db = new SalesContext())
            {
                var sale = new Sale()
                {
                    EntryDate = e.EntryDate,
                    SaleAmount = e.SaleAmount,
                    StateCode = e.StateCode,
                    TaxRate = e.TaxRate,
                    TotalAmount = e.TotalAmount
                };
                db.Sale.Add(sale);
                db.SaveChanges();
            }
            this.Handler.OnSale(e);
        }
        private void ProcessReport(object sender, ReportEventArgs e)
        {
            using (var db = new SalesContext())
            {
                var query = from s in db.Sale
                            orderby s.EntryDate descending
                            select s;
                foreach (var sale in query)
                {
                    var dateString = sale.EntryDate.ToString("dd/mm/yyyy");
                    var amountString = sale.SaleAmount.ToString("C");
                    var rateString = sale.TaxRate.ToString("P");
                    var totalString = sale.TotalAmount.ToString("C");
                    var resultString = $"{dateString} {sale.StateCode} {amountString} {rateString} {totalString}";
                    this.Console.WriteLine(resultString);
                    e.ReportStack.Push(resultString);
                }
            }
            this.Handler.OnReport(e);
        }
        private void ProcessError(object sender, ErrorEventArgs e)
        {
            this.Handler.OnError(e);
        }
        private void ProcessQuit(object sender, EventArgs e)
        {
            this.Status = StatusEnum.Quit;
            this.Handler.OnQuit(e);
        }

        private void PopulateTaxRates(StateTaxRateContext db)
        {
            var taxRates = new StateTaxRate[]
            {
                new StateTaxRate() {StateCode = "CA", TaxRate = 0.09m},
                new StateTaxRate() {StateCode = "AL", TaxRate = 0.10m},
                new StateTaxRate() {StateCode = "UT", TaxRate = 0.11m},
                new StateTaxRate() {StateCode = "CO", TaxRate = 0.12m},
                new StateTaxRate() {StateCode = "MA", TaxRate = 0.13m},
                new StateTaxRate() {StateCode = "VT", TaxRate = 0.14m},
                new StateTaxRate() {StateCode = "TX", TaxRate = 0.15m},
                new StateTaxRate() {StateCode = "FL", TaxRate = 0.16m},
                new StateTaxRate() {StateCode = "LA", TaxRate = 0.17m},
                new StateTaxRate() {StateCode = "WI", TaxRate = 0.05m}
            };
            foreach(var taxRate in taxRates)
            {
                db.SalesTaxRate.Add(taxRate);
            }
            db.SaveChanges();
        }
        private void InitializeStateTaxRates()
        {
            using (var db = new StateTaxRateContext())
            {
                try
                {
                    var query = from b in db.SalesTaxRate
                                select b;
                    if (query.Any()) return;
                    PopulateTaxRates(db);
                }
                catch (Exception)
                {
                    PopulateTaxRates(db);
                }
            }
        }

        private decimal? GetStateTaxRate(string stateCode)
        {
            using (var db = new StateTaxRateContext())
            {
                var query = from b in db.SalesTaxRate
                    where b.StateCode == stateCode
                    select b;
                try
                {
                    return query.First().TaxRate;
                }
                catch (Exception)
                {
                    return null; 
                }
            }
        }

        public void ProcessEvents()
        {
            InitializeStateTaxRates();
            while (this.Status != StatusEnum.Quit)
            {
                try
                {
                    decimal saleAmount;
                    decimal? stateTax;
                    decimal finalStateTaxRate;
                    var input = this.Console.ReadKey(true);
                    switch (char.ToUpper(input.KeyChar))
                    {
                        case 'S':
                            var sale = new Sale();
                            this.Console.Write("Sale amount: ");
                            var saleInput = this.Console.ReadLine();
                            if (decimal.TryParse(saleInput, out saleAmount))
                            {
                                this.Console.Write("State code: ");
                                saleInput = this.Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(saleInput) && saleInput.Length > 1)
                                {
                                    var stateCode = saleInput.Substring(0, 2).ToUpperInvariant();
                                    stateTax = GetStateTaxRate(stateCode);
                                    if (stateTax.HasValue)
                                    {
                                        finalStateTaxRate = stateTax.Value;
                                    }
                                    else
                                    {
                                        stateCode = "XX";
                                        finalStateTaxRate = StateTaxRate.DefaultTaxRate;
                                    }
                                    ProcessSale(this, new SaleEventArgs()
                                    {
                                        EntryDate = DateTime.UtcNow,
                                        StateCode = stateCode,
                                        SaleAmount = saleAmount,
                                        TaxRate = finalStateTaxRate,
                                        TotalAmount = saleAmount * (1.0m + finalStateTaxRate)
                                    });
                                    this.Console.Write("Finished transaction");
                                }
                                else
                                {
                                    ProcessError(this, new ErrorEventArgs($"Invalid state code: {saleInput}"));
                                }
                            }
                            else
                            {
                                ProcessError(this, new ErrorEventArgs($"Invalid sale amount: {saleInput}"));
                            }
                            break;
                        case 'R':
                            ProcessReport(this, new ReportEventArgs());
                            this.Console.Write("Finished report");
                            break;
                        case 'Q':
                            ProcessQuit(this, EventArgs.Empty);
                            return;
                        default:
                            ProcessError(this, new ErrorEventArgs("Invalid input"));
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ProcessError(this, new ErrorEventArgs(ex.ToString()));
                }
                Thread.Sleep(500);
            }
        }
    }
}
