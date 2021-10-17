using FlyyAirlines.Repository;
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

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Get(string id)
        {
            var GetUser = userService.Get(id);
            return View();
        }
        public IActionResult Edit(string id)
        {
            return RedirectToAction();
        }
        public IActionResult Delete(string id)
        {
            return RedirectToAction();
        }
    }
}
