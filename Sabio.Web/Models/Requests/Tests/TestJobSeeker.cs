using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Tests
{
    public class TestJobSeeker
    {
        [Required]
        public string projectName { get; set; }

        [Required]
        public string projectUrl { get; set; }

        public string projectImageLink { get; set; }

        [Required]
        public string projectDescription { get; set; }

        [Required]
        public string dateLaunched { get; set; }


    }
}