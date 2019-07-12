using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Contracts;
using TravelWebApp.Repository.Criteria;

namespace TravelWebApp.Repository.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public ISession Session { get; set; }

        public BookingRepository()
        {
            var nhibernateHelper = new NhibernateHelper();
            Session = nhibernateHelper.OpenSession();
        }
        public IList<Booking> GetBookingByDate(DateTime date)
        {
            return FindBySpecification(new GetBookingByDateCriteria(date)).ToList();
        }

        public IList<Booking> GetBookingByUser(User user)
        {
            return FindBySpecification(new GetBookingByUserCriteria(user)).ToList();
        }

        public IList<Booking> GetBookingByUserAndBookingType(User user, BookingType type)
        {
            return FindBySpecification(new GetBookingByUserAndBookingType(user, type)).ToList();
        }

        public IList<Booking> GetBookingsByBookingType(BookingType type)
        {
            return FindBySpecification(new GetBookingsByBookingType(type)).ToList();
        }

        public IList<Booking> GetBookingListByDate(User user, DateTime date)
        {
            return FindBySpecification(new GetBookingByUserDateCriteria(user, date)).ToList();
        }
    }
}