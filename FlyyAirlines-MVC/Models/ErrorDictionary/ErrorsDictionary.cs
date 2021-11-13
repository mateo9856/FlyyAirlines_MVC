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
            {"Internal Error", "Wewnętrzny błąd serwera"},
            {"Request Error", "Niepoprawne zapytanie serwera"},
            {"Media Type Error", "Niepoprawny format."},
            {"Not found", "Nie znaleziono!" },
            {"FormError", "Formularz źle wypełniony!" }
        };
    }
}
