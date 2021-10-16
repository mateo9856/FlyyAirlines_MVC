using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Employees()
        {
            return View();
        }

        public IActionResult Reservations()
        {
            return View();
        }

        public IActionResult FlightsAirplanes()
        {
            return View();
        }

    }
}
