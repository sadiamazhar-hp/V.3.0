using V._3._0.Models;

namespace V._3._0.Interfaces
{
    public interface IPatients
    {
        IEnumerable<Patients> GetPatientByName(string? name);
        public Patients GetByID(int id);
        public Patients update(Patients PatientUpdated);
        int commit();
        int GetCountOfPatients();
        public Patients Add(Patients newpatients);
        public Patients Delete(int id);
    }
}
