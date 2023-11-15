using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;
using V._3._0.App_Data;
using V._3._0.Interfaces;
using V._3._0.Methods;
using V._3._0.Models ;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Net.Http.Headers;
using System.Drawing;
using System.Data.Entity;
using System.IO;

namespace V._3._0.Controllers
{
    public class ModulesController : Controller
    {
        public readonly HospitalData db;
        private readonly IPatients patientsdata;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ModulesController(HospitalData db, IPatients patientsdata, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.patientsdata = patientsdata;
            this.webHostEnvironment = webHostEnvironment;
        }
        List<Patients> patients;

        //Method to Display the list of patients in patients view
        public IActionResult Patients()
        {
            patients = db.Patients.ToList();
            return View(patients);
        }
        //Specific method to Search Patients
        [HttpGet]
        public IActionResult OnGet(string searchinput)
        {
            if (searchinput == null)
            {
                // Handle the null input gracefully, such as displaying an error message or redirecting to a different page.
                // For example:
                return RedirectToAction("Patients", "Modules");
            }

            IEnumerable<Patients> patients = patientsdata.GetPatientByName(searchinput);
            return View("~/Views/Modules/Patients.cshtml", patients);
        }
        [HttpPost]
        //Delete patient using ID
        public IActionResult OnDelete(int PatientId)
        {
            var patient = db.Patients.Find(PatientId);
            if (patient != null)
            {
                db.Patients.Remove(patient);
                db.SaveChanges();
            }
            return RedirectToAction("Patients", "Modules");
        }

        //Display form for new patient for entry
        public IActionResult NewPatient() { return View(); }

        //Submit the form and add new patient in patient list
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

        /*At the PersonalInfo, Appointment, Medhistory, Payments button
         * get the patient Id to see if it is present if not then
         * display forms to add their information
         */
        [HttpPost]
        public IActionResult GetPatId(int patid, string Button)
        {
            //Convert.ToInt32(Request.Form["PatientId"]);
            int value = patid;
            var patient = db.Patients.Find(value);
            var PatientName = patient.Name;
            if (Button == "personalinfo")
            {
                var existingPersonalInfo = db.PersonalInfo.FirstOrDefault(pi => pi.PatId == value);


                if (existingPersonalInfo != null)
                {
                    var personalInfoId = existingPersonalInfo.PIId;
                    // PersonalInfo exists for the specified patient
                    return RedirectToAction("PersonalInfoView", "Modules", new { PersonalInfoId = personalInfoId, patientId = value });
                }
                else
                {
                    // PersonalInfo does not exist for the specified patient
                    return RedirectToAction("PersonalInfo", "Modules", new { patientId = value, patientName = PatientName });
                }
            }
            if (Button == "medicalinfo")
            {
                return RedirectToAction("MedFiles", "Modules", new { patientId = value, patientName = PatientName });
            }
            if (Button == "AppList")
            {
                return RedirectToAction("AppList", "Modules", new { PatientId = value, PatientName = PatientName });
            }
            if (Button == "Payment")
            {
                return RedirectToAction("Payment", "Modules", new { PatientID = value });
            }
            return RedirectToAction("Patients", "Modules");


        }
        //Display form of PersonalInfo for every specific patient
        public IActionResult PersonalInfo(int patientId, string patientName)
        {
            var personalInfo = new Models.PersonalInfo { PatId = patientId, Name = patientName };
            return View(personalInfo);
        }
        //Display PersonalInfo of specific patients
        public IActionResult PersonalInfoView(int PersonalInfoId, int patientId)
        {
            var PatPersonalInfo = db.PersonalInfo.Find(PersonalInfoId);
            ViewBag.patId = patientId;
            return View(PatPersonalInfo);
        }

