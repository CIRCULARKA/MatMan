using System;

namespace MatMan.Domain.Models
{
    public class Configuration : Entity
    {
        public Guid ComponentID { get; init; }

        public Guid UnitID { get; init; }

        public Unit Unit { get; init; }

        public Component Component { get; init; }
    }
}
