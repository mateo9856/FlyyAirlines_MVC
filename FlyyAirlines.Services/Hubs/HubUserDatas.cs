using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyyAirlines.Repository
{
    public class HubUserDatas
    {
        public string UserName { get; init; }
        public string Email { get; init; }
        public string ConnectionId { get; init; }
        public HubUserDatas(string name, string email, string connectionid)
        {
            this.UserName = name;
            this.Email = email;
            this.ConnectionId = connectionid;
        }
    }
}
