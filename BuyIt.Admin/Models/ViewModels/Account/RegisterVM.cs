using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyIt.Admin.Models.ViewModels.Account
{
    public class RegisterVM
    {
        [Required, Display(Name = "Email Address"), EmailAddress]
        [Remote(action: "IsUserAvailable", controller: "Account")]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        [Required, DataType(DataType.Password), MinLength(6), MaxLength(20)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), MinLength(6), MaxLength(20), Compare("Password", ErrorMessage = "Password does not match!!!"), Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
