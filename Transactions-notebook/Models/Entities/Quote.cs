using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Transactions_notebook.Models.Entities;

namespace Transactions_notebook.Models.Entities
{
    public class Quote
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime OperationDate { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public double TotalAmount { get; set; }
    }
}