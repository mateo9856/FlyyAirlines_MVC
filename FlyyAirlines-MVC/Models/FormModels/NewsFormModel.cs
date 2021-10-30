using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models.FormModels
{
    public class NewsFormModel
    {
        public string Id { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public DateTime PublicDate { get; set; }
        public IFormFile Image { get; set; }
    }
}
