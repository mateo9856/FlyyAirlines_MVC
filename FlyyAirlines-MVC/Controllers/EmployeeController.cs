using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines.Services.Account;
using FlyyAirlines_MVC.Models.FormModels;
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
        private readonly IAccountService accountService;
        private readonly IMapper mapper;
        public EmployeeController(IBaseService<Employee> _employee, IMapper _mapper, IAccountService _accountService)
        {
            employee = _employee;
            mapper = _mapper;
            accountService = _accountService;
        }

        public IActionResult EmployeePanel()
        {
            return View();
        }
        public IActionResult EditView(string id)
        {
            if(id == null)
            {
                return View();
            }

            var GetEmployee = employee.Get(id);

            var MapToModel = mapper.Map<EmployeeFormModel>(GetEmployee);

            return View(MapToModel);
        }

        public async Task<IActionResult> Create(EmployeeFormModel model)
        {
            if(ModelState.IsValid)
            {
                var MapToEmployee = mapper.Map<Employee>(model);
                MapToEmployee.Id = Guid.NewGuid().ToString();

                if (model.IsUser.HasValue == true)
                {
                    var RegisterEmployee = await accountService.RegisterUser(model.Register, Roles.Employee);
                    if(!RegisterEmployee)
                    {
                        return RedirectToAction("Employees", "Admin");
                    }
                }
                await employee.Add(MapToEmployee);
            }

            return RedirectToAction("Employees", "Admin");
        }

        public async Task<IActionResult> Edit(string id, EmployeeFormModel model)
        {
            var GetEmployee = await employee.Get(id);
            if(GetEmployee == null)
            {
                return RedirectToAction("Employees", "Admin");
            }

            var MapToEmployee = mapper.Map(model, GetEmployee);

            employee.Update(MapToEmployee);

            return RedirectToAction("Employees", "Admin");
        }
        public async Task<IActionResult> Delete(string id)
        {
            var GetEmployee = await employee.Get(id);
            await employee.Delete(GetEmployee);
            return RedirectToAction("Employees", "Admin");
        }
    }
}
