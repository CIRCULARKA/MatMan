using System;

namespace MatMan.Domain.Models
{
    public class WorkRule : Entity
    {
        public Guid WorkID { get; init; }

        public Work Work { get; init; }

        public double AdditionalPerimeter { get; init; }
    }
}
