using System.Linq;
using NHibernate;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;
using TravelWebApp.Repository.Criteria;

namespace TravelWebApp.Repository.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public ISession Session { get; set; }

        public UserRepository()
        {
            Session = new NhibernateHelper().OpenSession();
        }

        public User GetUserByEmail(string email)
        {
            return FindBySpecification(new GetUserByEmailCriteria(email)).Single();
        }

        public User GetUserByEmailAndPassword(string username, string password)
        {
            return FindBySpecification(new GetUserByEmailAndPasswordCriteria(username, password)).SingleOrDefault();
        }
    }
}