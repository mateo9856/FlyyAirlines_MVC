using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models.FormModels
{
    public class UserFormModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string RetypePassword { get; set; }
        public string Role { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
