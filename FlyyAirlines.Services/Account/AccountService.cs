using FlyyAirlines.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FlyyAirlines.Services.Account
{
    public class AccountService : IAccountService
    {
        protected readonly UserManager<User> userManager;
        protected readonly SignInManager<User> signInManager;
        public AccountService(UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;

        }

        public async Task<bool> LoginUser(LoginModel loginModel)
        {
            var checkUser = await userManager.FindByNameAsync(loginModel.UserName);

            await userManager.AddClaimsAsync(checkUser, new Claim[]
            {
                    new Claim("Role", checkUser.Role),
                    new Claim("Email", checkUser.Email)
            });

            if (checkUser == null)
            {
                return false;
            }

            var result = await signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, loginModel.RememberMe, false);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task LogoutUser()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterUser(RegisterModel registerModel, string role)
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerModel.Email,
                UserName = registerModel.UserName,
                Password = registerModel.Password,
                Name = registerModel.Name,
                Surname = registerModel.Surname,
                Role = role
            };

            var RegisterResult = await userManager.CreateAsync(newUser, newUser.Password);

            return RegisterResult.Succeeded;
        }
    }
}
