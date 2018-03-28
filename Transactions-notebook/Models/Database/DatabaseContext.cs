using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Transactions_notebook.Models.Entities;

namespace Transactions_notebook.Models.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=TransactionsDatabase")
        {
            
        }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<Customer> Customers { get; set; }
    }
}