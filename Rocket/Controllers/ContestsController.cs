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
    public class ContestsController : Controller
    {
        private readonly RocketDbContext rocketDbContext;


        public ContestsController(RocketDbContext dbContext)
        {
            rocketDbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Models.Contest> Get()
        {
            return rocketDbContext.Contests.ToList();
        }

        [HttpPost]
        public ActionResult<Models.Contest> Post([FromBody] ContestViewModel contest)
        {
            var newContest = new Models.Contest { Id=contest.Id, Description=contest.Description };
            rocketDbContext.Contests.Add(newContest);
            rocketDbContext.SaveChanges();

            return CreatedAtAction(nameof(Post), new { id = newContest.Id }, newContest);
        }
    }
}
