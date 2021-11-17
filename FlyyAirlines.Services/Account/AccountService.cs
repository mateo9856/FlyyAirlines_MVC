using FlyyAirlines.Data;
using FlyyAirlines.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly AppDbContext dbContext;
        public AccountService(UserManager<User> _userManager, SignInManager<User> _signInManager, AppDbContext _dbContext)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            dbContext = _dbContext;
        }

        public async Task<bool> LoginUser(LoginModel loginModel)
        {
            var checkUser = await userManager.FindByNameAsync(loginModel.UserName);
            
            if (checkUser == null)
            {
                return false;
            }

            var IncludePermissions = await dbContext.Users.Include(d => d.Permissions).FirstOrDefaultAsync(d => d == checkUser);

            await userManager.AddClaimsAsync(checkUser, new Claim[]
            {
                    new Claim("Role", checkUser.Role),
                    new Claim("Email", checkUser.Email),
                    new Claim("User", checkUser.UserName),
                    new Claim("IsSupport", IncludePermissions.Permissions.Any(d => d.Name == "ISSUPPORT").ToString())
            });

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
                Id = registerModel.Id,
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
