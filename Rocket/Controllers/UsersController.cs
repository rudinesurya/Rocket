using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket.Controllers.ViewModels;
using Rocket.Repositories;

namespace Rocket.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly RocketDbContext rocketDbContext;


        public UsersController(RocketDbContext dbContext)
        {
            rocketDbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Models.User> Get()
        {
            return rocketDbContext.Users.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Models.User> Get(int id)
        {
            return rocketDbContext.Users.Find(id);
        }

        [HttpPost]
        public ActionResult<Models.User> Post([FromBody] UserViewModel user)
        {
            var newUser = new Models.User { Id=user.Id, Name = user.Name };
            rocketDbContext.Users.Add(newUser);
            rocketDbContext.SaveChanges();
            
            return CreatedAtAction(nameof(Post), new { id = newUser.Id }, newUser);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var toDelete = rocketDbContext.Users.Find(id);
            if (toDelete == null)
                return NotFound();

            rocketDbContext.Users.Remove(toDelete);
            rocketDbContext.SaveChanges();
            return NoContent();
        }
    }
}
