using FlyyAirlines.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models.FormModels
{
    public class ReservationFormModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long? PersonIdentify { get; set; }
        public int? Seat { get; set; }
        public string FlightId { get; set; }
        public string UserId { get; set; }
        public List<Flight> FlightsList { get; set; }
    }
}
