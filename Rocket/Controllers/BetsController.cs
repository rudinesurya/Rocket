using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket.Controllers.ViewModels;
using Rocket.Repositories;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<Models.Bet> Get()
        {
            return rocketDbContext.Bets.Include(x=>x.User).Include(x=>x.Contest).ToList();
        }

        [HttpPost]
        public ActionResult<Models.Bet> Post([FromBody] BetViewModel bet)
        {
            var user = rocketDbContext.Users.Find(bet.UserId);
            var contest = rocketDbContext.Contests.Find(bet.ContestId);
            var newBet = new Models.Bet {
                Id = bet.Id,
                User = user,
                Contest = contest,
                Amount = bet.Amount,
                Timestamp = DateTimeOffset.Now
            };

            rocketDbContext.Bets.Add(newBet);
            rocketDbContext.SaveChanges();

            return CreatedAtAction(nameof(Post), new { id = newBet.Id }, newBet);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var toDelete = rocketDbContext.Bets.Find(id);
            if (toDelete == null)
                return NotFound();

            rocketDbContext.Bets.Remove(toDelete);
            rocketDbContext.SaveChanges();
            return NoContent();
        }
    }
}
