using Newtonsoft.Json;

namespace TravelWebApp.Models
{
    public class DestinationModel
    {
        [JsonProperty(PropertyName = "dest_id")]
        public string DestinationId { get; set; }
        [JsonProperty (PropertyName = "country")]
        public string Country { get; set; }
        [JsonProperty (PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty (PropertyName = "region")]
        public string Region { get; set; }
        [JsonProperty (PropertyName = "nr_hotels")]
        public string NumberOfHotels { get; set; }
        [JsonProperty (PropertyName = "dest_type")]
        public string DestinationType { get; set; }

    }

    public class RootObject
    {
        public string lc { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public int hotels { get; set; }
        public int rtl { get; set; }
        public string city_name { get; set; }
        public string country { get; set; }
        public string dest_id { get; set; }
        public string dest_type { get; set; }
        public int nr_hotels { get; set; }
        public string timezone { get; set; }
        public string image_url { get; set; }
        public string cc1 { get; set; }
        public int? city_ufi { get; set; }
        public string region { get; set; }
        public double latitude { get; set; }
        public string type { get; set; }
        public double longitude { get; set; }
    }
}