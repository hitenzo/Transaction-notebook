using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transactions_notebook.Models.Entities
{
    public class QuoteInput
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public string Price { get; set; }
    }
}