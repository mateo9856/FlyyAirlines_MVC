using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlyyAirlines.Services.News
{
    public class NewsService : INewsService
    {
        public string ReplaceImageSource(string name)
        {
            string CutStart = Regex.Replace(name, @"^.*?(?=\\images)", "");
            string ChangeSlashes = Regex.Replace(CutStart, @"\\", @"/");
            return ChangeSlashes;
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                return null;
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);

            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return ReplaceImageSource(path);

            } catch (Exception)
            {
                return null;
            }

        
        }
    }
}
