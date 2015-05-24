using Sabio.Web.Domain;
using Sabio.Web.Models;
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

    [RoutePrefix("api/jobs")]
    public class JobsApiController : ApiController
    {

        
        [Route("validate"), HttpPost]
        public HttpResponseMessage EchoJobsValidation(JobsCreateRequestModel model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                //request is also part of MVC, so is ModelState and IsValid, httpstatuscode.badrequest is saying "400 error"
                // modelstate is a helper object with info about the model state.
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }

            //return model to the REST client with all the stuff in it.
            return Request.CreateResponse(model);
        }

        

        [Route("create"), HttpPost]
        [Authorize]
        public HttpResponseMessage JobsCreate(JobsCreateRequestModel model)
        {

            Guid newJob = Guid.Empty;

            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }


            string currentUserId = null;
            currentUserId = UserService.GetCurrentUserId();
            
            ItemResponse<Guid> response = new ItemResponse<Guid>();
            //SC* new code below
            newJob = JobsService.JobCreate(model, currentUserId);
            response.Item = newJob;
            
            
            return Request.CreateResponse(response);
           
        }



        [Route("view"), HttpGet]
        [Authorize]
        public HttpResponseMessage JobsView()
        {

            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }


            ItemsResponse<JobsDomainModel> response = new ItemsResponse<JobsDomainModel>();

            string userIdGuid = UserService.GetCurrentUserId();
            int appUserId = UserService.GetAppUserId(userIdGuid);

            response.Items = JobsService.Select(appUserId);

            return Request.CreateResponse(response);

        }



        [Route("view/{uid:guid}"), HttpGet]
        public HttpResponseMessage JobsView(Guid Uid)
        {

            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }


            ItemResponse<JobsDomainModel> response = new ItemResponse<JobsDomainModel>();

            //newJob = JobsService.JobCreate(model, currentUserId);
            response.Item = JobsService.Select(Uid);

            return Request.CreateResponse(response);
           
        }





    }
}



