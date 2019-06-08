using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Service.Contracts
{
    public interface IUserService : IRepositoryService<User>
    {
        User GetUserByCredentials(string email, string password);
    }
}