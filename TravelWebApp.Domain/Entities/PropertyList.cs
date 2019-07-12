using System;
using Newtonsoft.Json;

namespace TravelWebApp.Domain.Entities
{
    public class PropertyList
    {
        [JsonProperty(PropertyName = "min_total_price")]
        public string MinimumPrice { get; set; }
        [JsonProperty(PropertyName = "business_review_score_word")]
        public string Review { get; set; }
        [JsonProperty(PropertyName = "currencycode")]
        public string CurrencyCode { get; set; }
        [JsonProperty(PropertyName = "deals")]
        public Deals Deals { get; set; }    
        public PriceBreakdown PriceBreakdown { get; set; }
        [JsonProperty(PropertyName = "main_photo_url")]
        public string PhotoUrl { get; set; }
        [JsonProperty(PropertyName = "hotel_name")]
        public string HotelName { get; set; }
        [JsonProperty(PropertyName = "available_rooms")]
        public string AvailableRooms { get; set; }
        [JsonProperty(PropertyName = "hotel_include_breakfast")]
        public int IncludeBreakfast { get; set; }
        [JsonProperty(PropertyName = "review_score")]
        public Double ReviewScore { get; set; }
        [JsonProperty(PropertyName = "cleanliness_score_word")]
        public string CleanlinessScore { get; set; }
        

    }

    public class PriceBreakdown
    {
        [JsonProperty(PropertyName = "currency")]
        public int Currency { get; set; }
        [JsonProperty(PropertyName = "gross_price")]
        public int GrossPrice { get; set; }
    }

    public class Deals
    {

    }
}