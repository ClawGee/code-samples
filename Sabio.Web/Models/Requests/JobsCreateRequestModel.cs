using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class JobsCreateRequestModel
    {

        [Required]
        [MaxLength(32)]
        public string JobTitle { get; set; }

        [Required]
        [MaxLength(32)]
        public string JobDescription { get; set; }

        [Required]
        public int SalaryFillIn { get; set; }

        [Required]
        public int SalaryDropDown { get; set; }

        [Required]
        public int JobLevelRadio { get; set; }

        [Required]
        [MaxLength(32)]
        public string JobSkillsCheckBox { get; set; }

        //wait on this for now; we'll be updating it to take an arry instead of an int or string
        //[Required]
        //[MinLength(1)]
        //public int[] JobSkills { get; set; }

        [Required]
        [MaxLength(32)]
        public string locationStateDropDown { get; set; }

        [Required]
        [MaxLength(32)]
        public string locationCityFillIn { get; set; }

    }
}