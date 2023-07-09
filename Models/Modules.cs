using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Globalization;
using Azure.Core;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace V._3._0.Models
{
    public enum PatientStatus
    {
        Admitted,
        Discharged
    }
    public enum Gender
    {
        Male,Female, Other
    }
    public class Patients
    {
        [BindProperty]
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public PatientStatus Status { get; set; }
        public int Roomno { get; set; } 
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime DateOfAdm { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfDis { get; set; }

        public virtual PersonalInfo PersonalInfo { get; set; }
        public virtual ICollection<MedicalInfo> MedicalInfo { get; set; }
    }
    public class PersonalInfo
    {
        [BindProperty]
        [ForeignKey("Patients")]
        public int PatId { get; set; }
        [Key]
        public int PIId { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Contact { get; set; }
        
        public virtual Patients Patients { get; set; }
    }
    public class MedicalInfo
    {
        [BindProperty]
        
        [Key]
        public int MedId { get; set; }
        
        public byte[] ImageFile { get; set; }
        public string ImageFileName { get; set; }
        public int PatientsId { get; set; } // Convention-based foreign key
        public virtual Patients Patients { get; set; }
    }
    public class DoctorApp {
        [BindProperty]
        public DateTime AppDate { get; set; }
        
    } 

}
