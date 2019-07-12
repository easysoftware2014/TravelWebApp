using NHibernate;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Repository.Contracts
{
    public interface IBudgetRepository : IRepository<Budget>
    {
        ISession Session { get; set; }
        Budget GetBudgetByUserId(User user);
    }
}