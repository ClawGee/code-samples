using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class DeveloperRequirementsAddRequest
    {
        [Required]
        public string[] WantedJobTitleKeywords { get; set; }

        [Required]
        public string[] WantedLocation { get; set; }

        public int[] WorkType { get; set; }

        public int[] Skills { get; set; }

        public int[] CompanyType { get; set; }

        public string SalaryRange { get; set; }

        public string JobDistance { get; set; }

        public string ZipCode { get; set; }

        public int[] Languages { get; set; }
    }
}