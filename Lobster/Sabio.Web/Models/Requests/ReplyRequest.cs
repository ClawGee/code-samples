using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class ReplyCreateRequest
    {
        //[Required]
        //public string SenderId { get; set; }

        //[Required]
        //public string RecipientId { get; set; }

        [MaxLength(128)]
        public string Subject { get; set; }

        [MaxLength(1000)]
        public string Body { get; set; }

        [Required]
        public int ConversationId { get; set; }
    }

}