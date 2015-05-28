using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class RatingRequest
    {
        public int RatingValue { get; set; }

        public string Comments { get; set; }

        public int EntityType { get; set; }

        public int EntityId { get; set; }
    }

}