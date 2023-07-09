using V._3._0.App_Data;
using V._3._0.Interfaces;
using V._3._0.Models;

namespace V._3._0.Methods
{
    public class PatientData : IPatients
    {
        public readonly HospitalData db;
        private List<Patients> patients;
        public PatientData(HospitalData db)
        {
            this.db = db;
            patients = db.Patients.ToList();
        }
        public List<Patients> GetPatients()
        {
            //patients = db.Patients.ToList();
            return (patients);
        }
        public Patients GetByID(int id)
        {
            return patients.SingleOrDefault(p => p.Id == id);
        }
        public Patients Add(Patients newpatients)
        {
            patients.Add(newpatients);
            newpatients.Id = patients.Max(p => p.Id) + 1;
            return newpatients;
        }
        public Patients update(Patients PatientUpdated)
        {
            var patient = patients.SingleOrDefault(p => p.Id == PatientUpdated.Id);
            if (patient != null)
            { 
                patient.Name = PatientUpdated.Name;
                patient.Status = PatientUpdated.Status;
                patient.Roomno = PatientUpdated.Roomno;
                patient.DateOfAdm = PatientUpdated.DateOfAdm;
                patient.DateOfDis = PatientUpdated.DateOfDis;
            }
            db.Patients.Update(patient);
            db.SaveChanges();
            return patient;
        }
        public int commit()
        {
            return 0;
        }
        public Patients Delete(int id)
        {
            var patient = patients.FirstOrDefault(p => p.Id == id);
            if (patient != null)
            { patients.Remove(patient); }
            return patient;
        }

        public IEnumerable<Patients> GetPatientByName(string? name)
        {
            return from p in patients
                   where (string.IsNullOrEmpty(name) || p.Name.StartsWith(name))
                   orderby p.Name
                   select p;
        }

        public int GetCountOfPatients()
        {
            return patients.Count();
        }
    }
}
