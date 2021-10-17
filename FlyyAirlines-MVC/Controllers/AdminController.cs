using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class AdminController : Controller
    {//create services list z paginacją i operacjami crud
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            var GetUsers = UsersService.GetAll();
            return View();
        }

        public IActionResult Employees()
        {
            var GetEmployees = EmployeesService.GetAll();
            return View();
        }

        public IActionResult Reservations()
        {
            var GetReservations = ReservationsService.GetAll();
            return View();
        }

        public IActionResult FlightsAirplanes()
        {
            var GetAirplanes = AirplanesService.GetAll();
            var GetFlights = FlightsService.GetAll();
            return View();
        }

    }
}
