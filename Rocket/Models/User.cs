using System;
using System.ComponentModel.DataAnnotations;

namespace Rocket.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
