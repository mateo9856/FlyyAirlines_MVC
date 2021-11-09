using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class EmployeeOperations : Controller
    {
        public IActionResult Support()
        {
            return View();
        }

        public IActionResult CheckReservation()
        {
            return View();
        }
    }
}
