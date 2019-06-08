using System.Collections.Generic;

namespace TravelWebApp.Service.Contracts
{
    public interface IRepositoryService <T>
    {
        long Save(T entity);
        void SaveOrUpdate(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(long id);
        IList<T> GetAll();
    }
}