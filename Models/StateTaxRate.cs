using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class StateTaxRate
    {
        public const decimal DefaultTaxRate = 0.06m;
        [Key]
        public string StateCode { get; set; }
        public decimal TaxRate { get; set; }
    }
}
