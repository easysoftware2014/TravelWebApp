using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Remotion.Linq.Clauses.ResultOperators;
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

            return RedirectToAction("Login");
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
        //[ValidateAntiForgeryToken]
        public JsonResult Login(string username, string password, string returnUrl)
        {

            try
            {
                var decodedUrl = "";

                if (!string.IsNullOrEmpty(returnUrl))
                    decodedUrl = Server.UrlDecode(returnUrl);

                var passwordHash = MD5Hasher.GetMd5Hash(username);
                var passwordNew = Encryption.Encrypt(password);
                var user = _userService.GetUserByCredentials(username, passwordNew);

                if (user != null)
                {
                    if (passwordHash == user.PasswordHash)
                    {
                        Session["User"] = user;
                        FormsAuthentication.SetAuthCookie(user.Name, false);
                        var response = Response;

                        response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, SetCookie(user)));

                        if (Url.IsLocalUrl(decodedUrl))
                        {
                            if (!string.IsNullOrEmpty(decodedUrl))
                                return Json(decodedUrl);
                        }

                        var userModel = new UserModel(user);
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);

                        //return RedirectToAction("Index","BookingManagement");
                    }
                    else
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);

                        //model.ErrorMessage = "Incorrect combination of email and password, please reenter";
                        //return View(model);
                    }
                }
                else
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);

                    //model.ErrorMessage = "Incorrect combination of email and password, please reenter";
                    //return View(model);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        private string SetCookie(User user)
        {
            var ticket = new FormsAuthenticationTicket(1,
                user.Name,
                DateTime.Now,
                DateTime.Now.AddDays(30),
                true,
                user.Id.ToString(),
                FormsAuthentication.FormsCookiePath);
            // Encrypt the ticket.
            return FormsAuthentication.Encrypt(ticket);
            // Create the cookie.
            
        }

        [Authorize]
        public ActionResult UserAdmin()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Register(string email, string password, string name, string surname, string contact)
        {

            try
            {
                var newPassword = EncryptPassword(password);
                var passwordHash = MD5Hasher.GetMd5Hash(email);

                var role = _roleService.Get(1);

                var user = new User
                {
                    ContactNumber = contact,
                    Email = email,
                    Name = name,
                    Surname = surname,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Password = newPassword,
                    PasswordHash = passwordHash
                };

                role.CreatedAt = DateTime.Now;
                role.ModifiedAt = DateTime.Now;

                user.AddRole(role);
                var id = _userService.Save(user);

                if (id > 0)
                {
                    Session["User"] = user;
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, SetCookie(user)));
                    var emailContent = SendEmail(user.Email);
                    var serializedObj = JsonConvert.SerializeObject(emailContent);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(serializedObj);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var uri = ConfigurationManager.AppSettings["RapidApiEmailEndpoint"];
                    var apiKey = ConfigurationManager.AppSettings["RapidApiKey"];
                    var apiEmailHost = ConfigurationManager.AppSettings["RapidApiEmailHost"];

                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Key", apiKey);
                        client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Host", apiEmailHost);
                        
                        var response = client.PostAsync(uri, byteContent).Result;
                        var content = response.Content.ReadAsStringAsync();
                    }

                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private Email SendEmail(string userEmail)
        {
            return new Email
            {
                recipient = "ayandapatrick@gmail.com",
                message = "Welcome to api",
                sender = "ayandapatrick@gmail.com",
                subject = "Registration"
            };
        }

        private string EncryptPassword(string modelPassword)
        {
            return Encryption.Encrypt(modelPassword);
        }
    }

}