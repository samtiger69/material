using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class RegisterModel
    {
        [Display(Name = "Username", ResourceType = typeof(Resource))]
        public string Username { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resource))]
        public string Password { get; set; }

        [Display(Name = "PasswordConfirm", ResourceType = typeof(Resource))]
        public string ConfirmPassword { get; set; }
    }
}