using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class JobsDomainModel
    {
        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public int SalaryFillIn { get; set; }

        public int SalaryDropDown { get; set; }

        public int JobLevelRadio { get; set; }

        public string JobSkillsCheckBox { get; set; }

        //wait on this for now; we'll be updating it to take an arry instead of an int or string
        //public int[] JobSkills { get; set; }

        public string LocationStateDropDown { get; set; }

        public string LocationCityFillIn { get; set; }

    }
}