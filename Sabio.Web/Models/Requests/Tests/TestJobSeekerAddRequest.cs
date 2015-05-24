using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Tests
{

    public class TestJobSeekerAddRequest
    {

        [Required]
        public string wantedJobTitleKeywords { get; set; }

        [Required]
        public string wantedLocation { get; set; }


    }
}