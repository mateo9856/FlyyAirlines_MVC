using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines_MVC.Models;
using FlyyAirlines_MVC.Models.ErrorDictionary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAirplanesFlightsService airplanesFlightsService;
        private readonly IBaseService<Flight> flights;
        private readonly IBaseService<News> news;
        public HomeController(IBaseService<Flight> _baseService, IBaseService<News> baseNews, IAirplanesFlightsService _airplanesFlightsService)
        {
            airplanesFlightsService = _airplanesFlightsService;
            flights = _baseService;
            news = baseNews;
        }

        public IActionResult Index()
        {

            var GetFlights = flights.GetAll();
            var GetNews = news.GetAll();
            var GetBestSellerCount = airplanesFlightsService.GetBestSellerFlightCount();
            var Model = new HomeModel
            {
                Flights = GetFlights,
                IsSearched = false,
                News = GetNews,
                BestSeller = GetFlights.FirstOrDefault(),
                BestSellerCount = GetBestSellerCount,
                SearchedFlights = null
            };

            return View(Model);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult NotFoundPage()
        {
            return View("NotFound");
        }

        public IActionResult Error(string ErrorName)
        {
            var GetError = ErrorsDictionary.ErrorsList[ErrorName];

            return View(new ErrorViewModel() {RequestCode = ErrorName, RequestDescription = GetError });
        }
    }
}
