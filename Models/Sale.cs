using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Sale
    {
        [Key]
        public long Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string StateCode { get; set; }
        public decimal SaleAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
