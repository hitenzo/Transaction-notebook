using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Transactions_notebook.Models;
using Transactions_notebook.Models.DataAccess;
using Transactions_notebook.Models.Database;
using Transactions_notebook.Models.Entities;
using Transactions_notebook.Models.Services;

namespace Transactions_notebook.Controllers
{
    public class HomeController : Controller
    {
        private TransactionsService transactionsService;

        public HomeController()
        {
            DatabaseContext context = new DatabaseContext();
            QuoteRepository quoteRepo = new QuoteRepository(context);
            transactionsService = new TransactionsService(quoteRepo);
        }

        public ActionResult Index()
        {
            return View();
        }

        public string GetAllTransactions()
        {
            IEnumerable<Quote> allQuotes = transactionsService.GetAllQuotes();
            string jsonQuotes = JsonConvert.SerializeObject(allQuotes, Formatting.Indented);
            return jsonQuotes;
        }

        [HttpPost]
        public void AddTransaction(QuoteInput quoteInput)
        {
            if (quoteInput != null)
                transactionsService.AddQuote(quoteInput);
        }

        [HttpPost]
        public void DeleteTransaction(List<string> quoteIds)
        {
            if(quoteIds.Count > 0)
                transactionsService.DeleteQuote(quoteIds);
        }

        [HttpPost]
        public void UpdateTransaction(QuoteInput quoteInput)
        {
            if (quoteInput != null)
                transactionsService.UpdateQuote(quoteInput);
        }
    }
}