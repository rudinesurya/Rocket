using System;
using System.ComponentModel.DataAnnotations;

namespace Rocket.Models
{
    public class Bet
    {
        [Key]
        public int Id { get; set; }
        public int Outcome { get; set; }
        public double Amount { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int ContestId { get; set; }
        public Contest Contest { get; set; }
    }
}
