using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class ExperienceRequestModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string CompanyName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string Title { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string CompanyCity { get; set; }

        [Required]
        public int CompanyState { get; set; }

        [Required]
        public Int16 StartDateMonth { get; set; }

        [Required]
        public Int16 StartDateYear { get; set; }

        //[Required]
        //public string CurrentEmployer { get; set; }

        [Required]
        public Int16 EndDateMonth { get; set; }

        [Required]
        public Int16 EndDateYear { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Description { get; set; }

        //[Required]
        //public string UploadFile { get; set; }

    }
}

