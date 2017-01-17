using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess
{
    public class SalesContext :  DbContext
    {
        public DbSet<Sale> Sale { get; set; }
    }
}
