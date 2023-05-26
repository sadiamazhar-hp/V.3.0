using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using V._3._0.App_Data;

using V._3._0.Models;

namespace V._3._0.Controllers
{
    
    public class SignUpController : Controller
    {
        private readonly HospitalData db;

        public SignUpController(HospitalData _db) { db = _db; }

        public IActionResult Signup()
        {
            
            /*var HosUser = new SignUp { HospitalName = HosUser.HospitalName, 
                location = "kda", 
                Email = "sad@gmail.com", 
                Password = "*****", 
                ConfirmPassword = "****" };*/
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUp newHosUser) 
        {
            SignUp User = new SignUp()
            {
                HospitalName = newHosUser.HospitalName,
                location = newHosUser.location,
                Email = newHosUser.Email,
                Password = newHosUser.Password,
                ConfirmPassword = newHosUser.ConfirmPassword
            };
            db.HosUser.Add(User);
            db.SaveChanges();
            return View();
        }
            /*TempData["Message"] = "You Are Now A User";
            hosData.commit();
            return RedirectToPage("~/Views/SignUp/user.cshtml",new { HosId = SignUp.Id });*/
            
    }
    
}
