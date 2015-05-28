using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Domain
{
    public class Rating
    {
        // GET: RatingReview
        public int RatingValue {get; set;}

        public string Comments { get; set; }

        public int EntityType { get; set; }

        public int EntityId { get; set; }

        public Guid? Uid { get; set; }

        public DateTime CreateDate { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }

        //included FullUser Domain Model via "Decorator Pattern", to access Full User FirstNames/LastNames
        public FullUser FullUser { get; set; }
    }
}