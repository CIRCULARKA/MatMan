using System;

namespace MatMan.Domain.Models
{
    public class OrderComponent<TComponent> : Entity
        where TComponent : Component
    {
        public Guid OrderID { get; init; }

        public Guid ComponentID { get; init; }

        public Order Order { get; init; }

        public TComponent Component { get; init; }

        public double ComponentAmount { get; init; }
    }
}
