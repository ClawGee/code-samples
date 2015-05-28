using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Employer
    {
        public int EmployerId { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string Size { get; set; }
        public string Industry { get; set; }
        public string ContactEmail { get; set; }
        public string URL { get; set; }
        public string Country { get; set; }
        public int StateOptions { get; set; }
        public string OtherLocation { get; set; }
        public string Tech { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
        public string City { get; set; }
        public double? AverageRating { get; set; }
        public int RatingCount { get; set; }
        public Media Media { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
