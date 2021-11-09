using FlyyAirlines.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models.StaticModels
{
    public static class Authorization
    {
        public static bool Can(string Permission, User user)
        {
            if(user.Permissions == null)
            {
                return false;
            }
            return user.Permissions.Any(d => d.Name == Permission);
        }

    }
}
