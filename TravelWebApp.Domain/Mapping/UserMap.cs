using FluentNHibernate.Mapping;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Domain.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("User");
            Id(x => x.Id).GeneratedBy.Identity().Column("id");
            Map(x => x.Email).Column("email");
            Map(x => x.Password).Column("password");
            Map(x => x.Name).Column("name");
            Map(x => x.Surname).Column("surname");
            Map(x => x.ContactNumber).Column("contact_number");
            Map(x => x.CreatedAt).Column("createdAt");
            Map(x => x.ModifiedAt).Column("modifiedAt");

            HasOne(x => x.Budget).Cascade.All().PropertyRef("User");
        }
    }
}