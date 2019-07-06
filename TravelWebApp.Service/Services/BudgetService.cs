using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;
using TravelWebApp.Repository.Repository;
using TravelWebApp.Service.Contracts;

namespace TravelWebApp.Service.Services
{
    public class BudgetService: RepositoryService<Budget>, IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        public BudgetService()
        {
            _budgetRepository = new BudgetRepository();
        }
        
        public Budget GetBudgetByUserId(User user)
        {
            return _budgetRepository.GetBudgetByUserId(user);
        }
        
    }
}