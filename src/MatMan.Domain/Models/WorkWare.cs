using System;

namespace MatMan.Domain.Models
{
    public class WorkWare : Entity
    {
        public Guid WorkID { get; init; }

        public Guid WareID { get; init; }

        public Guid PerimeterTypeID { get; init; }

        public Work Work { get; init; }

        public Ware Ware { get; init; }

        public PerimeterType PerimeterType { get; init; }

        public int WaresAmount { get; init; }
    }
}
