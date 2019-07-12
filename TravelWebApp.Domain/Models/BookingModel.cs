using System;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Domain.Models
{
    public class BookingModel
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOut { get; set; }
        public int NoOfChildren { get; set; }
        public string Destination { get; set; }
        public string PlaceOfOrigin { get; set; }
        public int Quantity { get; set; }
        public bool IsReturn { get; set; }
        public int NoOfDays { get; set; }
        public CabinClass CabinClass { get; set; }
        public BookingType BookingType { get; set; }

        public BookingModel()
        {}

        public BookingModel(Booking entity)
        {
            CheckInDate = entity.CheckInDate;
            CheckOut = entity.CheckInDate;
            NoOfChildren = entity.NoOfChildren;
            Destination = entity.Destination;
            PlaceOfOrigin = entity.PlaceOfOrigin;
            Quantity = entity.Quantity;
            IsReturn = entity.IsReturn;
            NoOfDays = entity.NoOfDays;
            CabinClass = entity.CabinClass;
            BookingType = entity.BookingType;
        }

        
    }
}