using NHibernate;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Repository.Contracts
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        User GetUserByEmailAndPassword(string username, string password);
        ISession Session { get; set; }
    }
}