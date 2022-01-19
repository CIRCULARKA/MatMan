using System;

namespace MatMan.Domain.Models
{
    public class OrderWork : Entity
    {
        public Guid OrderID { get; init; }

        public Guid WorkID { get; init; }

        public Order Order { get; init; }

        public Work Work { get; init; }

        public double Perimeter { get; init; }
    }
}
