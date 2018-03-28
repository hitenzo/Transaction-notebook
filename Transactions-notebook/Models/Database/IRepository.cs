using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transactions_notebook.Models.Database
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Delete(T entity);
    }
}