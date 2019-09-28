using System;

namespace TravelWebApp.Models
{
    public class Flight
    {
        public double value { get; set; }
        public int trip_class { get; set; }
        public bool show_to_affiliates { get; set; }
        public string return_date { get; set; }
        public string origin { get; set; }
        public int number_of_changes { get; set; }
        public object gate { get; set; }
        public DateTime found_at { get; set; }
        public object duration { get; set; }
        public int distance { get; set; }
        public string destination { get; set; }
        public string depart_date { get; set; }
        public bool actual { get; set; }
    }

    public class CheapestFlight
    {
        public string price { get; set; }
        public string airline { get; set; }
        public string flight_number { get; set; }
        public string departure_at { get; set; }
        public string return_at { get; set; }
        public string expires_at { get; set; }
        public string token { get; set; }
    }
}