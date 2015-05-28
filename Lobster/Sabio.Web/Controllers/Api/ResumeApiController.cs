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
    [RoutePrefix("api/resume")]

    public class ResumeApiController : ApiController
    {
        [Authorize]
        [Route(""), HttpPost]
        public HttpResponseMessage CreateResume(ResumeRequest Model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            string LoggedInUser = UserService.GetCurrentUserId();
            Guid NewResumeId = ResumeService.InsertResume(Model, LoggedInUser);

            ItemResponse<Guid> ResumeResponse = new ItemResponse<Guid>();
            ResumeResponse.Item = NewResumeId;
            return Request.CreateResponse(ResumeResponse);
        }

        [Authorize]
        [Route(""), HttpPut]
        public HttpResponseMessage EditResume(ResumeRequest Model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            string LoggedInUser = UserService.GetCurrentUserId();
            ResumeService.UpdateResume(Model, LoggedInUser);

            ItemResponse<bool> ResumeResponse = new ItemResponse<bool>();
            ResumeResponse.Item = true;
            return Request.CreateResponse(ResumeResponse);
        }

        [Authorize]
        [Route(""), HttpGet]
        public HttpResponseMessage SelectResume()
        {
            string LoggedInUser = UserService.GetCurrentUserId();
            Resume CurrentUserResume = ResumeService.GetResume(LoggedInUser);
            ItemResponse<Resume> ResumeResponse = new ItemResponse<Resume>();
            ResumeResponse.Item = CurrentUserResume;
            return Request.CreateResponse(ResumeResponse);
        }

        [Authorize]
        [Route("full"), HttpGet]
        public HttpResponseMessage SelectFullResume()
        {
            string LoggedInUser = UserService.GetCurrentUserId();
            FullResume CurrentUserFullResume = ResumeService.GetFullResume(LoggedInUser);
            ItemResponse<FullResume> ResumeResponse = new ItemResponse<FullResume>();
            ResumeResponse.Item = CurrentUserFullResume;
            return Request.CreateResponse(ResumeResponse);
        }
    }
}