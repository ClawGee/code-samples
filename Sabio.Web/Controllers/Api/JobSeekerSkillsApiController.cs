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
    [RoutePrefix ("api/jobseekerskills")]
    public class JobSeekerSkillsApiController : ApiController
    {
        //authorize checks whether or not it is working
        [Authorize]
        [Route("create"), HttpPost]
        public HttpResponseMessage EchoJobSeekerSkillsCreate(JobSeekerSkillsAddRequestModel model)
        {
            if(!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string currentUserId = UserService.GetCurrentUserId();


            ItemResponse<Guid> response = new ItemResponse<Guid>();
            Guid newId = JobSeekerSkillsService.JobSeekerSkillsInsert(model, currentUserId);

            response.Item = newId;

            return Request.CreateResponse(response);

        }

        [Authorize]
        [Route("view"), HttpGet]
        public HttpResponseMessage EchoJobSeekerSkillsSelect()
        {

            string currentUserId = UserService.GetCurrentUserId();
            ItemResponse<JobSeekerSkillsDomainModel> response = new ItemResponse<JobSeekerSkillsDomainModel>();

            JobSeekerSkillsDomainModel item = JobSeekerSkillsService.JobSeekerSkillsSelect(currentUserId);

            response.Item = item;

            return Request.CreateResponse(response);
        }


    }
}
