using System.Collections.Generic;

namespace TravelWebApp.Models
{
    public class FlightModel
    {
        public bool success { get; set; }
        public Flight[] data { get; set; }
    }

    public class CheapestFlightModel
    {
        public CheapestFlight[] data { get; set; }
    }
}