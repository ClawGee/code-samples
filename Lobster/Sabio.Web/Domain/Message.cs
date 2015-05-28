using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Message
    {
        public int MessageId { get; set; }

        public int ConversationId { get; set; }
        
        public DateTime CreateDate { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool Deleted { get; set; }

        public int AppUserId { get; set; }

        public int RecipientId { get; set; }
    }
}