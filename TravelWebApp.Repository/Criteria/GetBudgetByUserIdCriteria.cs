using NHibernate;
using NHibernate.Criterion;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;

namespace TravelWebApp.Repository.Criteria
{
    public class GetBudgetByUserIdCriteria : ICriteriaSpecification<Budget>
    {
        private readonly User _user;
        public GetBudgetByUserIdCriteria(User user)
        {
            _user = user;
        }
        public ICriteria Criteria(ISession session)
        {
            var criteria = session.CreateCriteria(typeof(Budget));
            criteria.Add(Restrictions.Eq("User", _user));

            return criteria;
        }
    }
}