using FluentNHibernate.Mapping;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Domain.Mapping
{
    public class BookingsMap : ClassMap<UserBooking>
    {
        public BookingsMap()
        {
            Table("UserBooking");
            Id(x => x.Id).GeneratedBy.Identity().Column("id");
            References(x => x.User)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("   ");
            References(x => x.Booking)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("booking_id");

            Map(x => x.CreatedAt).Column("created_at");
            Map(x => x.ModifiedAt).Column("modified_at");
        }
        
    }
}