using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TravelWebApp.Domain.Models;

namespace TravelWebApp.Domain.Entities
{
    public class User : Entity
    {
        [Required(ErrorMessage = "Required.")]
        public virtual string Name { get; set; }
        [Required(ErrorMessage = "Required.")]
        public virtual string Surname { get; set; }
        [Required(ErrorMessage = "Required.")]
        public virtual string Email { get; set; }
        [Required(ErrorMessage = "Required.")]
        public virtual string Password { get; set; }
        [Required(ErrorMessage = "Required.")]
        public virtual string ContactNumber { get; set; }

        public virtual string PasswordHash { get; set; }    
        public virtual Budget Budget { get; set; }
        public virtual IList<UserRole> Roles { get; set; }
        public virtual UserRole Role { get; set; }
        public virtual string TemporaryPassword { get; set; }
        public virtual DateTime? TemporaryPasswordCreatedDate { get; set; }

        public User()
        {
            Roles = new List<UserRole>();
        }
        public User(RegistrationModel model)
        {
            Name = model.Name;
            Surname = model.Surname;
            Email = model.Email;
            Password = model.Password;
            ContactNumber = model.ContactNumber;
        }
        public virtual void AddRole(Role role)
        {
            Roles.Add(new UserRole { User = this, Role = role });
        }

        
        public virtual void ClearRoles()
        {
            Roles.Clear();
        }
        public virtual void SetTempPassword(string temppassword)
        {
            TemporaryPassword = temppassword;
        }
        public virtual long GetRoleId()
        {
            return Roles.Select(x => x.Role.Id).FirstOrDefault();
        }
        public virtual IList<string> GetRoles()
        {
            return Roles.Select(x => x.Role.RoleName).ToList();
        }
        public virtual string GetRoleString()
        {
            var roles = Roles.Select(x => x.Role.RoleName);
            return string.Join(",", roles.ToArray());
        }

    }
}