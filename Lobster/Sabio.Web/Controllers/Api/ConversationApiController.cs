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
    [RoutePrefix("api/conversation")]

    public class ConversationApiController : ApiController
    {
        [Authorize]
        [Route(""), HttpGet]
        public HttpResponseMessage GetConversations()
        {
            string loggedInUser = UserService.GetCurrentUserId();
            int AppUserId = UserService.ConvertGuid(loggedInUser);
            List<Conversation> conversationGroup = MessageService.GetConversationsByUserId(AppUserId);
            ItemsResponse<Conversation> ConversationResponse = new ItemsResponse<Conversation>();
            ConversationResponse.Items = conversationGroup;
            return Request.CreateResponse(ConversationResponse);
        }

        [Authorize]
        [Route("received"), HttpGet]
        public HttpResponseMessage GetConversationsReceived()
        {
            string loggedInUser = UserService.GetCurrentUserId();
            int AppUserId = UserService.ConvertGuid(loggedInUser);
            List<Conversation> conversationGroup = MessageService.GetConversationsByRecipientId(AppUserId);
            ItemsResponse<Conversation> ConversationResponse = new ItemsResponse<Conversation>();
            ConversationResponse.Items = conversationGroup;
            return Request.CreateResponse(ConversationResponse);
        }

        [Authorize]
        [Route("messages/{ConversationId:int}"), HttpGet]
        public HttpResponseMessage GetConversationMessages(int ConversationId)
        {
            List<Message> currentConversation = MessageService.GetConversationThread(ConversationId);
            ItemsResponse<Message> MessageResponse = new ItemsResponse<Message>();
            MessageResponse.Items = currentConversation;
            return Request.CreateResponse(MessageResponse);
        }

        [Authorize]
        [Route("messages/meta/{ConversationId:int}"), HttpGet]
        public HttpResponseMessage GetConvoMetaData(int ConversationId)
        {
            ConvoMetaData metaData = MessageService.GetConvoMetaDataById(ConversationId);
            ItemResponse<ConvoMetaData> metaDataResponse = new ItemResponse<ConvoMetaData>();
            metaDataResponse.Item = metaData;
            return Request.CreateResponse(metaDataResponse);
        }
    }
}