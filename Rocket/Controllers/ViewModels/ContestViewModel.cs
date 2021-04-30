using System;
using System.ComponentModel.DataAnnotations;

namespace Rocket.Controllers.ViewModels
{
    public class ContestViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class ContestOutcomeViewModel
    {
        public int Outcome { get; set; }
    }
}
