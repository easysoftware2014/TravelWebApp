using System;
using Newtonsoft.Json;
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Models
{
    public class PropertyListModel
    {
        [JsonProperty(PropertyName = "result")]
        public PropertyList[] Result { get; set; }
    }
}