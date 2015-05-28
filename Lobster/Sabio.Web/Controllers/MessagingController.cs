using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sabio.Web.Models.ViewModels;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("messaging")]
    public class MessagingController : BaseController
    {
        [Route(""), HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        [Route("conversation/{conversationId:int}")]
        public ActionResult Conversation(int conversationId)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = conversationId;
            return View("ConversationIndex", model);
        }
    }
}