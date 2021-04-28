using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rocket.Models
{
    public class Contest
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsOpen { get; set; }
        public double TotalBetAmount { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
