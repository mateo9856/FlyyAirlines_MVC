using FlyyAirlines.Data;
using FlyyAirlines_MVC.Models.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models
{
    public class ReservationViewModel
    {
        public List<Reservation> Reservations { get; set; }
        public int ReservationCount { get; set; }
        public ReservationFormModel FormModel { get; set; }
    }
}
