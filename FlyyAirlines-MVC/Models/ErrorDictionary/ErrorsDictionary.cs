using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models.ErrorDictionary
{
    public static class ErrorsDictionary
    {
        public static Dictionary<string, string> ErrorsList = new Dictionary<string, string>()
        {
            {"Forbidden", "Brak uprawnień do tego modułu/elementu"},
            {"Unauthorized", "Nie jesteś zalogowany!"},
            {"", ""}
        };
    }
}
