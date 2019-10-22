using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebApplication18.Models;

namespace WebApplication18.Models
{
    public class CompanyRegistration
    {
        

        [Required(ErrorMessage = "Enter username")]

        public string Username { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter RePassword")]
        [DataType(DataType.Password)]

        [Compare("Password")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Enter Company name")]
        public string CompanyName { get; set; }

        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
            ErrorMessage = "Please provide valid email id")]
        public string Email { get; set; }
        public static object Models { get; internal set; }

    }
}