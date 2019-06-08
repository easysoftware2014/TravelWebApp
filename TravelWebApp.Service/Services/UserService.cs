using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;
using TravelWebApp.Repository.Repository;
using TravelWebApp.Service.Contracts;

namespace TravelWebApp.Service.Services
{
    public class UserService : RepositoryService<User>, IUserService
    {
        private readonly IUserRepository _repository;
        public UserService()
        {
            _repository = new UserRepository();
        }
        public User GetUserByCredentials(string email, string password)
        {
            return _repository.GetUserByEmailAndPassword(email, password);
        }
    }
}