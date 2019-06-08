using System.Collections.Generic;

namespace TravelWebApp.Domain.Entities
{
    public class Role : Entity
    {
        public virtual string RoleName { get; set; }
        public virtual IList<User> UsersInRole { get; set; }

        public Role()
        {
            UsersInRole = new List<User>();
        }
    }
}