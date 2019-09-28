using System.Configuration;

namespace TravelWebApp
{
    public static class ConfigKeys
    {
        public static string GetRapidApiKey()
        {
            return ConfigurationManager.AppSettings["RapidApiKey"];
        }
        public static string GetRapidPropertyApiHost()
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
        public static string RapidApiFlightEndPoint()
        {
            return ConfigurationManager.AppSettings["RapidApiFlightBookingEndpoint"];
        }
        public static string RapidApiFlightHost()
        {
            return ConfigurationManager.AppSettings["RapidApiFlightBookingHost"];
        }
        public static string GetCheapestFlightTicket()
        {
            return ConfigurationManager.AppSettings["CheapestFlightTicket"];
        }
    }
}