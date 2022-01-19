using System;

namespace MatMan.Domain.Models
{
    public class Work : NamedEntity
    {
        public bool IsApplicableToWholePerimeter { get; init; }

        public double ApplicableLength { get; init; }

        public double CalculatePerimeter(double perimeter)
        {
            if (Name == "Использование кантика")
                Math.Ceiling(perimeter % 1 <= 0.5 ? perimeter : perimeter + 1);
            return perimeter;
        }
    }
}
