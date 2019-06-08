using NHibernate;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Repository.Contracts
{
    public interface IRoleRepository : IRepository<Role>
    {
        ISession Session { get; set; }
    }
}