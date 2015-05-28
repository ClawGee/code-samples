using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class JobsAddRequest
    {
        [Required]
        public String Title { get; set; }

        [Required]
        public String Description { get; set; }

        [Required]
        public String Qualifications { get; set; }

        public String Contacts { get; set; }

        [Required]
        [EmailAddress]
        public String PrimaryEmail { get; set; }

        [Required]
        [Url]
        public String Url { get; set; }

        [Required]
        public String Rate { get; set; }

        public int EntityType { get; set; }

        public int EntityId { get; set; }

        [Required]
        public String Terms { get; set; }

  }
}
