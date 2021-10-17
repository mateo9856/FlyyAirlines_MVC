using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class FlightController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetFlight(string id)
        {
            return View();
        }
        public IActionResult EditFlight()
        {
            return RedirectToAction();
        }
        public IActionResult DeleteFlight()
        {
            return RedirectToAction();
        }
        public IActionResult GetAirplane(string id)
        {
            return View();
        }
        public IActionResult EditAirplane()
        {
            return RedirectToAction();
        }
        public IActionResult DeleteAirplane()
        {
            return RedirectToAction();
        }
    }
}
