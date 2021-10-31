using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyyAirlines.Data
{
    public class UserPermission
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public long PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
