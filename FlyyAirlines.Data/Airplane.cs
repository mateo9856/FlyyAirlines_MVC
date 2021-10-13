using FlyyAirlines.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FlyyAirlines.Data
{
    public class Airplane : BaseEntity
    {
        public string PlaneName { get; set; }
        public int? NumberOfSeats { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
