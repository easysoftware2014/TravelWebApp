using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NHibernate;
using TravelWebApp.Repository.Contracts;

namespace TravelWebApp.Repository.Repository
{
    public class Repository <T> : IRepository<T> where T : class
    {
        private readonly ISession _session;
        public Repository()
        {
            _session = new NhibernateHelper().OpenSession();
        }
        public long AddEntity(T entity)
        {
            return Convert.ToInt32(_session.Save(entity));
        }

        public void Delete(T entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(entity);
                transaction.Commit();
            }

        }

        public void Update(T entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Update(entity);
                transaction.Commit();
            }

        }

        public IList<T> GetList()
        {
            return _session.Query<T>().ToList();
        }

        public T Entity(long id)
        {
            return _session.Get<T>(Convert.ToInt64(id));

        }
        public void SaveOrUpdate(T item)
        {
            _session.SaveOrUpdate(item);
        }
        public IList<T> FindBySpecification(ICriteriaSpecification<T> specification)
        {
            return specification.Criteria(_session).List<T>();
        }
    }
}