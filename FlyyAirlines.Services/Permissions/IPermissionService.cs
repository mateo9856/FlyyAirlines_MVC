using FlyyAirlines.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyyAirlines.Services.Permissions
{
    public interface IPermissionService
    {
        Task<Permission> GetById(string id);
        Task<IEnumerable<Permission>> GetAll();
        IOrderedQueryable<Permission> GetList();
        Task<bool> CheckPermission(User user, string PermissionName);
        Task AddPermissionToUser(User user, string PermissionName);
        Task DeleteUserPermission(User user, string PermissionName);
        Task<Permission> GetByPermissionName(string PermissionName);
        Task AddPermissionToTable(Permission permission);
        Task UpdatePermission(Permission permission);
        long PermissionsLength();
        Task DeletePermission(Permission permission);
    }
}
