using FlyyAirlines.Data;
using FlyyAirlines.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public async Task<Reservation> GetByFlightId(string id)
        {
            return await _dbContext.Reservations.Include(f => f.Flights).SingleOrDefaultAsync(d => d.Flights.Id == id);
        }

        public async Task <IEnumerable<Reservation>> GetReservationsFromUser(User user)
        {
            var getReservations = await _dbContext.Reservations.Include(u => u.User).Include(f => f.Flights).Where(d => d.User == user).ToListAsync();
            return getReservations;
        }
    }
}
