using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Developer
    {
        public Guid UId { get; set;}

        public int EducationLevel { get; set;}

        public int YearsOfExperience {get; set;}

        public bool FrontEnd {get; set;} 

        public bool BackEnd {get; set;} 

        public bool Network {get; set;} 

        public string Certifications {get; set;}

        public int Id { get; set;}


    }
}