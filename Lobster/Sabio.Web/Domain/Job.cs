using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Job
    {
        public String Title { get; set; }

        public String Description { get; set; }

        public String CompanyName { get; set; }

        public String Qualifications { get; set; }

        public String Contacts { get; set; }

        public String PrimaryEmail { get; set; }

        public String Url { get; set; }

        public int Id { get; set; }

        public Guid Uid { get; set; }

        public String Rate { get; set; }

        public int EntityType { get; set; }

        public int EntityId { get; set; }

        public String Terms { get; set; }

        public Employer EmployerUser { get; set; }

        public Recruiter RecruiterUser { get; set; }
        //public int Id { get; set; }

        //public int AppUserId { get; set; }

        public Boolean Approved { get; set; }
    }
}