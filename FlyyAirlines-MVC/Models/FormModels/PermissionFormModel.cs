using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models.FormModels
{
    public class PermissionFormModel
    {
        public long? Id { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
    }
}
