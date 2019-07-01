using System;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Models
{
    public class BudgetModel
    {
        public long Id { get; set; }    
        public decimal Amount { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string CreatedAt { get; set; }
        public BudgetModel()
        {}

        public BudgetModel(Budget budget)
        {
            Amount = budget.Amount;
            CreatedAt = budget.CreatedAt.ToShortDateString();
            Id = budget.Id;
            //ValidFrom = budget.ValidFrom;
            //ValidTo = budget.ValidTo;
        }
    }
}