namespace TravelWebApp.Domain.Entities
{
    public class UserBooking : Entity
    {
        public virtual User User { get; set; }
        public virtual Booking Booking{ get; set; }
    }
}