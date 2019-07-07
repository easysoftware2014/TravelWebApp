using System;
using System.Collections.Generic;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Service.Contracts
{
    public interface IBookingService : IRepositoryService<Booking>
    {
        IList<Booking> GetBookingByDate(DateTime date);
        IList<Booking> GetBookingByUser(User user);
        IList<Booking> GetBookingByUserAndBookingType(User user, BookingType type);
        IList<Booking> GetBookingsByBookingType(BookingType type);
        IList<Booking> GetBookingListByDate(User user, DateTime date);

    }
}