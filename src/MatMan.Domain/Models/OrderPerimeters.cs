using System;

namespace MatMan.Domain.Models
{
    public class OrderPerimeter : Entity
    {
        public Guid OrderID { get; init; }

        public Guid PerimeterTypeID { get; init; }

        public Order Order { get; init; }

        public PerimeterType PerimeterType { get; init; }

        public double? Perimeter { get; init; }
    }
}
