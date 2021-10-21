using FlyyAirlines.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines.Repository
{
    public interface IAirplanesFlightsService
    {
        IOrderedQueryable<Flight> GetAllFlights();
        Task<bool> CheckReservesFromFlights(Reservation reservation, Flight flight);
        string CalculateFlightTime(string[] datas);
        int GetBestSellerFlightCount();
    }
}
