using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Projects
{
    public class ProjectCreateRequestModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string name { get; set; }

        [Required]
        [Url]
        public string url { get; set; }

        public string image { get; set; }

        [Required]
        [MinLength(20)]
        [MaxLength(100)]
        public string description { get; set; }

        [Required]
        public DateTime launchdate { get; set; }

        public string technologies { get; set; }
    }
}