using FlyyAirlines.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models
{
    public class HomeModel
    {
        public IEnumerable<Flight> Flights { get; set; }
        public bool IsSearched { get; set; }
        public IEnumerable<Flight> SearchedFlights { get; set; }
        public Flight BestSeller { get; set; }
        public int BestSellerCount { get; set; }
        public IEnumerable<News> News { get; set; }
    }
}
