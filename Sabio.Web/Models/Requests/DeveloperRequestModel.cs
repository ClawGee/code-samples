using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class DeveloperRequestModel
    {

        [Required]
        public int EducationLevel { get; set;}

        [Required]
        public int YearsOfExperience { get; set; }

        public bool FrontEnd { get; set; }

        public bool BackEnd { get; set; }

        public bool Network { get; set; }

        public string Certifications { get; set; }

    }
}