using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using TravelWebApp.Domain.Entities;
using TravelWebApp.Models;
using TravelWebApp.Repository;
using TravelWebApp.Service.Contracts;
using TravelWebApp.Service.Services;

namespace TravelWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UserController()
        {
            _userService = new UserService();
            _roleService = new RoleService(); 
        }
        // GET: User
        public ActionResult Index(string returnUrl)
        {
            var model = new LoginModel();

            if (Session["User"] == null)
                return RedirectToAction("Login", model);

            return View(" ", model);
        }
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginModel();

            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
                returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnURL = returnUrl;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {

            try
            {
                var decodedUrl = "";

                if (!string.IsNullOrEmpty(returnUrl))
                    decodedUrl = Server.UrlDecode(returnUrl);

                var passwordHash = MD5Hasher.GetMd5Hash(model.Username);
                var password = Encryption.Encrypt(model.Password);
                var user = _userService.GetUserByCredentials(model.Username, password);

                if (user != null)
                {
                    if (passwordHash == user.PasswordHash)
                    {
                        Session["User"] = user;
                        FormsAuthentication.SetAuthCookie(user.Name, false);

                        if (Url.IsLocalUrl(decodedUrl))
                        {
                            return Redirect(decodedUrl);
                        }
                    }
                    else
                    {
                        model.ErrorMessage = "Incorrect combination of email and password, please reenter";
                        return View(model);
                    }
                }
                else
                {
                    model.ErrorMessage = "Incorrect combination of email and password, please reenter";
                    return View(model);
                }

                return View(model);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Budget");
        }
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationModel model)
        {

            try
            {
                var password = EncryptPassword(model.Password);
                var passwordHash = MD5Hasher.GetMd5Hash(model.Email);

                var role = _roleService.Get(1);

                var user = new User
                {
                    ContactNumber = model.ContactNumber,
                    Email = model.Email,
                    Name = model.FirstName,
                    Surname = model.LastName,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Password = password,
                    PasswordHash = passwordHash
                };
                role.CreatedAt = DateTime.Now;
                role.ModifiedAt = DateTime.Now;

                user.AddRole(role);
                var id = _userService.Save(user);

                if (id > 0)
                    return RedirectToAction("Login","User");
                    
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return View();
        }

        private string EncryptPassword(string modelPassword)
        {
            return Encryption.Encrypt(modelPassword);
        }
    }

}