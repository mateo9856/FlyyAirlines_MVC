using DinkToPdf;
using DinkToPdf.Contracts;
using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Controllers
{
    public class PDFController : Controller
    {
        private readonly IBaseService<Reservation> reservations;
        private readonly IConverter _converter;
        public PDFController(IBaseService<Reservation> reserve, IConverter converter)
        {
            reservations = reserve;
            _converter = converter;
        }

        public async Task<IActionResult> GeneratePDF(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var GetReserve = await reservations
                .EntityWithEagerLoad(d => d.Id == id, new string[] { "Flights" });

            if(GetReserve == null)
            {
                return NotFound();
            }

            var GlobalSet = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Reservation Check",
            };

            var GetPath = Directory.GetCurrentDirectory();
            string ChangedPath = Regex.Replace(GetPath, @"\\FlyyAirlines-MVC$", "\\FlyyAirlines.Services\\PDFGenerator");

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(ChangedPath, "assets", "style.css") },
                HtmlContent = TemplateGenerator.GetHTMLString(GetReserve.First()),
                HeaderSettings = { FontName = "Arial", FontSize = 12, Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 12, Line = true }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = GlobalSet,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return File(file, "application/pdf");
        }
    }
}
