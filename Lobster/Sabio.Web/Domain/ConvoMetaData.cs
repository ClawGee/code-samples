using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class ConvoMetaData
    {
        public int ConversationId { get; set; }

        public DateTime CreateDate { get; set; }

        public int AppUserId { get; set; }

        public int RecipientId { get; set; }

        public FullUser Recipient { get; set; }

        public FullUser Sender { get; set; }

        //public int RecipientId { get; set; }

        //public string RecipientEmail { get; set; }

        //public string RecipientPhoneNumber { get; set; }

        //public string RecipientUserName { get; set; }

        //public string RecipientLastName { get; set; }

        //public string RecipientFirstName { get; set; }

        //public int RecipientUserType { get; set; }

        //public int RecipientMediaId { get; set; }

        //public string SenderEmail { get; set; }

        //public string SenderPhoneNumber{ get; set; }

        //public string SenderUserName { get; set; }

        //public string SenderLastName { get; set; }
        
        //public string SenderFirstName { get; set; }

        //public int SenderUserType { get; set; }

        //public int SenderMediaId { get; set; }
    }
}