using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines.Services.Account;
using FlyyAirlines_MVC.Models.FormModels;
using FlyyAirlines_MVC.Models.StaticModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IAccountService accountService;
        private UserManager<User> userManager;
        private readonly IMapper mapper;

        public UserController(IUserService _userService, IMapper _mapper, UserManager<User> _userManager, IAccountService _accountService)
        {
            userService = _userService;
            mapper = _mapper;
            userManager = _userManager;
            accountService = _accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Get(string id)
        {
            var GetUser = userService.Get(id);

            var GetUserAccount = await userManager.GetUserAsync(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }
            return View(GetUser);
        }

        public async Task<IActionResult> EditView(string id)
        {
            var GetUserAccount = await userManager.GetUserAsync(User);

            if (!Authorization.Can("ADMIN", GetUserAccount))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            if (id == null)
            {
                return View(new UserFormModel());
            }

            var GetUser = userService.Get(id);

            var UserModel = mapper.Map<UserFormModel>(GetUser);

            return View(UserModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserFormModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            var MapToRegister = mapper.Map<RegisterModel>(model);

            var IsUserExist = await userManager.FindByEmailAsync(MapToRegister.Email);

            var GetUser = await userManager.GetUserAsync(User);

            if (!Authorization.Can("ADMIN", GetUser) || !Authorization.Can("SUPERADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            if (IsUserExist != null)
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Not found" });
            }

            if(ModelState.IsValid)
            {
                dynamic RegisterUser = null;
                switch (model.Role)
                {
                    case "User":
                        RegisterUser = await accountService.RegisterUser(MapToRegister, Roles.User);
                        break;
                    case "Employee":
                        RegisterUser = await accountService.RegisterUser(MapToRegister, Roles.Employee);
                        break;
                    case "Admin":
                        if (!Authorization.Can("SUPERADMIN", GetUser))
                        {
                            return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
                        }
                        RegisterUser = await accountService.RegisterUser(MapToRegister, Roles.Admin);
                        break;

                    default:
                        return RedirectToAction("Users", "Admin");
                }

                if(!RegisterUser)
                {
                    return NotFound();
                }

            }
            return RedirectToAction("Users", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserFormModel model)
        {
            var GetUser = userService.Get(id);

            var GetUserAccount = await userManager.GetUserAsync(User);

            if (!Authorization.Can("ADMIN", GetUserAccount))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            if (GetUser == null)
            {
                return RedirectToAction("Users", "Admin");
            }

            var MapToUser = mapper.Map(model, GetUser);

            userService.Update(MapToUser);

            return RedirectToAction("Users", "Admin");
        }
        public async Task<IActionResult> Delete(string id)
        {
            var GetUser = await userManager.GetUserAsync(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            await userService.Delete(id);
            return RedirectToAction("Users", "Admin");
        }
    }
}
