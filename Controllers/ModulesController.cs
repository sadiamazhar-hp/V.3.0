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
                    return RedirectToAction("PersonalInfoView", "Modules", new { PersonalInfoId = personalInfoId });
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
            return RedirectToAction("Patients", "Modules");


        }
        //Display form of PersonalInfo for every specific patient
        public IActionResult PersonalInfo(int patientId, string patientName)
        {
            var personalInfo = new Models.PersonalInfo { PatId = patientId, Name = patientName };
            return View(personalInfo);
        }
        //Display PersonalInfo of specific patients
        public IActionResult PersonalInfoView(int PersonalInfoId)
        {
            var PatPersonalInfo = db.PersonalInfo.Find(PersonalInfoId);
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
        /*public IEnumerable<Patients> GetByName()
        {
            string name = Request.Form["Searchninput"];
            return patients.Where(p => string.IsNullOrEmpty(name) || p.Name.StartsWith(name))
                           .OrderBy(p => p.Name);
        }*/


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
        [HttpPost]
        //Delete MedicalFile using MedId
        public IActionResult OnDeleteMedfile(int medid, int patid)
        {
            var medfile = db.MedicalInfo.Find(medid);
            db.MedicalInfo.Remove(medfile);
            db.SaveChanges();
            return RedirectToAction("MedFiles", "Modules", new { patientId = patid});
        }
        [HttpPost]
        //update patient using ID
        public IActionResult OnUpdate(int PatientId) 
        {
            var patient = db.Patients.Find(PatientId);
            return View(patient);
        }

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
        public IActionResult MedFiles(int patientId, string patientName)
        {
            //list of medical files of specific patient
            List<MedicalInfo> medfiles = db.MedicalInfo.Where(p => p.PatientsId == patientId).ToList();
            ViewBag.ID = patientId;
            return View(medfiles);
        }

        //For medicalinfo of the patient to upload
        public IActionResult PatientMed(int patientId, string patientName)
        {
            
            MedicalInfo file = new MedicalInfo() { PatientsId = patientId };
            ViewBag.Name = patientName;
            return View(file);
        }

        [HttpPost]
        public IActionResult PatientMed(MedicalInfo file ,IFormFile ImageFile)
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
                    ImageFileName = originalFileName ,
                    ImageFile = imageBytes
                };
                db.MedicalInfo.Add(File);
                db.SaveChanges();
                return View("~/Views/Modules/file.cshtml", file);
            }
            return View();
            
        }
        public IActionResult RetrieveImage(int id)
        {
            MedicalInfo file = db.MedicalInfo.Find(id);

            if (file != null)
            {
                return File(file.ImageFile, "image/jpeg"); // Adjust the content type based on the image format stored in the database
            }

            // Handle file not found error
            return NotFound();
        }
        //<img src="@Url.Action("RetrieveImage", "Modules", new { id = Model.PatientsId })" alt="Patient Image" />
       
    }
}

