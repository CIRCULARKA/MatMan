using System;

namespace MatMan.Domain.Models
{
    public class WareMaterial : Entity
    {
        public Guid WareID { get; init; }

        public Guid MaterialID { get; init; }

        public Ware Ware { get; init; }

        public Material Material { get; init; }

        public double MaterialsAmount { get; init; }
    }
}
