using FluentNHibernate.Mapping;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Domain.Mapping
{
    public class UserRoleMap : ClassMap<UserRole>
    {
        public UserRoleMap()
        {
            Table("[UserRole]");
            Id(x => x.Id).GeneratedBy.Identity().Column("id");
            References(x => x.User).Column("[user_id]");
            References(x => x.Role).Column("role_id");
            Map(x => x.CreatedAt).Column("created_at");
            Map(x => x.ModifiedAt).Column("modified_at");
        }
    }
}