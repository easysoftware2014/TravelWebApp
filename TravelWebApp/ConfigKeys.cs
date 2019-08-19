using System.Configuration;

namespace TravelWebApp
{
    public static class ConfigKeys
    {
        public static string GetRapidApiKey()
        {
            return ConfigurationManager.AppSettings["RapidApiKey"];
        }
        public static string GetRapidApiHost()
        {
            return ConfigurationManager.AppSettings["RapidApiPropertyBookingHost"]; 
        }

        public static string GetCurrencyHost()
        {
            return ConfigurationManager.AppSettings["RapidApiCurrencyHost"]; 
        }

        public static string RapidApiCurrencyEndpoint()
        {
            return ConfigurationManager.AppSettings["RapidApiCurrencyEndpoint"];
        }

        public static string RapidApiBookingEndPoint()
        {
            return ConfigurationManager.AppSettings["RapidApiPropertyBookingEndpoint"];
        }
    }
}