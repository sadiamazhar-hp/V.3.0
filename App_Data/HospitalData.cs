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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            // Configure the one-to-one relationship
         modelBuilder.Entity<Patients>()
            .HasOne(s => s.PersonalInfo)
            .WithOne(a => a.Patients)
            .HasForeignKey<PersonalInfo>(p => p.PatId)
            .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
