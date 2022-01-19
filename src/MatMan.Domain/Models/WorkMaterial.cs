using System;

namespace MatMan.Domain.Models
{
    public class WorkMaterial : Entity
    {
        public Guid WorkID { get; init; }

        public Guid MaterialID { get; init; }

        public Guid PerimeterTypeID { get; init; }

        public Work Work { get; init; }

        public Material Material { get; init; }

        public PerimeterType PerimeterType { get; init; }

        public int MaterialsAmount { get; init; }
    }
}
