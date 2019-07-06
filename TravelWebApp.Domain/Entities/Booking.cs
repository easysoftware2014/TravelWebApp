using System;

namespace TravelWebApp.Domain.Entities
{
    public class Booking : Entity
    {
        public virtual DateTime CheckInDate { get; set; }
        public virtual DateTime CheckOut { get; set; }
        public virtual int NoOfChildren { get; set; }
        public virtual string Destination { get; set; }
        public virtual string PlaceOfOrigin { get; set; }
        public virtual int Quantity { get; set; }
        public virtual bool IsReturn { get; set; }
        public virtual int NoOfDays { get; set; }
        public virtual CabinClass CabinClass { get; set; }
        public virtual BookingType BookingType { get; set; }
    }
}