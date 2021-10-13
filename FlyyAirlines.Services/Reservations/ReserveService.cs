using FlyyAirlines.Data;
using FlyyAirlines.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyyAirlines.Repository
{
    public class ReserveService : IReserveService
    {
        private readonly AppDbContext _dbContext;
        public ReserveService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task <IEnumerable<Reservation>> GetReservationsFromUser(User user)
        {
            var getUser = await _dbContext.Users.FindAsync(user);
            return getUser.Reservations;
        }
    }
}
