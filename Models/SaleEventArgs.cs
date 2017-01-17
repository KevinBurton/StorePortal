using System;

namespace Models
{
    public class SaleEventArgs : EventArgs
    {
        public DateTime EntryDate { get; set; }
        public string StateCode { get; set; }
        public decimal SaleAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
