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
    [RoutePrefix("api/jobs")]
    [Authorize]
    public class JobsApiController : ApiController
    {
        [Route(""), HttpPost]
        public HttpResponseMessage Create(JobsAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string currentUserId = UserService.GetCurrentUserId();
            //string currentUserId = "ca42a26b-78c5-46ad-94cb-2cdd22260d58";

            Guid NewJobId = JobsService.InsertJobs(model, currentUserId);
            
            ItemResponse<Guid> response = new ItemResponse<Guid>();
            response.Item = NewJobId;
            //UserService.UpdateUser(Model, NewUser.Id);
            return Request.CreateResponse(response);
        }

        [Route("{JobGuid:guid}"), HttpPut]
        [Authorize]
        public HttpResponseMessage Update(JobsAddRequest model, Guid jobGuid)
        {
            //string currentUserId = UserService.GetCurrentUserId();
            //string currentUserId = "ca42a26b-78c5-46ad-94cb-2cdd22260d58";
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            JobsService.UpdateJobs(model, jobGuid);

            ItemResponse<Guid> response = new ItemResponse<Guid>();
            response.Item = jobGuid;
            return Request.CreateResponse(response);
        }

        [Route("{jobGuid:guid}"), HttpGet]
        [Authorize]
        public HttpResponseMessage GetJobByUid(Guid jobGuid)
        {
            Job SelectedJob = JobsService.GetJobByUid(jobGuid);
            ItemResponse<Job> response = new ItemResponse<Job>();
            response.Item = SelectedJob;
            return Request.CreateResponse(response);
        }

        [Route(""), HttpGet]
        [Authorize]
        public HttpResponseMessage GetJobsByAppUserId()
        {
            string currentUserId = UserService.GetCurrentUserId();
            List<Job> JobsList = JobsService.GetJobsByAppUserId(currentUserId);

            ItemsResponse<Job> response = new ItemsResponse<Job>();
            response.Items = JobsList;
            return Request.CreateResponse(response);
         }
            
    }
}
