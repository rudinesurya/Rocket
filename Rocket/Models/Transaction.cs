using System;
using System.ComponentModel.DataAnnotations;

namespace Rocket.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int Type { get; set; }
        public double Amount { get; set; }
        public string Comment { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
