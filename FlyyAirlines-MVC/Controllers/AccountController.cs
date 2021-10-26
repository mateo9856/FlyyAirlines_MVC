using FlyyAirlines.Data;
using FlyyAirlines.Services.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IAccountService accountService;

        public AccountController(UserManager<User> _userManager, IAccountService _accountService)
        {
            userManager = _userManager;
            accountService = _accountService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var Login = await accountService.LoginUser(model);
                if(!Login)
                {
                    return RedirectToAction("NotFoundPage", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await accountService.LogoutUser();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var IsUserExist = await userManager.FindByEmailAsync(model.Email);

            if(IsUserExist != null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            
            if(ModelState.IsValid)
            {
                var RegisterUser = await accountService.RegisterUser(model, Roles.User);
                if(!RegisterUser)
                {
                    return RedirectToAction("NotFoundPage", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
