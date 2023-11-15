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
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive value")]
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
        public virtual ICollection<PatientApp> PatientApp { get; set; }

        public virtual ICollection<PatientPayment> PatientPayments { get; set; }
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
    public class PatientApp {
        [BindProperty]
        [Key]
        public int App { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime AppDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AppTime { get; set; }

        public string Process { get; set; }
        public int PatientsId { get; set; } // Convention-based foreign key
        public virtual Patients Patients { get; set; }


    } 
    public class PatientPayment
    {
        [BindProperty]
        [Key]
        public int PayId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Required]
        public int Amount { get; set; }
        public int PatientsId { get; set; } // Convention-based foreign key
        public virtual Patients Patients { get; set; }

        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
    }

    public class PaymentDetail
    {
        [BindProperty]
        [Key]
        public int Sno { get; set; }

        [Required]
        public string procedure { get; set; }
        [Required]
        public int Amount { get; set; }

        public int PaymentID { get; set; } // convention based foreign key

        public virtual PatientPayment PatientPayment { get; set; }

    }

}
