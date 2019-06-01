using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Domain.Contracts
{
    public interface IBudget
    {
        decimal Amount { get; set; }
        User User { get; set; }
    }
}