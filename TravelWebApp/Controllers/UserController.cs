using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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

        private string EncryptPassword(string modelPassword)
        {
            return Encryption.Encrypt(modelPassword);
        }
    }

}