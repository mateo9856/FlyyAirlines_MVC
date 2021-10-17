using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult EmployeePanel()
        {
            return View();
        }
        public IActionResult Get(string id)
        {
            return View();
        }
        public IActionResult Edit()
        {
            return RedirectToAction();
        }
        public IActionResult Delete()
        {
            return RedirectToAction();
        }
    }
}
