using System;
using NHibernate;
using NHibernate.Criterion;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;

namespace TravelWebApp.Repository.Criteria
{
    public class GetBookingsByBookingType : ICriteriaSpecification<Booking>
    {
        private readonly BookingType _bookingType;
        public GetBookingsByBookingType(BookingType bookingType)
        {
            _bookingType = bookingType;
        }
        public ICriteria Criteria(ISession session)
        {
            var criteria = session.CreateCriteria(typeof(Booking))
                                  .Add(Restrictions.Eq("BookingType", _bookingType));

            return criteria;

        }
    }
}