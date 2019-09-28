using System.Collections.Generic;
using System.Web.WebPages.Html;

namespace TravelWebApp.Models
{
    public class CityModel
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }
    }
}