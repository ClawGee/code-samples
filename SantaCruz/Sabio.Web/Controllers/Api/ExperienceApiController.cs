using Sabio.Web.Domain;
using Sabio.Web.Models;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/experiences")]
    public class ExperienceApiController : ApiController
    {
        [Route("validate"), HttpPost]
        public HttpResponseMessage EchoExperienceValidation(ExperienceRequestModel model)
        {
            //This method has this route:
            //  api/experience/validate
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                //BadRequest returns a 400 Error message
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            return Request.CreateResponse(model);
        }


        [Authorize]
        [Route, HttpPost]
        public HttpResponseMessage PostExperience(ExperienceRequestModel model)
        {

            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                //BadRequest returns a 400 Error message
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string currentUserId = null;
            currentUserId = UserService.GetCurrentUserId();

            ItemResponse<Guid> response = new ItemResponse<Guid>();

            Guid newExperience = ExperienceService.Post(model, currentUserId);

            response.Item = newExperience;

            return Request.CreateResponse(response);
        }


        [Authorize]
        [Route("user"), HttpGet]
        public HttpResponseMessage GetExperiences()
        {
            //controller level steps
            //1) get current user id from userservice
            //2) create a new list variable List<ExperienceDomainModel> to hold the output (ie newExperience)
            //3) call GetExperienceByUserGuid
            //4) create a new envelope with ItemsResponse<ExperienceDomainModel>
            //5) set response.Items = new list variable
            //6) return output model

            string currentUserId = UserService.GetCurrentUserId();

            List<ExperienceDomainModel> newExperience = ExperienceService.GetExperiencesByUserGuid(currentUserId);

            ItemsResponse<ExperienceDomainModel> response = new ItemsResponse<ExperienceDomainModel>();

            response.Items = newExperience;

            return Request.CreateResponse(response);
        }


        [Authorize]
        [Route("{uid:guid}"), HttpGet]
        public HttpResponseMessage GetExperience(Guid uid)
        {
            string currentUserId = UserService.GetCurrentUserId();
            ExperienceDomainModel newExperience = ExperienceService.Get(uid);

            ItemResponse<ExperienceDomainModel> response = new ItemResponse<ExperienceDomainModel>();

            //ExperienceDomainModel newExperience = ExperienceService.Get(uid);
            response.Item = newExperience;
   
            return Request.CreateResponse(response);

        }


        [Route("{uid:guid}"), HttpPut]
        public HttpResponseMessage PutExperience(Guid uid, ExperienceRequestModel model)
        {

            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                //BadRequest returns a 400 Error message
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }



            ItemResponse<Guid> response = new ItemResponse<Guid>();

           ExperienceService.Put(model,uid);


            return Request.CreateResponse(response);
        }
    
        
    
    }

}
