using FlyyAirlines.Data;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlyyAirlines.Repository
{
    public interface IReserveService
    {
        Task<IEnumerable<Reservation>> GetReservationsFromUser(User user);
        Task<Reservation> GetByFlightId(string id);
    }
}
