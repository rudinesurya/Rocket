using System;
using System.ComponentModel.DataAnnotations;

namespace Rocket.Controllers.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public double Amount { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
    }
}
