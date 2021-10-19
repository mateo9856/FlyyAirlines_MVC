using FlyyAirlines.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models.FormModels
{
    public class FlightFormModel
    {
        public string FromCountry { get; set; }
        public string FromCity { get; set; }
        public string ToCountry { get; set; }
        public string ToCity { get; set; }
        public string AirplaneId { get; set; }
        public DateTime? DepartureDate { get; set; }
        public List<Airplane> Airplanes { get; set; }
    }
}
