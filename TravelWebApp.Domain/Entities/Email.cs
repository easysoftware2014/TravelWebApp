using System.Collections.Generic;

namespace TravelWebApp.Domain.Entities
{
    public class Email
    {
        public string recipient { get; set; }
        public string sender { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
    }

    //public class Personalization
    //{
    //    public List<recipient> to { get; set; }
    //    public string subject { get; set; }
    //}

    //public class sender
    //{
    //    public string email { get; set; }
    //}
    //public class recipient
    //{
    //    public string email { get; set; }
    //}
    //public class subject
    //{
    //   public string message { get; set; }

    //}

   
}