using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines.Services.Account;
using FlyyAirlines_MVC.Models.FormModels;
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

        public IActionResult Get(string id)
        {
            var GetUser = userService.Get(id);
            return View(GetUser);
        }

        public IActionResult EditView(string id)
        {
            if(id == null)
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

            if (IsUserExist != null)
            {
                return RedirectToAction("NotFoundPage", "Home");
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
        public IActionResult Edit(string id, UserFormModel model)
        {
            var GetUser = userService.Get(id);

            if(GetUser == null)
            {
                return RedirectToAction("Users", "Admin");
            }

            var MapToUser = mapper.Map(model, GetUser);

            userService.Update(MapToUser);

            return RedirectToAction("Users", "Admin");
        }
        public IActionResult Delete(string id)
        {
            userService.Delete(id);
            return RedirectToAction("Users", "Admin");
        }
    }
}
