using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/message")]

    public class MessageApiController : ApiController
    {
        [Authorize]
        [Route(""), HttpPost]
        public HttpResponseMessage Create(MessageCreateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            string loggedInUser = UserService.GetCurrentUserId();
            ConversationMessagePair newMessageIdentities = MessageService.InsertMessage(model, loggedInUser);

            ItemResponse<ConversationMessagePair> messageResponse = new ItemResponse<ConversationMessagePair>();

            messageResponse.Item = newMessageIdentities;
            return Request.CreateResponse(messageResponse);
        }

        [Authorize]
        [Route("reply"), HttpPost]
        public HttpResponseMessage Create(ReplyCreateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            string loggedInUser = UserService.GetCurrentUserId();
            Message NewMessageId = MessageService.InsertReply(model, loggedInUser);

            ItemResponse<Message> MessageResponse = new ItemResponse<Message>();
            MessageResponse.Item = NewMessageId;
            return Request.CreateResponse(MessageResponse);
        }
    }
}