using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Recruiter
    {
        public string Specialty { get; set; }

        public string Location { get; set; }

        public string URL { get; set; }

        public string PhoneNumber1 { get; set; }

        public string PhoneNumber2 { get; set; }

        public string CompanyLogo { get; set; }

        public string Summary { get; set; }

        public string TwitterAccount { get; set; }

        public string FacebookAccount { get; set; }

        public string LinkedInAccount { get; set; }

        public string GooglePlusAccount { get; set; }

        public Guid Uid { get; set; }

        public int Id { get; set; }

        public double? AverageRating { get; set; }

        public int RatingCount { get; set; }

        public Media RecruiterPhoto { get; set; }

        public FullUser RecruiterUser { get; set; }
    }
}