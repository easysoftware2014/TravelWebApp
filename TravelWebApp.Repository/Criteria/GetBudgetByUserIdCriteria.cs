using NHibernate;
using NHibernate.Criterion;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;

namespace TravelWebApp.Repository.Criteria
{
    public class GetBudgetByUserIdCriteria : ICriteriaSpecification<Budget>
    {
        private readonly long _id;
        public GetBudgetByUserIdCriteria(long id)
        {
            _id = id;
        }
        public ICriteria Criteria(ISession session)
        {
            var criteria = session.CreateCriteria(typeof(Budget));
            criteria.Add(Restrictions.Eq("Id", _id));

            return criteria;
        }
    }
}