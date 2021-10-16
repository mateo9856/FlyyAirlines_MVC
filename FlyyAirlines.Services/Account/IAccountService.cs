using FlyyAirlines.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyyAirlines.Services.Account
{
    public interface IAccountService
    {
        Task<bool> RegisterUser(RegisterModel registerModel, string role);
        Task<bool> LoginUser(LoginModel loginModel);
        Task LogoutUser();
    }

}
