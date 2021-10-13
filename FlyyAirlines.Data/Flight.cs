using System;
using System.Collections.Generic;

#nullable disable

namespace FlyyAirlines.Data
{
    public class Flight : BaseEntity
    {
        public Flight()
        {
            Reservations = new HashSet<Reservation>();
        }
        public string FlightName { get; set; }
        public string FromCountry { get; set; }
        public string FromCity { get; set; }
        public string ToCountry { get; set; }
        public string ToCity { get; set; }
        public DateTime DepartureDate { get; set; }
        public virtual Airplane Airplane { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
