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
   
}