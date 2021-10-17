using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class FlightController : Controller
    {
        private readonly IAirplanesFlightsService airplanesFlightsService;
        private readonly IBaseService<Flight> flight;
        private readonly IBaseService<Airplane> airplane;

        public FlightController(IBaseService<Flight> _flight, IBaseService<Airplane> _airplane, IAirplanesFlightsService _airplaneFlightsService)
        {
            flight = _flight;
            airplane = _airplane;
            airplanesFlightsService = _airplaneFlightsService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetFlight(string id)
        {
            return View();
        }
        public IActionResult EditFlight(string id)
        {
            return RedirectToAction();
        }
        public IActionResult DeleteFlight(string id)
        {
            return RedirectToAction();
        }
        public IActionResult GetAirplane(string id)
        {
            return View();
        }
        public IActionResult EditAirplane(string id)
        {
            return RedirectToAction();
        }
        public IActionResult DeleteAirplane(string id)
        {
            return RedirectToAction();
        }
    }
}
