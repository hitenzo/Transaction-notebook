using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Transactions_notebook.Models.DataAccess;
using Transactions_notebook.Models.Entities;

namespace Transactions_notebook.Models.Database
{
    public class CustomerRepository : IRepository<Customer>
    {
        private DatabaseContext databaseContext;

        public CustomerRepository(DatabaseContext context)
        {
            databaseContext = context;
        }

        public void Add(Customer entity)
        {
            databaseContext.Customers.Add(entity);
            databaseContext.SaveChanges();
        }

        public void Update(Customer entity)
        {
            databaseContext.Customers.AddOrUpdate(entity);
            databaseContext.SaveChanges();
        }

        public Customer GetById(int id)
        {
            return databaseContext.Customers.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return databaseContext.Customers;
        }

        public void Delete(Customer entity)
        {
            databaseContext.Customers.Remove(entity);
            databaseContext.SaveChanges();
        }
    }
}