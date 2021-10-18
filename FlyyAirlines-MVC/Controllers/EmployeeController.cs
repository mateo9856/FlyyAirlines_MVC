using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IBaseService<Employee> employee;
        
        public EmployeeController(IBaseService<Employee> _employee)
        {
            employee = _employee;
        }

        public IActionResult EmployeePanel()
        {
            return View();
        }
        public IActionResult EditView(string id)
        {
            return View();
        }

        public IActionResult Create()
        {
            return RedirectToAction();
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
