using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Resume
    {

        public int Id { get; set; }

        //public int AppUserId { get; set; }

        public string Inst1Name { get; set; }

        public string Inst1Location { get; set; }

        public string Inst1DateFrom { get; set; }

        public string Inst1DateTo { get; set; }

        public string Inst1Degree { get; set; }

        public string Inst1DegreeMajor { get; set; }

        public string Inst1DegreeMinor { get; set; }

        public decimal? Inst1DegreeGpa { get; set; }

        public string Inst2Name { get; set; }

        public string Inst2Location { get; set; }

        public string Inst2DateFrom { get; set; }

        public string Inst2DateTo { get; set; }

        public string Inst2Degree { get; set; }

        public string Inst2DegreeMajor { get; set; }

        public string Inst2DegreeMinor { get; set; }

        public decimal? Inst2DegreeGpa { get; set; }

        public string Inst3Name { get; set; }

        public string Inst3Location { get; set; }

        public string Inst3DateFrom { get; set; }

        public string Inst3DateTo { get; set; }

        public string Inst3Degree { get; set; }

        public string Inst3DegreeMajor { get; set; }

        public string Inst3DegreeMinor { get; set; }

        public decimal? Inst3DegreeGpa { get; set; }

        public string Job1EmployerName { get; set; }

        public string Job1Department { get; set; }

        public string Job1EmployerLocation { get; set; }

        public string Job1EmployerDescr { get; set; }

        public string Job1Position { get; set; }

        public string Job1DateFrom { get; set; }

        public string Job1DateTo { get; set; }

        public string Job1Description { get; set; }

        public string Job2EmployerName { get; set; }

        public string Job2Department { get; set; }

        public string Job2EmployerLocation { get; set; }

        public string Job2EmployerDescr { get; set; }

        public string Job2Position { get; set; }

        public string Job2DateFrom { get; set; }

        public string Job2DateTo { get; set; }

        public string Job2Description { get; set; }

        public string Job3EmployerName { get; set; }

        public string Job3Department { get; set; }

        public string Job3EmployerLocation { get; set; }

        public string Job3EmployerDescr { get; set; }

        public string Job3Position { get; set; }

        public string Job3DateFrom { get; set; }

        public string Job3DateTo { get; set; }

        public string Job3Description { get; set; }

        public string ObjectiveStmnt { get; set; }

        public string VolunteerExperience { get; set; }

        public string Hobbies { get; set; }

        public string FrontEndSkills { get; set; }

        public string BackEndSkills { get; set; }

        public string AdditionalSkills { get; set; }

        public string Certifications { get; set; }

        public Guid UId { get; set; }
    }
}