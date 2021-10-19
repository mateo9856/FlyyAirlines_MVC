using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines_MVC.Models.FormModels;
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
        private readonly IMapper mapper;

        public FlightController(IBaseService<Flight> _flight, IBaseService<Airplane> _airplane, IAirplanesFlightsService _airplaneFlightsService, IMapper _mapper)
        {
            flight = _flight;
            airplane = _airplane;
            airplanesFlightsService = _airplaneFlightsService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditAirplaneView(string id)
        {
            if(id == null)
            {
                return View();
            }

            var GetAirplane = airplane.Get(id);

            return View(mapper.Map<AirplaneFormModel>(GetAirplane));
        }

        [HttpPost]
        public IActionResult CreateAirplane(AirplaneFormModel model)
        {
            if(ModelState.IsValid)
            {
                var MapAirplane = mapper.Map<Airplane>(model);
                airplane.Add(MapAirplane);
            }

            return RedirectToAction("Airplanes", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> EditAirplane(string id, AirplaneFormModel model)
        {
            var GetAirplane = await airplane.Get(id);

            if(GetAirplane == null)
            {
                return RedirectToAction("Airplanes", "Admin");
            }

            var MapToAirplane = mapper.Map(model, GetAirplane);

            airplane.Update(MapToAirplane);

            return RedirectToAction("Airplanes", "Admin");
        }

        public async Task<IActionResult> DeleteAirplane(string id)
        {
            var GetAirplane = await airplane.Get(id);
            await airplane.Delete(GetAirplane);
            return RedirectToAction("Airplanes", "Admin");
        }

        public IActionResult EditFlightView(string id)
        {
            var GetAirplane = airplane.GetAll().ToList();

            if(id == null)
            {
                return View(new FlightFormModel() { 
                Airplanes = GetAirplane
                });
            }

            var GetFlight = flight.Get(id);

            var MapToModel = mapper.Map<FlightFormModel>(GetFlight);

            MapToModel.Airplanes = GetAirplane;

            return View(MapToModel);
        }

        [HttpPost]
        public IActionResult CreateFlight(FlightFormModel model)
        {
            if (ModelState.IsValid)
            {
                var MapToFlight = mapper.Map<Flight>(model);
                flight.Add(MapToFlight);
            }

            return RedirectToAction("Flights", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> EditFlight(string id, FlightFormModel model)
        {
            var GetFlight = await flight.Get(id);

            if(GetFlight == null)
            {
                return RedirectToAction("Flights", "Admin");
            }

            var MapToFlight = mapper.Map(model, GetFlight);

            flight.Update(MapToFlight);

            return RedirectToAction("Flights", "Admin");
        }
        public async Task <IActionResult> DeleteFlight(string id)
        {
            var GetFlight = await airplane.Get(id);
            await airplane.Delete(GetFlight);
            return RedirectToAction("Flights", "Admin");
        }
    }
}
