using FluentNHibernate.Mapping;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Domain.Mapping
{
    public class BudgetMap : ClassMap<Budget>
    {
        public BudgetMap()
        {
            Table("Budget");
            Id(x => x.Id).GeneratedBy.Identity().Column("id");
            References(x => x.User).Column("user_id");
            Map(x => x.Amount).Column("amount");
            //Map(x => x.ValidFrom).Column("valid_from");
            //Map(x => x.ValidTo).Column("valid_to");
            Map(x => x.CreatedAt).Column("createdAt");
            Map(x => x.ModifiedAt).Column("modifiedAt");
        }
    }
}