using FlyyAirlines.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models.FormModels
{
    public class EmployeeFormModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string WorkPosition { get; set; }
        public bool IsUser { get; set; }
        public RegisterModel Register { get; set; }
    }
}
