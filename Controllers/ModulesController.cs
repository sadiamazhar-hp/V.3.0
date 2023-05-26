using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;
using V._3._0.App_Data;
using V._3._0.Models ;

namespace V._3._0.Controllers
{
    public class ModulesController : Controller
    {
        public readonly HospitalData db;
        public ModulesController(HospitalData db)
        {
            this.db = db;
        }

        public IActionResult Patients()
        {
            var patients = db.Patients.ToList();
            return View(patients);
        }
        public IActionResult NewPatient() { return View(); }

        [HttpPost]
        public IActionResult NewPatient(Models.Patients newpatient)
        {
            Models.Patients patient = new Models.Patients()
            {
                Id = newpatient.Id,
                Name = newpatient.Name,
                Status = newpatient.Status,
                DateOfAdm = newpatient.DateOfAdm,
                DateOfDis = newpatient.DateOfDis,
                Roomno = newpatient.Roomno,
                //PersonalInfo = new PersonalInfo() { PatId = newpatient.Id }

            };
            db.Patients.Add(patient);
            db.SaveChanges();
            return RedirectToAction("Patients", "Modules");


        }
        [HttpPost]
        public IActionResult GetPatId() 
        {
            int value = Convert.ToInt32( Request.Form["PatientId"]);

            return RedirectToAction("PersonalInfo", "Modules", new {patientId = value });
        }
        /*int value = Convert.ToInt32( Request.Form["PatientId"]);
          ViewBag.ButtonValue = value;
          return View();
         */

        public IActionResult PersonalInfo(int patientId) {
            ViewBag.PatientId = patientId;
            return View(patientId); 
        }
        public IActionResult PersonalInfoView(Models.PersonalInfo viewpat) { return View(viewpat); }

        [HttpPost]
        public IActionResult PersonalInfo(Models.PersonalInfo personalInfo )
        {
            Models.PersonalInfo personalInfo1 = new Models.PersonalInfo()
            {
                PatId = personalInfo.PatId,
                PIId = personalInfo.PIId,
                Name = personalInfo.Name,
                FatherName = personalInfo.FatherName,
                Gender = personalInfo.Gender,
                Age = personalInfo.Age,
                Email = personalInfo.Email,
                Address = personalInfo.Address,
                Contact = personalInfo.Contact,
            };
            db.PersonalInfo.Add(personalInfo1);
            db.SaveChanges();
            return RedirectToAction("PersonalInfoView", "Modules",new{ viewpat = personalInfo1 });
        }
    }
}

