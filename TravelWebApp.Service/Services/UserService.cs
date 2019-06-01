using TravelAgencyApp.Service.Contracts;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Service.Contracts;

namespace TravelWebApp.Service.Services
{
    public class UserService : RepositoryService<User>, IUserService
    {
        
    }
}