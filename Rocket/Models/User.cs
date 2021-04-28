using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rocket.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Bet> Bets { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
