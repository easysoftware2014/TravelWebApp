using System.Collections.Generic;
using System.Configuration;
using NHibernate;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;

namespace TravelWebApp.Repository.Repository
{
    public class BudgetRepository : Repository<Budget>, IBudgetRepository
    {
        private readonly NhibernateHelper _nhibernateHelper;
        public ISession Session { get; set; }

        public BudgetRepository()
        {
            _nhibernateHelper = new NhibernateHelper();
            Session = _nhibernateHelper.OpenSession();
        }
        public Budget GetBudgetByUserId(long userId)
        {
            throw new System.NotImplementedException();
        }

        public IList<Budget> FindBySpecification(ICriteriaSpecification<Budget> specification)
        {
            throw new System.NotImplementedException();
        }
    }
}