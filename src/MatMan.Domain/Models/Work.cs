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
            {
                if ((perimeter % 1) > 0.5)
                    return Math.Floor(perimeter) + 2;
                else
                    return Math.Floor(perimeter) + 1;
            }
            return perimeter;
        }
    }
}
