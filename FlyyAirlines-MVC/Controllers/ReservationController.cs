using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines_MVC.Models;
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
        private readonly IReserveService reserveService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public ReservationController(IBaseService<Reservation> _reservation, IBaseService<Flight> _flights, IMapper _mapper
            , UserManager<User> _userService, IReserveService _reserveService)
        {
            reservation = _reservation;
            flights = _flights;
            mapper = _mapper;
            userManager = _userService;
            reserveService = _reserveService;
        }

        public async Task<IActionResult> MyReservations()
        {
                var GetUser = await userManager.GetUserAsync(User);
                if(GetUser == null)
                {
                    return RedirectToAction("NotFoundPage", "Home");
                }
                var GetUserReservations = await reserveService.GetReservationsFromUser(GetUser);

                if(GetUserReservations == null)
                {
                    return View(new ReservationViewModel()
                    {
                        Reservations = null,
                        ReservationCount = 0,
                        FormModel = new ReservationFormModel()
                    });
                }

                var ViewModel = new ReservationViewModel()
                {
                    Reservations = GetUserReservations.ToList(),
                    ReservationCount = GetUserReservations.Count(),
                    FormModel = new ReservationFormModel()
                };
                ViewModel.FormModel.FlightsList = flights.GetAll().ToList();
                return View(ViewModel);
        }

        public async Task<IActionResult> ReserveByFlightId(string id)
        {//to change
            var GetUser = await userManager.GetUserAsync(User);

            if(GetUser == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            var GetByFlightId = await reserveService.GetByFlightId(id);

            if(GetByFlightId == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            
            var MapToForm = mapper.Map<ReservationFormModel>(GetByFlightId);
            MapToForm.FlightsList = new List<Flight>()
            {
                GetByFlightId.Flights
            };

            return View("Reserve", MapToForm);
        }

        public async Task<IActionResult> EditView(string id)
        {
            var GetFlights = flights.GetAll();

            if(id == null)
            {
                return View(new ReservationFormModel()
                {
                    FlightsList = GetFlights.ToList()
                });
            }

            var GetReservation = await reservation.Get(id);

            var MapReservation = mapper.Map<ReservationFormModel>(GetReservation);
            MapReservation.FlightsList = GetFlights.ToList();

            return View(MapReservation);
        }

        public async Task<IActionResult> Get(string id)
        {
            var child = new string[] { "Flights" };
            var GetReservation = await reservation.EntityWithEagerLoad(d => d.Id == id, child);
            return View(GetReservation.FirstOrDefault());
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
