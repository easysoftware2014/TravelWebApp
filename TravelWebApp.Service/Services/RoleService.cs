using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;
using TravelWebApp.Repository.Repository;
using TravelWebApp.Service.Contracts;

namespace TravelWebApp.Service.Services
{
    public class RoleService : RepositoryService<Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService()
        {
            _roleRepository = new RoleRepository();
        }

    }
}