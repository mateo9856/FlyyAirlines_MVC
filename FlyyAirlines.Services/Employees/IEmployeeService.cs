using FlyyAirlines.Data;
using System.Threading.Tasks;

namespace FlyyAirlines.Repository
{
    public interface IEmployeeService
    {
        Task<bool> CheckReservation(Reservation reservation, User user);

    }
}
