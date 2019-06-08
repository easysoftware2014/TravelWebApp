using System;
using System.ComponentModel.DataAnnotations;
using TravelWebApp.Domain.Contracts;

namespace TravelWebApp.Domain.Entities
{
    public class Budget : Entity, IBudget
    {
        [Required(ErrorMessage = "Amount required.")]
        public virtual decimal Amount { get; set; }
        public virtual DateTime ValidFrom { get; set; }
        public virtual DateTime ValidTo { get; set; }
        public virtual User User { get; set; }
    }
}