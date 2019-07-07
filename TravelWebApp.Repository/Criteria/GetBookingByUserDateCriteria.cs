using System;
using NHibernate;
using NHibernate.Criterion;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;

namespace TravelWebApp.Repository.Criteria
{
    public class GetBookingByUserDateCriteria : ICriteriaSpecification<Booking>
    {
        private readonly DateTime _date;
        private readonly User _user;
        public GetBookingByUserDateCriteria(User user, DateTime date)
        {
            _date = date;
            _user = user;
        }
        public ICriteria Criteria(ISession session)
        {
            var criteria = session.CreateCriteria(typeof(Booking));
            criteria.Add(Restrictions.Eq("CheckInDate", _date.Date))
                    .Add(Restrictions.Eq("User", _user));

            return criteria;

        }
    }
}