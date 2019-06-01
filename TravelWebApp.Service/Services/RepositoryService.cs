using System.Collections.Generic;
using System.Linq;
using TravelAgencyApp.Service.Contracts;
using TravelWebApp.Repository.Contracts;
using TravelWebApp.Repository.Repository;

namespace TravelWebApp.Service.Services
{
    public class RepositoryService <T> : IRepositoryService<T> where T : class
    {
        private readonly IRepository<T> _eRepository;

        public RepositoryService()
        {
            _eRepository = new Repository<T>();
        }
        public long Save(T entity)
        {
            return _eRepository.AddEntity(entity);
        }

        public void Update(T entity)
        {
            _eRepository.Update(entity);
        }

        public void Delete(T entity)
        {
            _eRepository.Delete(entity);
        }

        public T Get(long id)
        {
            return _eRepository.Entity(id);
        }

        public IList<T> GetAll()
        {
            return _eRepository.GetList().ToList();
        }
    }
}