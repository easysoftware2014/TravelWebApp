using System.Collections.Generic;

namespace TravelWebApp.Repository.Contracts
{
    public interface IRepository <T>
    {
        long AddEntity(T entity);
        void Delete(T entity);
        void Update(T entity);
        void SaveOrUpdate(T entity);
        IList<T> GetList();
        T Entity(long id);
        IList<T> FindBySpecification(ICriteriaSpecification<T> specification);

    }
}