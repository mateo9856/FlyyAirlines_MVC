﻿using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines_MVC.Models.FormModels;
using Microsoft.AspNetCore.Identity;
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
        private readonly IBaseService<Flight> flights;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public ReservationController(IBaseService<Reservation> _reservation, IBaseService<Flight> _flights, IMapper _mapper, UserManager<User> _userService)
        {
            reservation = _reservation;
            flights = _flights;
            mapper = _mapper;
            userManager = _userService;
        }

        public IActionResult MyReservations()
        {
            return View();
        }

        public IActionResult ReserveByFlightId()
        {
            return View();
        }

        public async Task<IActionResult> EditView(string id)
        {
            var GetFlights = flights.GetAll();

            if(id == null)
            {
                return View(new ReservationFormModel()
                {
                    Flights = GetFlights.ToList()
                });
            }

            var GetReservation = await reservation.Get(id);

            var MapReservation = mapper.Map<ReservationFormModel>(GetReservation);
            MapReservation.Flights = GetFlights.ToList();

            return View(MapReservation);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationFormModel model)
        {
            if(ModelState.IsValid)
            {
                var MapReservation = mapper.Map<Reservation>(model);
                MapReservation.Id = Guid.NewGuid().ToString();
                MapReservation.Flights = await flights.Get(model.FlightId);
                MapReservation.User = await userManager.GetUserAsync(User);
                await reservation.Add(MapReservation);
            }
            return RedirectToAction("Reservations", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ReservationFormModel model)
        {
            var GetReserve = await reservation.Get(id);

            if (GetReserve == null)
            {
                return RedirectToAction("Reservations", "Admin");
            }

            var MapToUser = mapper.Map(model, GetReserve);
            MapToUser.Flights = await flights.Get(model.FlightId);
            MapToUser.User = await userManager.GetUserAsync(User);
            reservation.Update(MapToUser);

            return RedirectToAction("Reservations", "Admin");
        }
        public async Task<IActionResult> Delete(string id)
        {
            var GetReserve = await reservation.Get(id);
            await reservation.Delete(GetReserve);
            return RedirectToAction("Reservations", "Admin");
        }

    }
}
