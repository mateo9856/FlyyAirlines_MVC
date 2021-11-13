using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines_MVC.Models.EmployeeActions;
using FlyyAirlines_MVC.Models.StaticModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class EmployeeOperations : Controller
    {
        private readonly IUserService user;
        private readonly IBaseService<Reservation> reservation;
        private readonly IReserveService reserveService;
        private readonly IMapper mapper;
        public EmployeeOperations(IUserService _user, IBaseService<Reservation> _reservation, IReserveService _reserveService
            , IMapper _mapper)
        {
            user = _user;
            reservation = _reservation;
            reserveService = _reserveService;
            mapper = _mapper;
        }

        public IActionResult Support()
        {
            return View();
        }

        public async Task<IActionResult> CheckReservation()
        {
            var GetUser = await user.GetByClaim(User);

            if(Authorization.Can("CHECKRESERVE", GetUser))
            {
                return View("Views/Employee/CheckReservation.cshtml", new CheckReserveModel() {
                    IsChecked = false
                });
            }

            return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
        }

        [HttpPost]
        public async Task<IActionResult> CheckReservation(CheckReserveModel model)
        {
            var GetUser = await user.GetByClaim(User);

            if (Authorization.Can("CHECKRESERVE", GetUser))
            {
                dynamic GetReservation = null;
                if (model.ReservationId == null)
                {
                    if(model.PersonIdentify != null)
                    {
                        GetReservation = await reserveService.GetByPersonIdentify(model.PersonIdentify.Value);
                    }
                } else
                {
                    GetReservation = await reserveService.GetByFlightId(model.ReservationId);
                }

                if(GetReservation == null)
                {
                    return RedirectToAction("EmployeePanel", "Employee");
                }

                var MapToModel = mapper.Map<CheckReserveModel>(GetReservation);
                MapToModel.IsChecked = true;
                return View("Views/Employee/CheckReservation.cshtml", MapToModel as CheckReserveModel);
            }

            return RedirectToAction("Error", "Home", new { ErrorName = "Forbidden" });
        }
    }
}
