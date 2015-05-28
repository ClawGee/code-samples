using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class EmployerSearchResults
    {
        public int EmployerId { get; set; }
        public string EmployerName { get; set; }
        public string EmployerContactName { get; set; }
        public string EmployerIndustry { get; set; }
        public string EmployerContactEmail { get; set; }
        public string EmployerURL { get; set; }
        public string EmployerOtherLocation { get; set; }
        public string EmployerTech { get; set; }
        public string EmployerDescription { get; set; }
        public double? AverageRating { get; set; }
        public int RatingCount { get; set; }
        public string Uid { get; set; }
        public Media EmployerMedia { get; set; }
        public List<Tag> EmployerTags { get; set; }
    }
}
