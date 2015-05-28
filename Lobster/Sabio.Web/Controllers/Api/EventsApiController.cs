using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api.Events
{
    [RoutePrefix("api/events")]
    public class EventsApiController : ApiController
    {
        [Route, HttpPost]
        [Authorize]
        public HttpResponseMessage Create(EventsInsertRequest Model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            string currentUserId = UserService.GetCurrentUserId();//aka GetCurrentAppUserID from users table
            //EventsService.Insert calls the service with the "insert" fucntion name.
            Guid NewId = EventsService.Insert(Model, currentUserId);
            ItemResponse<Guid> response = new ItemResponse<Guid>();
            response.Item = NewId;
            return Request.CreateResponse(response);
        }
        [Route("{uid:guid}"), HttpPut]
        [Authorize]
        public HttpResponseMessage Update(EventsUpdateRequest Model, Guid uid)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            string currentUserId = UserService.GetCurrentUserId();//aka GetCurrentAppUserID from users table
            //EventsService.update calls the service with the "update" function name. No var, in front, to hold a response, because nothing gets returned.
            EventsService.Update(Model, uid, currentUserId);
            SuccessResponse response = new SuccessResponse();
            return Request.CreateResponse(response);
        }
        [Route("{uid:guid}"), HttpGet]
        [Authorize]
        public HttpResponseMessage GetEventByUid(Guid uid)
        {
            Event SelectedEvent = EventsService.GetEventByUid(uid);
            ItemResponse<Event> response = new ItemResponse<Event>();
            response.Item = SelectedEvent;
            return Request.CreateResponse(response);
        }
        [Route(""), HttpGet]
        [Authorize]
        public HttpResponseMessage GetEventsByAppUserId([FromUri]int currentPage, int pageSize)
        {
            //service that gets the logged in user , then in event service, there's another serice to turn into AppUserId:
            string currentUserId = UserService.GetCurrentUserId();
            //1.) first, create the request of the events data and put it in a list, with an Event model, name it eventslist
            List<Event> eventsList = EventsService.GetEventsByAppUserId(currentUserId);
            //2.) next, set up the envelope of the PagedList class, to contain the response. It's no longer the below code:
            // ItemsResponse<Event> response = new ItemsResponse<Event>();
            ItemResponse<PagedList<Event>> response = new ItemResponse<PagedList<Event>>();
            //3.) grab the data and set the parameters to be displayed
            PagedList<Event> pagedEventsList = new PagedList<Event>(eventsList, currentPage, pageSize);
            //4.) finally, set the data into the envelope and return it:
            response.Item = pagedEventsList;
            return Request.CreateResponse(response);
        }
    }

}