using Microsoft.AspNetCore.Identity;

namespace FlyyAirlines.Data
{
    public class Roles : IdentityRole
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string Employee = "Employee";
        public const string User = "User";
    }
}
