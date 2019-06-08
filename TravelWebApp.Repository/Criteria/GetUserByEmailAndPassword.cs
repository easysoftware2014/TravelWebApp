using NHibernate;
using NHibernate.Criterion;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;

namespace TravelWebApp.Repository.Criteria
{
    public class GetUserByEmailAndPasswordCriteria : ICriteriaSpecification<User>
    {
        private readonly string _email;
        private readonly string _password;

        public GetUserByEmailAndPasswordCriteria(string email, string password)
        {
            _email = email;
            _password = password;
        }
        public ICriteria Criteria(ISession session)
        {
            var criteria = session.CreateCriteria(typeof(User));
            criteria.Add(Restrictions.Eq("Email", _email));
            criteria.Add(Restrictions.Eq("Password", _password));

            return criteria;
        }
    }
}