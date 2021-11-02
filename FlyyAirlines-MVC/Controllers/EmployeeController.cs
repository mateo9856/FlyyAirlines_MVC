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
    public class EmployeeController : Controller
    {
        private readonly IBaseService<Employee> employee;
        private readonly IAccountService accountService;
        private readonly IMapper mapper;
        private readonly IUserService users;
        private readonly UserManager<User> user;
        public EmployeeController(IBaseService<Employee> _employee, IMapper _mapper, IAccountService _accountService, IUserService Users, UserManager<User> userManager)
        {
            employee = _employee;
            mapper = _mapper;
            accountService = _accountService;
            users = Users;
            user = userManager;
        }

        public async Task<IActionResult> EmployeePanel()
        {
            var GetUser = await user.GetUserAsync(User);

            if (!Authorization.Can("EMPLOYEE", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            return View();
        }
        public async Task<IActionResult> EditView(string id)
        {
            var GetUser = await user.GetUserAsync(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            if (id == null)
            {
                return View(new EmployeeFormModel()
                {
                    IsUser = false
                });
            }

            var GetEmployee = await employee.Get(id);

            var MapToModel = mapper.Map<EmployeeFormModel>(GetEmployee);

            return View(MapToModel);
        }

        public async Task<IActionResult> Create(EmployeeFormModel model)
        {
            var GetUser = await user.GetUserAsync(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            if (ModelState.IsValid)
            {
                var MapToEmployee = mapper.Map<Employee>(model);
                MapToEmployee.Id = Guid.NewGuid().ToString();

                if (model.IsUser)
                {
                    model.Register.Name = model.Name;
                    model.Register.Surname = model.Surname;
                    model.Register.Id = Guid.NewGuid().ToString();
                    var RegisterEmployee = await accountService.RegisterUser(model.Register, Roles.Employee);
                    if(!RegisterEmployee)
                    {
                        return RedirectToAction("Employees", "Admin");
                    }
                    MapToEmployee.User = users.Get(model.Register.Id);
                }
                await employee.Add(MapToEmployee);
            }

            return RedirectToAction("Employees", "Admin");
        }

        public async Task<IActionResult> Get(string id)
        {
            var GetEmployee = await employee.Get(id);
            return View(GetEmployee);
        }

        public async Task<IActionResult> Edit(string id, EmployeeFormModel model)
        {
            var GetEmployee = await employee.Get(id);
            if(GetEmployee == null)
            {
                return RedirectToAction("Employees", "Admin");
            }

            var GetUser = await user.GetUserAsync(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            var MapToEmployee = mapper.Map(model, GetEmployee);

            employee.Update(MapToEmployee);

            return RedirectToAction("Employees", "Admin");
        }
        public async Task<IActionResult> Delete(string id)
        {
            var GetEmployee = await employee.Get(id);

            var GetUser = await user.GetUserAsync(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            await employee.Delete(GetEmployee);
            return RedirectToAction("Employees", "Admin");
        }
    }
}
