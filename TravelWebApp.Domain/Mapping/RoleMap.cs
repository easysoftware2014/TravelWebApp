using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Domain.Mapping
{
    public class RoleMap: ClassMap<Role>
    {
        public RoleMap()
        {
            Table("[Role]");

            Id(x => x.Id).GeneratedBy.Identity().Column("id");
            Map(x => x.RoleName).Column("name");
            Map(x => x.CreatedAt).Column("created_at");
            Map(x => x.ModifiedAt).Column("modified_at");

            HasManyToMany(x => x.UsersInRole)
                .Cascade.All()
                .Inverse()
                .Table("UserRole");
        }
    }
}