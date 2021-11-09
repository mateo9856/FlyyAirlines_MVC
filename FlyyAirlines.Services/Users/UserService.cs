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
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public User GetByClaim(ClaimsPrincipal claim)
        {
            var GetEmailClaim = claim.Claims.FirstOrDefault(d => d.Type == "Email").Value;
            return _dbContext.Users.Include(d => d.Permissions).SingleOrDefault(d => d.Email == GetEmailClaim);
        }

        public async Task Delete(string id)
        {
            var GetData = this.Get(id);
            _dbContext.Remove(GetData);
            await _dbContext.SaveChangesAsync();
        }

        public User Get(string id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.Include(d => d.Reservations).ToList();
        }

        public IOrderedQueryable<User> GetList()
        {
            return _dbContext.Users.AsNoTracking().OrderBy(s => s.Id);
        }

        public void Update(User entity)
        {
            _dbContext.Users.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
