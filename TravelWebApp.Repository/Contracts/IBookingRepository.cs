using System;
using System.Collections.Generic;
using NHibernate;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Repository.Contracts
{
    public interface IBookingRepository : IRepository<Booking>
    {
        ISession Session { get; set; }
        IList<Booking> GetBookingByDate(DateTime date);
        IList<Booking> GetBookingByUser(User user);
        IList<Booking> GetBookingByUserAndBookingType(User user, BookingType type);
        IList<Booking> GetBookingsByBookingType(BookingType type);
        IList<Booking> GetBookingListByDate(User user, DateTime date);
    }
}