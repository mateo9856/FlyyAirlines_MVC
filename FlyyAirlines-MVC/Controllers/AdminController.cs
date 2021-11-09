using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines_MVC.Models.ErrorDictionary;
using FlyyAirlines_MVC.Models.StaticModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService UsersService;
        private readonly IBaseService<Reservation> ReservationsService;
        private readonly IBaseService<Employee> EmployeesService;
        private readonly IBaseService<Flight> FlightsService;
        private readonly IBaseService<Airplane> AirplanesService;
        private readonly IAirplanesFlightsService AirplanesFlightsService;

        public AdminController(IUserService users, IBaseService<Reservation> reservations, IBaseService<Employee> employees,
             IBaseService<Flight> flights, IBaseService<Airplane> airplanes, IAirplanesFlightsService airplanesFlights)
        {
            UsersService = users;
            ReservationsService = reservations;
            EmployeesService = employees;
            FlightsService = flights;
            AirplanesService = airplanes;
            AirplanesFlightsService = airplanesFlights;
        }

        public async Task<IActionResult> Index()
        {
            var GetUser = UsersService.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            return View();
        }

        public async Task<IActionResult> Users(int page = 1)
        {
            var GetUsers = UsersService.GetList();
            var model = await PagingList.CreateAsync(GetUsers, 10, page);

            var GetUser = UsersService.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser)) {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }
            
            return View(model);
        }

        public async Task<IActionResult> Employees(int page = 1)
        {
            var GetEmployees = EmployeesService.GetList(new string[] {"User"});
            var model = await PagingList.CreateAsync(GetEmployees, 10, page);

            var GetUser = UsersService.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            return View(model);
        }

        public async Task<IActionResult> Reservations(int page = 1)
        {
            var GetReservations = ReservationsService.GetList(new string[] { "Flights", "User" });
            var model = await PagingList.CreateAsync(GetReservations, 10, page);

            var GetUser = UsersService.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            return View(model);
        }

        public async Task<IActionResult> Flights(int page = 1)
        {
            var GetFlights = FlightsService.GetList(new string[] {"Airplane"});
            var model = await PagingList.CreateAsync(GetFlights, 10, page);

            var GetUser = UsersService.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            return View(model);
        }

        public async Task<IActionResult> Airplanes(int page = 1)
        {
            var GetAirplanes = AirplanesService.GetList();
            var model = await PagingList.CreateAsync(GetAirplanes, 10, page);

            var GetUser = UsersService.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            return View(model);
        }

    }
}
