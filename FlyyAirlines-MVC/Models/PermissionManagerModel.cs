using FlyyAirlines.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models
{
    public class PermissionManagerModel
    {
        public List<Permission> Permissions { get; set; }
        public List<User> Users { get; set; }
        public string[] SelectedPermissions { get; set; }
        public string[] SelectedUsers { get; set; }
    }
}
