using System;
using System.Collections.Generic;
using System.Text;

namespace TravelWebApp.Domain.Models
{
    public class RegistrationModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }
}