        //Submit and added the PersonalInfo of Specified patient
        [HttpPost]
        public IActionResult PersonalInfo(Models.PersonalInfo personalInfo)
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
            return RedirectToAction("Patients", "Modules", new { viewpat = personalInfo1 });
        }
        //Delete Patients Personal Info using patientid
        [HttpPost]
        public IActionResult OnDeletePersonalInfo(int patId, int infoId)
        {
            var personalinfo = db.PersonalInfo.Find(infoId);
            db.PersonalInfo.Remove(personalinfo);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Personal information deleted successfully.";
            return RedirectToAction("Patients", "Modules");


        }


        // view to update patient personal info
        public IActionResult OnUpdatePersonalInfo(int InfoId)
        {
            var Infodetail = db.PersonalInfo.Find(InfoId);
            return View(Infodetail);
        }
        [HttpPost]
        //updated personalinfo of patients personal info
        public IActionResult UpdatedPersonalInfo(PersonalInfo UpdatedpersonalInfo)
        {
            var existingpersonalinfo = db.PersonalInfo.Find(UpdatedpersonalInfo.PIId);
            existingpersonalinfo.Name = UpdatedpersonalInfo.Name;
            existingpersonalinfo.FatherName = UpdatedpersonalInfo.FatherName;
            existingpersonalinfo.Address = UpdatedpersonalInfo.Address;
            existingpersonalinfo.Age = UpdatedpersonalInfo.Age;
            existingpersonalinfo.Gender = UpdatedpersonalInfo.Gender;
            existingpersonalinfo.Email = UpdatedpersonalInfo.Email;
            existingpersonalinfo.Contact = UpdatedpersonalInfo.Contact;
            db.SaveChanges();
            TempData["UpdateMessage"] = UpdatedpersonalInfo.Name + "Personal information Updated successfully ";
            return RedirectToAction("Patients", "Modules");
        }


        //update patient using ID
        public IActionResult OnUpdate(int PatientId)
        {
            var patient = db.Patients.Find(PatientId);
            return View(patient);
        }



        //updated done on patient
        [HttpPost]

        public IActionResult OnUpdated(Patients UpdatedPatient)
        {

            var existingPatient = db.Patients.Find(UpdatedPatient.Id);
            existingPatient.Name = UpdatedPatient.Name;
            existingPatient.Status = UpdatedPatient.Status;
            existingPatient.Roomno = UpdatedPatient.Roomno;
            existingPatient.DateOfAdm = UpdatedPatient.DateOfAdm;
            existingPatient.DateOfDis = UpdatedPatient.DateOfDis;
            db.SaveChanges();
            return RedirectToAction("Patients", "Modules");
        }
        //For display of multiple medical file of a specific patient
        public IActionResult MedFiles(int patientId)
        {
            // List of medical files of a specific patient
            List<MedicalInfo> images = db.MedicalInfo
                .Where(medicalInfo => medicalInfo.PatientsId == patientId)
                .ToList();

            ViewBag.ID = patientId;
            return View(images);
        }


        //For medicalinfo of the patient to upload
        public IActionResult PatientMed(int Upload)
        {
            ViewBag.Patid = Upload;
            return View();
        }
        /*[HttpPost]
        public IActionResult Upload(int Upload)
        {
            return RedirectToAction("PatientMed", "Modules", new { Patid = Upload });
        }*/

        [HttpPost]
        public IActionResult PatientMed(MedicalInfo file, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Convert image file to byte array
                byte[] imageBytes;
                using (var memoryStream = new MemoryStream())
                {
                    ImageFile.CopyTo(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }

                // Get the original file name from Content-Disposition header
                var contentDisposition = ContentDispositionHeaderValue.Parse(ImageFile.ContentDisposition);
                var originalFileName = contentDisposition.FileName.ToString();

                MedicalInfo File = new MedicalInfo()
                {
                    PatientsId = file.PatientsId,
                    ImageFileName = originalFileName,
                    ImageFile = imageBytes
                };
                db.MedicalInfo.Add(File);
                db.SaveChanges();
                return RedirectToAction("MedFiles", "Modules", new { patientId = File.PatientsId });
            }
            return View();

        }
        [HttpPost]
        //Delete MedicalFile using MedId
        public IActionResult OnDeleteMedfile(int medid, int patid)
        {
            var medfile = db.MedicalInfo.Find(medid);
            db.MedicalInfo.Remove(medfile);
            db.SaveChanges();
            return RedirectToAction("MedFiles", "Modules", new { patientId = patid });
        }
        public IActionResult GetImage(MedicalInfo file)
        {

            if (file != null)
            {
                return File(file.ImageFile, "image/jpeg");
            }

            // Handle file not found error
            return NotFound();
        }
        //<img src="@Url.Action("RetrieveImage", "Modules", new { id = Model.PatientsId })" alt="Patient Image" />


