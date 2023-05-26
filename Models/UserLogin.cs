using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace V._3._0.Models
{
    public class UserLogin
    {
        [BindProperty]
        [Required]
        public string Emailid { get; set; }
        [Required]
        public string Password { get; set; }
    }
}