using Microsoft.EntityFrameworkCore;
using V._3._0.Models;

namespace V._3._0.App_Data
{
    public class HospitalData : DbContext
    {
        public HospitalData(DbContextOptions<HospitalData> options)
            : base(options) { }
        public DbSet<SignUp> HosUser { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<PersonalInfo> PersonalInfo { get; set; }

        public DbSet<MedicalInfo> MedicalInfo{ get; set; }
        public DbSet<PatientApp> PatientApp { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Configure the one-to-one relationship
             for patients and its personalinfo*/
             modelBuilder.Entity<Patients>()
            .HasOne(s => s.PersonalInfo)
            .WithOne(a => a.Patients)
            .HasForeignKey<PersonalInfo>(p => p.PatId)
            .OnDelete(DeleteBehavior.Cascade);

            /*Configure the one-to-many relationship
              for patients and its MedicalInfo*/
            modelBuilder.Entity<Patients>()
            .HasMany(m => m.MedicalInfo)
            .WithOne(p => p.Patients)
            .OnDelete(DeleteBehavior.Cascade);

            /*Configure the one-to-many relationship
              for patients and its Appointments*/
            modelBuilder.Entity<Patients>()
            .HasMany(m => m.PatientApp)
            .WithOne(p => p.Patients)
            .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
