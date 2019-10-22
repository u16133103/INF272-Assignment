using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication18.Models
{
    public class ForgetPassword
    {
        [Display(Name ="User Email ID")]
        [Required(AllowEmptyStrings =false, ErrorMessage = "User Email ID is required")]
        public string EmailId { get; set; }
    }
}