using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleWebApi.Models
{
    public class LoginUserBindingModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}