using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket.Controllers.ViewModels;
using Rocket.Repositories;
using Microsoft.EntityFrameworkCore;
using Rocket.Models;

namespace Rocket.Controllers
{
    [Route("api/[controller]")]
    public class BetsController : Controller
    {
        private readonly RocketDbContext rocketDbContext;


        public BetsController(RocketDbContext dbContext)
        {
            rocketDbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Bet> Get()
        {
            return rocketDbContext.Bets.Include(x=>x.User).Include(x=>x.Contest).ToList();
        }

        [HttpPost]
        public ActionResult<Bet> Post([FromBody] BetViewModel bet)
        {
            var user = rocketDbContext.Users.Find(bet.UserId);
            var contest = rocketDbContext.Contests.Find(bet.ContestId);

            if (user == null || contest == null)
                throw new KeyNotFoundException();
            if (contest.IsOpen == false)
                throw new Exception("Contest is not open !");

            var duplicateExists = rocketDbContext.Bets.Where(x => x.UserId == bet.UserId && x.ContestId == bet.ContestId).SingleOrDefault();
            if (duplicateExists != null)
                throw new Exception("Duplicate found !");
            
            var newBet = new Bet
            {
                Id = bet.Id,
                User = user,
                Contest = contest,
                Amount = bet.Amount,
                Outcome = bet.Outcome,
                Timestamp = DateTimeOffset.Now
            };

            rocketDbContext.Bets.Add(newBet);
            var newTransaction = new Transaction
            {
                Type = 1,
                Amount = bet.Amount,
                Comment = $"Placed a bet of {bet.Amount}",
                UserId = user.Id
            };
            rocketDbContext.Transactions.Add(newTransaction);
            rocketDbContext.SaveChanges();

            return CreatedAtAction(nameof(Post), new { id = newBet.Id }, newBet);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var toDelete = rocketDbContext.Bets.Find(id);
            if (toDelete == null)
                return NotFound();
            if (toDelete.Contest.IsOpen == false)
                throw new AccessViolationException();

            var newTransaction = new Transaction
            {
                Type = 2,
                Amount = toDelete.Amount,
                Comment = $"Removed a bet of {toDelete.Amount}",
                UserId = toDelete.UserId
            };
            rocketDbContext.Transactions.Add(newTransaction);

            rocketDbContext.Bets.Remove(toDelete);
            rocketDbContext.SaveChanges();
            return NoContent();
        }
    }
}
