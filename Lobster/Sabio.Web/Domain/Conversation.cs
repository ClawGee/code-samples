using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Conversation
    {
        public int Id { get; set; }

        //public string Subject { get; set; }

        public int RecipientId { get; set; }

        public DateTime CreateDate { get; set; }

        public int AppUserId { get; set; }

    }
}