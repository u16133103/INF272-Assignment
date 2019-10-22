using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication18.Models
{
    public class JobSProfile
    {
        public string FirstName { get; set; }
        public string LaststName { get; set; }
        public string CellNumber { get; set; }

        public string Email { get; set; }

        public string Qualifcation { get; set; }

        public string Skills { get; set; }
        //public string ImagePath { get; set; }
        [DisplayName("Upload CV")]
        public HttpPostedFileBase ImagePath { get; set; }
    }
}