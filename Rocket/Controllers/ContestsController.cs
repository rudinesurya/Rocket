using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket.Controllers.ViewModels;
using Rocket.Models;
using Rocket.Repositories;

namespace Rocket.Controllers
{
    [Route("api/[controller]")]
    public class ContestsController : Controller
    {
        private readonly RocketDbContext rocketDbContext;


        public ContestsController(RocketDbContext dbContext)
        {
            rocketDbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Contest> Get()
        {
            return rocketDbContext.Contests.ToList();
        }

        [HttpPost]
        public ActionResult<Contest> Post([FromBody] ContestViewModel contest)
        {
            var newContest = new Contest { Id = contest.Id, Description = contest.Description, IsOpen = true };
            rocketDbContext.Contests.Add(newContest);
            rocketDbContext.SaveChanges();

            return CreatedAtAction(nameof(Post), new { id = newContest.Id }, newContest);
        }

        [HttpPut("{id}")]
        public ActionResult SetOutcome(int id, ContestOutcomeViewModel contestOutcome)
        {
            var toModify = rocketDbContext.Contests.Find(id);
            if (toModify == null)
                return NotFound();
            if (toModify.IsOpen == false)
                throw new Exception("Contest cannot be re-opened !");

            toModify.IsOpen = false;
            toModify.Outcome = contestOutcome.Outcome;

            var totalBets = rocketDbContext.Bets.Where(x => x.ContestId == id).Sum(x=>x.Amount);
            var totalWinningBets = rocketDbContext.Bets.Where(x => x.ContestId == id && x.Outcome == contestOutcome.Outcome).Sum(x => x.Amount);
            var winners = rocketDbContext.Bets.Where(x => x.ContestId == id && x.Outcome == contestOutcome.Outcome).ToList();

            winners.ForEach(x=> {
                var bounty = (x.Amount/ totalWinningBets)* totalBets;
                var newTransaction = new Transaction {
                    Type = 3,
                    Amount = bounty,
                    Comment = $"Congratulations on winning {bounty}",
                    UserId = x.UserId
                };
                rocketDbContext.Transactions.Add(newTransaction);
            });

            rocketDbContext.SaveChanges();
            return NoContent();
        }
    }
}
