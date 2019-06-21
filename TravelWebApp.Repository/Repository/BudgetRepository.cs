using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NHibernate;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;
using TravelWebApp.Repository.Criteria;

namespace TravelWebApp.Repository.Repository
{
    public class BudgetRepository : Repository<Budget>, IBudgetRepository
    {
        public ISession Session { get; set; }

        public BudgetRepository()
        {
            var nhibernateHelper = new NhibernateHelper();
            Session = nhibernateHelper.OpenSession();
        }
        public Budget GetBudgetByUserId(long userId)
        {
            return FindBySpecification(new GetBudgetByUserIdCriteria(userId)).SingleOrDefault();
        }
        
    }
}