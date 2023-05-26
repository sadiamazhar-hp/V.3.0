using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
using V._3._0.App_Data;
using V._3._0.Models;

namespace V._3._0.Controllers
{
    public class LoginController : Controller
    {
        /*private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }*/
        private readonly HospitalData db;

        public LoginController(HospitalData _db) { db = _db; }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            var v = db.HosUser.Where(a => a.Email == login.Emailid).FirstOrDefault();
            if (v != null)
            {
                if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                {
                    int timeout = login.RememberMe ? 525600 : 20;
                    var ticket = new FormsAuthenticationTicket(login.Emailid, login.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            else
            {
                message = "Invalid credential provided";
            }
            ViewBag.Message = message;
            return View();

        }*/
            public IActionResult Home() 
            {
            return View();
            if(ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return RedirectToPage("/home");
            }
            }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}