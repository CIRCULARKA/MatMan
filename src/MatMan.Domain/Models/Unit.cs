namespace MatMan.Domain.Models
{
    public class Unit : Entity
    {
        public string FullName { get; init; }

        public string ShortName { get; init; }

        public double AttitudeToMeter { get; init; }
    }
}
