using System;
using NHibernate;
using NHibernate.Criterion;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;

namespace TravelWebApp.Repository.Criteria
{
    public class GetBookingByDateCriteria : ICriteriaSpecification<Booking>
    {
        private readonly DateTime _date;
        public GetBookingByDateCriteria(DateTime date)
        {
            _date = date;
        }
        public ICriteria Criteria(ISession session)
        {
            var criteria = session.CreateCriteria(typeof(Booking));
            criteria.Add(Restrictions.Eq("CheckInDate", _date.Date));

            return criteria;

        }
    }
}