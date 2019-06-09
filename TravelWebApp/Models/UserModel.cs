
using TravelWebApp.Domain.Entities;

namespace TravelWebApp.Models
{
    public class UserModel
    {
        public string Name { get; set; }    
        public string Surname { get; set; }    
        public string Email { get; set; }
        public string ContactNumber { get; set; }

        public UserModel()
        {}

        public UserModel(User user)
        {
            Name = user.Name;
            Surname = user.Surname;
            ContactNumber = user.ContactNumber;
            Email = user.Email;

        }
    }
}