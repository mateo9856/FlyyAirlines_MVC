using FlyyAirlines.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models
{
    public class EmployeeViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string WorkPosition { get; set; }
        public string EmployeeOperation { get; set; }
        public List<Permission> EmployeePermissions { get; set; }
    }
}
