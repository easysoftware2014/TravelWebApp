using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Service.Contracts
{
    public interface IBudgetService : IRepositoryService<Budget>
    {
        Budget GetBudgetByUserId(User user);
    }
}