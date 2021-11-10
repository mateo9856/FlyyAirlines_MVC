using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines_MVC.Models.FormModels;
using FlyyAirlines_MVC.Models.StaticModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
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
        private readonly IMapper mapper;
        private readonly IUserService user;

        public FlightController(IBaseService<Flight> _flight, IBaseService<Airplane> _airplane, IAirplanesFlightsService _airplaneFlightsService, IMapper _mapper,
            IUserService userManager)
        {
            flight = _flight;
            airplane = _airplane;
            airplanesFlightsService = _airplaneFlightsService;
            mapper = _mapper;
            user = userManager;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var GetElements = airplanesFlightsService.GetAllFlights();
            var PagerModel = await PagingList.CreateAsync(GetElements, 10, page);
            return View(PagerModel);
        }

        public async Task<IActionResult> GetAirplane(string id)
        {
            var GetAirplane = await airplane.Get(id);
            return View(GetAirplane);
        }

        public async Task<IActionResult> GetFlight(string id)
        {
            var GetFlight = await flight.EntityWithEagerLoad(d => d.Id == id, new string[] { "Airplane" });
            return View(GetFlight.First());
        }

        public async Task<IActionResult> EditAirplaneView(string id)
        {
            var GetUser = await user.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            if (id == null)
            {
                return View(new AirplaneFormModel());
            }

            var GetAirplane = await airplane.Get(id);

            var AirplaneModel = mapper.Map<AirplaneFormModel>(GetAirplane);

            return View(AirplaneModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAirplane(AirplaneFormModel model)
        {
            var GetUser = await user.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }
            if (ModelState.IsValid)
            {
                var MapAirplane = mapper.Map<Airplane>(model);
                MapAirplane.Id = Guid.NewGuid().ToString();
                await airplane.Add(MapAirplane);
            }

            return RedirectToAction("Airplanes", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> EditAirplane(string id, AirplaneFormModel model)
        {
            var GetAirplane = await airplane.Get(id);

            var GetUser = await user.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            if (GetAirplane == null)
            {
                return RedirectToAction("Airplanes", "Admin");
            }

            var MapToAirplane = mapper.Map(model, GetAirplane);

            airplane.Update(MapToAirplane);

            return RedirectToAction("Airplanes", "Admin");
        }

        public async Task<IActionResult> DeleteAirplane(string id)
        {
            var GetUser = await user.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            var GetAirplane = await airplane.Get(id);
            await airplane.Delete(GetAirplane);
            return RedirectToAction("Airplanes", "Admin");
        }

        public async Task<IActionResult> EditFlightView(string id)
        {
            var GetUser = await user.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            var GetAirplane = airplane.GetAll().ToList();

            if (id == null)
            {
                return View(new FlightFormModel()
                {
                    Airplanes = GetAirplane
                });
            }

            var GetFlight = await flight.Get(id);

            var MapToModel = mapper.Map<FlightFormModel>(GetFlight);

            MapToModel.Airplanes = GetAirplane;

            return View(MapToModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlight(FlightFormModel model)
        {
            var GetUser = await user.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            if (ModelState.IsValid)
            {
                var GetAirplane = await airplane.Get(model.AirplaneId);
                var MapToFlight = mapper.Map<Flight>(model);
                MapToFlight.Airplane = GetAirplane;
                MapToFlight.Id = Guid.NewGuid().ToString();
                await flight.Add(MapToFlight);
            }

            return RedirectToAction("Flights", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> EditFlight(string id, FlightFormModel model)
        {
            var GetUser = await user.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            var GetFlight = await flight.Get(id);

            if (GetFlight == null)
            {
                return RedirectToAction("Flights", "Admin");
            }

            var MapToFlight = mapper.Map(model, GetFlight);

            flight.Update(MapToFlight);

            return RedirectToAction("Flights", "Admin");
        }
        public async Task<IActionResult> DeleteFlight(string id)
        {
            var GetUser = await user.GetByClaim(User);

            if (!Authorization.Can("ADMIN", GetUser))
            {
                return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
            }

            var GetFlight = await airplane.Get(id);
            await airplane.Delete(GetFlight);
            return RedirectToAction("Flights", "Admin");
        }
    }
}
