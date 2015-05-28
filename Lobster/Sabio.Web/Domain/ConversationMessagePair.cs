using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class ConversationMessagePair
    {
        public int ConversationId { get; set; }

        public int MessageId { get; set; }
    }
}