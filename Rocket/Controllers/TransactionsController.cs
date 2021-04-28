using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket.Controllers.ViewModels;
using Rocket.Repositories;

namespace Rocket.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly RocketDbContext rocketDbContext;


        public TransactionsController(RocketDbContext dbContext)
        {
            rocketDbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Models.Transaction> Get()
        {
            return rocketDbContext.Transactions.ToList();
        }

        [HttpPost]
        public ActionResult<Models.Transaction> Post([FromBody] TransactionViewModel transaction)
        {
            var user = rocketDbContext.Users.Find(transaction.UserId);
            var newTransaction = new Models.Transaction {
                Id = transaction.Id,
                Type = transaction.Type,
                Amount = transaction.Amount,
                Comment = transaction.Comment,
                User = user
            };
            rocketDbContext.Transactions.Add(newTransaction);
            rocketDbContext.SaveChanges();

            return CreatedAtAction(nameof(Post), new { id = newTransaction.Id }, newTransaction);
        }
    }
}
