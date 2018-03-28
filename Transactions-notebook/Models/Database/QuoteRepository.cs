using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Transactions_notebook.Models.DataAccess;
using Transactions_notebook.Models.Entities;

namespace Transactions_notebook.Models.Database
{
    public class QuoteRepository : IRepository<Quote>
    {
        private DatabaseContext databaseContext;

        public QuoteRepository(DatabaseContext context)
        {
            databaseContext = context;
        }

        public void Add(Quote entity)
        {
            databaseContext.Quotes.Add(entity);
            databaseContext.SaveChanges();
        }

        public void Update(Quote entity)
        {
            databaseContext.Quotes.AddOrUpdate(entity);
            databaseContext.SaveChanges();
        }

        public Quote GetById(int id)
        {
            return databaseContext.Quotes.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Quote> GetAll()
        {
            return databaseContext.Quotes.Include(item => item.Customer).ToList();
        }

        public void Delete(Quote entity)
        {
            databaseContext.Quotes.Remove(entity);
            databaseContext.SaveChanges();
        }
    }
}