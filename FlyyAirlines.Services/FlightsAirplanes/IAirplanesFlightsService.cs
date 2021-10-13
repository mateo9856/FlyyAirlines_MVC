using FlyyAirlines.Data;
using System.Threading.Tasks;

namespace FlyyAirlines.Repository
{
    public interface IAirplanesFlightsService
    {
        Task<bool> CheckReservesFromFlights(Reservation reservation, Flight flight);
        string CalculateFlightTime(string[] datas);
        int GetBestSellerFlightCount();
    }
}
