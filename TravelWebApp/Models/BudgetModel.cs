using System;

namespace TravelWebApp.Models
{
    public class BudgetModel
    {
        public decimal Amount { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}