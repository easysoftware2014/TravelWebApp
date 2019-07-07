using FluentNHibernate.Mapping;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Domain.Mapping
{
    public class BookingMap : ClassMap<Booking>
    {
        public BookingMap()
        {
            Table("Booking");

            Id(x => x.Id).GeneratedBy.Identity().Column("id");
            HasMany(x => x.Bookings)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Fetch.Join()
                .KeyColumn("booking_id");

            Map(x => x.CheckInDate).Column("check_in_date");
            Map(x => x.CheckOut).Column("checkout_date");
            Map(x => x.BookingType).Column("booking_type").CustomType<BookingType>();
            Map(x => x.CabinClass).Column("cabin_class").CustomType<CabinClass>();
            Map(x => x.Destination).Column("destination");
            Map(x => x.IsReturn).Column("is_return");
            Map(x => x.NoOfChildren).Column("no_of_children");
            Map(x => x.NoOfDays).Column("no_of_days");
            Map(x => x.Quantity).Column("quantity");
            Map(x => x.PlaceOfOrigin).Column("place_of_origin");

            Map(x => x.CreatedAt).Column("created_at");
            Map(x => x.ModifiedAt).Column("modified_at");
        }
    }
}