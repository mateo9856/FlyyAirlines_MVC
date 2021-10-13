using FlyyAirlines.Data;
using FlyyAirlines.Database;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines.Repository
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _dbContext;
        public EmployeeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CheckReservation(Reservation reservation, User user)
        {
            var getUser = await _dbContext.Users.FindAsync(user);
            var checkReservation = getUser.Reservations.FirstOrDefault(res => res == reservation);
            if(checkReservation != null)
            {
                return true;
            }
            return false;
        }

    }
}
