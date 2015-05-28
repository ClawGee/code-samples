using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class UpdateRecruiterRequest
    {
        [Required]
        [MinLength(2)]
        public string specialty { get; set; }

        [Required]
        [MinLength(2)]
        public string location { get; set; }

        public string url { get; set; }

        [Required]
        [MinLength(10)]
        public string phoneNumber1 { get; set; }

        public string phoneNumber2 { get; set; }

        public string companyLogo { get; set; }

        public string summary { get; set; }

        public string twitterAccount { get; set; }

        public string facebookAccount { get; set; }

        public string linkedInAccount { get; set; }

        public string googlePlusAccount { get; set; }

        private object appUserId { get; set; }

        public object uid { get; set; }
    }
}