using System;
using NHibernate;
using NHibernate.Criterion;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;

namespace TravelWebApp.Repository.Criteria
{
    public class GetBookingByUserCriteria : ICriteriaSpecification<Booking>
    {
        private readonly User _user;
        public GetBookingByUserCriteria(User user)
        {
            _user = user;
        }
        public ICriteria Criteria(ISession session)
        {
            var criteria = session.CreateCriteria(typeof(Booking))
                                  .Add(Restrictions.Eq("User", _user));

            return criteria;

        }
    }
}