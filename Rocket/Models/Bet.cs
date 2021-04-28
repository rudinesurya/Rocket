using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rocket.Models
{
    public class Bet
    {
        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        [ForeignKey("UserForeignKey")]
        public User User { get; set; }
        [ForeignKey("ContestForeignKey")]
        public Contest Contest { get; set; }
    }
}
