using System;
using TravelWebApp.Domain.Contracts;

namespace TravelWebApp.Domain.Entities
{
    public class Entity : IEntity
    {
        public virtual long Id { get; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime ModifiedAt { get; set; }
    }
}