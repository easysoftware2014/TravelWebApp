using Newtonsoft.Json;

namespace TravelWebApp.Models
{
    public class Destination
    {
        [JsonProperty (PropertyName = "result")]
        public DestinationModel[] DestinationModels { get; set; }
    }
}