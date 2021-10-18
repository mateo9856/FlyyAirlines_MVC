using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IBaseService<Reservation> reservation;

        public ReservationController(IBaseService<Reservation> _reservation)
        {
            reservation = _reservation;
        }

        public IActionResult MyReservations()
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
