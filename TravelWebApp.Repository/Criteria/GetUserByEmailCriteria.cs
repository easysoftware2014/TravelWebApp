using NHibernate;
using NHibernate.Criterion;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;

namespace TravelWebApp.Repository.Criteria
{
    public class GetUserByEmailCriteria : ICriteriaSpecification<User>
    {
        private readonly string _email;

        public GetUserByEmailCriteria(string email)
        {
            _email = email;
        }

        public ICriteria Criteria(ISession session)
        {
            var criteria = session.CreateCriteria(typeof(User));
            criteria.Add(Restrictions.Eq("email", _email));

            return criteria;
        }
    }
}