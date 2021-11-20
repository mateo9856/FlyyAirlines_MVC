using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines_MVC.Models;
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
    public class ReservationController : Controller
    {
        private readonly IBaseService<Reservation> reservation;
        private readonly IBaseService<Flight> flights;
        private readonly IReserveService reserveService;
        private readonly IMapper mapper;
        private readonly IUserService user;

        public ReservationController(IBaseService<Reservation> _reservation, IBaseService<Flight> _flights, IMapper _mapper
            , IUserService _userService, IReserveService _reserveService)
        {
            reservation = _reservation;
            flights = _flights;
            mapper = _mapper;
            user = _userService;
            reserveService = _reserveService;
        }

        public async Task<IActionResult> MyReservations()
        {
            var GetUser = await user.GetByClaim(User);
                if(GetUser == null)
                {
                    return RedirectToAction("Error", "Home", new { ErrorName = "Not found" });
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
        {
            var GetUser = await user.GetByClaim(User);

            if (GetUser == null)
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Not found" });
            }

            var GetByFlightId = await flights.EntityWithEagerLoad(d => d.Id == id, new string[] { "Airplane" });

            if(GetByFlightId == null)
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Not found" });
            }

            return View("Reserve", new ReservationFormModel() { FlightsList = new List<Flight>() { GetByFlightId.First() }, FlightId = GetByFlightId.First().Id });
        }

        public async Task<IActionResult> EditView(string id)
        {
            var GetFlights = flights.GetAll();

            var GetUser = await user.GetByClaim(User);

            if (Authorization.Can("ADMIN", GetUser) || Authorization.Can("CHECKRESERVE", GetUser))
            {
                if (id == null)
                {
                    return View("Reserve", new ReservationFormModel()
                    {
                        FlightsList = GetFlights.ToList()
                    });
                }

                var GetReservation = await reservation.Get(id);

                var MapReservation = mapper.Map<ReservationFormModel>(GetReservation);
                MapReservation.FlightsList = GetFlights.ToList();

                return View("Reserve", MapReservation);
            }
            return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
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
            var GetUser = await user.GetByClaim(User);

            if (Authorization.Can("ADMIN", GetUser) || Authorization.Can("CHECKRESERVE", GetUser) || Authorization.Can("CLIENT", GetUser))
            {

                if (ModelState.IsValid)
                {
                    var MapReservation = mapper.Map<Reservation>(model);
                    MapReservation.Id = Guid.NewGuid().ToString();
                    MapReservation.Flights = await flights.Get(model.FlightId);
                    MapReservation.User = await user.GetByClaim(User);
                    await reservation.Add(MapReservation);
                    return RedirectToAction("ReserveSuccess", "Reservation", new { id = MapReservation.Id });
                }
                return RedirectToAction("Reservations", "Admin");
            }
            return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
        }

        public async Task<IActionResult> ReserveSuccess(string id)
        {
            var data = await reservation.EntityWithEagerLoad(d => d.Id == id, new string[] { "Flights" });

            if (data == null) {
                return RedirectToAction("Index", "Home");
            }

            var MapToModel = mapper.Map<ReservationSummaryModel>(data.First());

            return View(MapToModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ReservationFormModel model)
        {
            var GetReserve = await reservation.Get(id);

            var GetUser = await user.GetByClaim(User);

            if (Authorization.Can("ADMIN", GetUser) || Authorization.Can("CHECKRESERVE", GetUser) || Authorization.Can("CLIENT", GetUser))
            {
                if (GetReserve == null)
                {
                    return RedirectToAction("Reservations", "Admin");
                }

                var MapToUser = mapper.Map(model, GetReserve);
                MapToUser.Flights = await flights.Get(model.FlightId);
                MapToUser.User = await user.GetByClaim(User);
                reservation.Update(MapToUser);

                return RedirectToAction("Reservations", "Admin");
            }
            return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
        }
        public async Task<IActionResult> Delete(string id)
        {
            var GetUser = await user.GetByClaim(User);

            if (Authorization.Can("ADMIN", GetUser) || Authorization.Can("CHECKRESERVE", GetUser) || Authorization.Can("CLIENT", GetUser))
            {
                var GetReserve = await reservation.Get(id);
                await reservation.Delete(GetReserve);
                return RedirectToAction("Reservations", "Admin");
            }
            return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
        }

    }
}
