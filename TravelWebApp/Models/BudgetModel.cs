using System;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Models
{
    public class BudgetModel
    {
        public decimal Amount { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public BudgetModel()
        {
            
        }

        public BudgetModel(Budget budget)
        {
            Amount = budget.Amount;
            ValidFrom = budget.ValidFrom;
            ValidTo = budget.ValidTo;
        }
    }
}