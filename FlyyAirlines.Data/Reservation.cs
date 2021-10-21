#nullable disable

namespace FlyyAirlines.Data
{
    public class Reservation : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long? PersonIdentify { get; set; }
        public int? Seat { get; set; }

        public virtual Flight Flights { get; set; }
        public virtual User User { get; set; }
    }
}
