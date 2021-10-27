using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models
{
    public class ReservationSummaryModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long PersonIdentify { get; set; }
        public string FlightName { get; set; }
        public int Seat { get; set; }
    }
}
