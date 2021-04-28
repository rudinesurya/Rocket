using System;
using System.ComponentModel.DataAnnotations;

namespace Rocket.Controllers.ViewModels
{
    public class BetViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ContestId { get; set; }
        public double Amount { get; set; }
    }
}
