using System;
using System.Collections.Generic;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Repository.Repository;
using TravelWebApp.Service.Contracts;

namespace TravelWebApp.Service.Services
{
    public class BookingService : RepositoryService<Booking>, IBookingService
    {
        private readonly BookingRepository _repository;
        public BookingService()
        {
            _repository = new BookingRepository();
            
        }
        public IList<Booking> GetBookingByDate(DateTime date)
        {
            return _repository.GetBookingByDate(date);
        }

        public IList<Booking> GetBookingByUser(User user)
        {
            return _repository.GetBookingByUser(user);
        }

        public IList<Booking> GetBookingByUserAndBookingType(User user, BookingType type)
        {
            return _repository.GetBookingByUserAndBookingType(user, type);
        }

        public IList<Booking> GetBookingsByBookingType(BookingType type)
        {
            return _repository.GetBookingsByBookingType(type);
        }

        public IList<Booking> GetBookingListByDate(User user, DateTime date)
        {
            return _repository.GetBookingListByDate(user, date);
        }
    }
}