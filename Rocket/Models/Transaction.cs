using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rocket.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int Type { get; set; }
        public double Amount { get; set; }
        public string Comment { get; set; }

        [ForeignKey("UserForeignKey")]
        public User User { get; set; }
    }
}