        //list to display appointments of specific patients
        List<PatientApp> Appointments;
        public IActionResult AppList(int PatientId, string PatientName)
        {
            List<PatientApp> Appointments = db.PatientApp.Where(p => p.PatientsId == PatientId).ToList();
            ViewBag.ID = PatientId;
            ViewBag.Name = PatientName;
            return View(Appointments);
        }
        //to display page for to add apointment
        public IActionResult AddApp(int PatId, string Name)
        {
            ViewBag.ID = PatId;
            ViewBag.Name = Name;
            return View();
        }
        //submitted the added appointment and add it in database
        [HttpPost]
        public IActionResult AddAppoint(PatientApp Appointment, String PatName)
        {
            PatientApp newApp = new PatientApp()
            {
                App = Appointment.App,
                AppDate = Appointment.AppDate,
                AppTime = Appointment.AppTime,
                Process = Appointment.Process,
                PatientsId = Appointment.PatientsId
            };
            db.PatientApp.Add(newApp);
            db.SaveChanges();
            TempData["AddMessage"] = "Appointment Added succesfully";
            return RedirectToAction("AppList", "Modules", new { PatientId = Appointment.PatientsId, PatientName = PatName });
        }
        //Delete Patients Appointment 
        public IActionResult OnDeleteApp(int AppId, int PatId, string PatName)
        {
            var App = db.PatientApp.Find(AppId);
            if (App != null)
            {
                db.PatientApp.Remove(App);
                db.SaveChanges();
            }
            return RedirectToAction("AppList", "Modules", new { PatientName = PatName, PatientId = PatId });
        }
        // update patients appointment
        public IActionResult OnUpdateApp(int AppId)
        {
            var Appointment = db.PatientApp.Find(AppId);
            return View(Appointment);
        }
        //updated patients appointment

        [HttpPost]
        public IActionResult OnUpdatedApp(PatientApp UpdatedApp, int AppId)
        {
            int PatId = UpdatedApp.PatientsId;
            var PatName = db.Patients.Find(PatId);
            var existingApp = db.PatientApp.Find(AppId);
            existingApp.AppDate = UpdatedApp.AppDate;
            existingApp.AppTime = UpdatedApp.AppTime;
            existingApp.Process = UpdatedApp.Process;
            db.SaveChanges();
            TempData["UpdateMessage"] = "Appointment Updated successfully ";
            return RedirectToAction("AppList", "Modules", new { PatientId = PatId, PatientName = PatName.Name });

        }

        //Display Patients Payments list

        public IActionResult Payment(int PatientID)
        {
            // List of medical files of a specific patient
            List<PatientPayment> payments = db.PatientPayments
                .Where(pays => pays.PatientsId == PatientID)
                .ToList();

            ViewBag.ID = PatientID;
            return View(payments);
        }
        // to add the payments

        public IActionResult AddPay(int PatId)
        {
            ViewBag.ID = PatId;
            return View();
        }

        [HttpPost]
        public IActionResult AddPays(PatientPayment payment)
        { PatientPayment payment1 = new PatientPayment()
        {
            PatientsId = payment.PatientsId,
            DOB = payment.DOB,
            Amount = payment.Amount
            };
            db.PatientPayments.Add(payment1);
            db.SaveChanges();
            TempData["AddMessage"] = "Payment Added succesfully";
            return RedirectToAction("Payment","Modules",new { PatientId = payment.PatientsId});
        }

    }
}

