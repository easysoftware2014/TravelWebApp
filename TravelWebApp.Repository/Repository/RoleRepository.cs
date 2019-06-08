using NHibernate;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;

namespace TravelWebApp.Repository.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public ISession Session { get; set; }

        public RoleRepository()
        {
            Session = new NhibernateHelper().OpenSession();
        }
    }
}