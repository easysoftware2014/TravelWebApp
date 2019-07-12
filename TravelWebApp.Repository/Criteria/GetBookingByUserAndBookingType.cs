using System;
using NHibernate;
using NHibernate.Criterion;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;

namespace TravelWebApp.Repository.Criteria
{
    public class GetBookingByUserAndBookingType : ICriteriaSpecification<Booking>
    {
        private readonly User _user;
        private readonly BookingType _bookingType;
        public GetBookingByUserAndBookingType(User user, BookingType bookingType)
        {
            _user = user;
            _bookingType = bookingType;
        }
        public ICriteria Criteria(ISession session)
        {
            var criteria = session.CreateCriteria(typeof(Booking))
                                  .Add(Restrictions.Eq("User", _user))
                                  .Add(Restrictions.Eq("BookingType", _bookingType));

            return criteria;

        }
    }
}