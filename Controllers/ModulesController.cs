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
                return RedirectToAction("PatientMed", "Modules", new { patientId = value, patientName = PatientName });
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
        //For medicalinfo of the patient
        public IActionResult PatientMed(int patientId, string patientName)
        {
            var medicalInfo = new Models.MedicalInfo { PatientsId = patientId};
            return View(medicalInfo);
        }

        [HttpPost]
        public IActionResult PatientMed(IFormFile ImageFile)
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
                MedicalInfo File = new MedicalInfo()
                {
                    ImageFileName = ImageFile.Name,
                    ImageFile = imageBytes
                };
                db.MedicalInfo.Add(File);
                db.SaveChanges();
                return View("~/Views/Modules/file.cshtml", ImageFile);
            }
            return View();



            /*if (file != null && file.FilePath != null)
            {
                // Process the uploaded file
                string fileName = Path.GetFileName(file.FilePath);
                string subfolderPath = Path.Combine(webHostEnvironment.ContentRootPath, "App_Data", "MedInfoFIles");
                string filePath = Path.Combine(subfolderPath, fileName);
                file.FilePath = filePath; // Update the file path

                // Save the file or perform any other required operations

                // Redirect to a success page or perform further actions
                return RedirectToAction("UploadSuccess");
            }

            // If no file was uploaded or an error occurred, return the same view with an error message
            //ModelState.AddModelError(string.Empty, "Please select a file to upload.");
            return View("~/Views/Modules/file.cshtml",file);*/
        }
    }
}

