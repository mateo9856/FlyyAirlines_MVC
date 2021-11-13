using FlyyAirlines.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models.EmployeeActions
{
    public class CheckReserveModel
    {
        public long? PersonIdentify { get;set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ReservationId { get; set; }
        public int? Seat { get; set; }
        public string FlightName { get; set; }
        public string AirplaneName { get; set; }
        public bool? IsChecked { get; set; }
    }
}
