using NHibernate;

namespace TravelWebApp.Repository.Contracts
{
    public interface ICriteriaSpecification<T>
    {
        ICriteria Criteria(ISession session);
    }
}