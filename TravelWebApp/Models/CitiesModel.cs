namespace TravelWebApp.Models
{
    
    public class CitiesModel
    {
       // public string time_zone { get; set; }
        public string name { get; set; }
        //public Coordinates coordinates { get; set; }
        public string code { get; set; }
       // public string country_code { get; set; }
    }
    public class Coordinates
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }
    
}