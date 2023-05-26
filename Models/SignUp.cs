using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace V._3._0.Models
{
    public class SignUp
    {
        [BindProperty]
        public int Id { get; set; }
        [Required]
        public string HospitalName { get; set; }
        [Required]
        public string location { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
      /*[Required]
        public int Package { get; set; }
        [Required]
        public int CardType { get; set; }
        [Required]
        public string CardName { get; set; }
        [Required]
        public int Exp { get; set; }
        [Required]
        public int Cvc { get; set; }*/
    }
}
