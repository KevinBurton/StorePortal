using System.Data.Entity;
using Models;

namespace DataAccess
{
    public class StateTaxRateContext : DbContext
    {
        public DbSet<StateTaxRate> SalesTaxRate { get; set; }
    }
}
