using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication18.Models
{
    public class JSRegistration
    {
        [Required(ErrorMessage = "Enter Firstame")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter LastName")]
        public string LaststName { get; set; }

        [Required(ErrorMessage = "Enter LastName")]
        public string CellNumber { get; set; }

        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
            ErrorMessage = "Please provide valid email id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter RePassword")]
        [DataType(DataType.Password)]

        [Compare("Password")]
        public string RePassword { get; set; }

        

        
    }
}