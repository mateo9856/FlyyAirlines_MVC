using FlyyAirlines.Data;
using FlyyAirlines.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyyAirlines.Services.Permissions
{
    public class PermissionService : IPermissionService
    {
        private readonly AppDbContext dbContext;

        public PermissionService(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task AddPermissionToTable(Permission permission)
        {
            await dbContext.Permissions.AddAsync(permission);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddPermissionToUser(User user, string PermissionName)
        {
            var GetPermissionName = await GetByPermissionName(PermissionName);
            if(GetPermissionName == null)
            {
                return;
            }
            user.Permissions.Add(GetPermissionName);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckPermission(User user, string PermissionName)
        {
            var GetPermissionName = await GetByPermissionName(PermissionName);

            return user.Permissions.Contains(GetPermissionName);
        }

        public async Task DeletePermission(Permission permission)
        {
            dbContext.Permissions.Remove(permission);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserPermission(User user, string PermissionName)
        {
            var GetPermissionName = await GetByPermissionName(PermissionName);
            if (GetPermissionName == null)
            {
                return;
            }
            user.Permissions.Remove(GetPermissionName);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Permission>> GetAll()
        {
            return await dbContext.Permissions.ToListAsync();
        }

        public async Task<Permission> GetById(string id)
        {
            return await dbContext.Permissions.SingleOrDefaultAsync(d => d.Id == id);
        }

        public Task<Permission> GetByPermissionName(string PermissionName)
        {
            var GetPermission = dbContext.Permissions.SingleOrDefaultAsync(d => d.Name == PermissionName);
            if(GetPermission == null)
            {
                return null;
            }
            return GetPermission;
        }

        public IOrderedQueryable<Permission> GetList()
        {
            return dbContext.Permissions.AsNoTracking().OrderBy(d => d.Id);
        }

        public long PermissionsLength()
        {
            return dbContext.Permissions.Count();
        }

        public async Task UpdatePermission(Permission permission)
        {
            dbContext.Permissions.Update(permission);
            dbContext.Entry(permission).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
    }
}
