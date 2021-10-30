﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyyAirlines.Services.News
{
    public interface INewsService
    {
        Task<string> UploadFile(IFormFile file);
    }
}
