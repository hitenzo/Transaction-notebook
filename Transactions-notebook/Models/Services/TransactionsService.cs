using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transactions_notebook.Controllers;
using Transactions_notebook.Models.Database;
using Transactions_notebook.Models.Entities;

namespace Transactions_notebook.Models.Services
{
    public class TransactionsService
    {
        private QuoteRepository quoteRepository;

        public TransactionsService(QuoteRepository quoteRepo)
        {
            quoteRepository = quoteRepo;
        }

        public IEnumerable<Quote> GetAllQuotes()
        {
            return quoteRepository.GetAll();
        }

        public void AddQuote(QuoteInput quoteInput)
        {
            Quote addedQuote = GetQuoteFromInput(quoteInput);
            quoteRepository.Add(addedQuote);
        }

        public void UpdateQuote(QuoteInput quoteInput)
        {
            Quote updatedQuote = GetQuoteFromInput(quoteInput);
            quoteRepository.Update(updatedQuote);
        }

        public void DeleteQuote(List<string> quoteIds)
        {
            foreach (string quoteId in quoteIds)
            {
                int id = 0;
                if (Int32.TryParse(quoteId, out id))
                {
                    Quote quoteToDelete = quoteRepository.GetById(id);
                    quoteRepository.Delete(quoteToDelete);
                }
            }
        }

        private Quote GetQuoteFromInput(QuoteInput quoteInput)
        {
            Quote addedQuote = new Quote();
            int quoteId = 0;
            if (Int32.TryParse(quoteInput.Id, out quoteId))
                addedQuote.Id = quoteId;
            addedQuote.OperationDate = DateTime.Parse(quoteInput.Date);
            addedQuote.Name = quoteInput.Name;
            addedQuote.Comment = quoteInput.Comment;
            addedQuote.Customer = new Customer() { Name = quoteInput.CustomerName };
            int customerId = 0;
            if (Int32.TryParse(quoteInput.CustomerId, out customerId))
                addedQuote.Customer.Id = customerId;
            addedQuote.TotalAmount = Convert.ToDouble(quoteInput.Price.Replace(".", ","));
            return addedQuote;
        }
    }
}