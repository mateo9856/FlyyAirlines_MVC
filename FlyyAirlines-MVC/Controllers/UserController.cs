using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines_MVC.Models.FormModels;
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
        private readonly IMapper mapper;

        public UserController(IUserService _userService, IMapper _mapper)
        {
            userService = _userService;
            mapper = _mapper;
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
                return View();
            }

            var GetUser = userService.Get(id);

            var UserModel = mapper.Map<UserFormModel>(GetUser);

            return View(UserModel);
        }

        public IActionResult Create(UserFormModel model)
        {
            var MapToRegister = mapper.Map<RegisterModel>(model);

            switch(model.Role)
            {
                case "User":
                    return RedirectToAction("Register", "Account", MapToRegister);//try change to another class
                case "Employee":
                    return RedirectToAction("RegisterEmployee", "Account", MapToRegister);
                case "Admin":
                    return RedirectToAction("RegisterAdmin", "Account", MapToRegister);

                default:
                    return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Edit(string id)
        {
            return RedirectToAction();
        }
        public IActionResult Delete(string id)
        {
            userService.Delete(id);
            return RedirectToAction("Users", "Admin");
        }
    }
}
