using Sabio.Web.Domain;
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
    [Authorize]
    [RoutePrefix("api/admin/jobs")]
    public class JobsAdminApiController : ApiController
    {
        [Route("pending"), HttpGet]
        public HttpResponseMessage PendingJobs()
        {
                ItemsResponse<Job> response = new ItemsResponse<Job>();
                response.Items = JobsService.GetJobsByStatus(false);
                return Request.CreateResponse(response);
        }
        [Route("approved"), HttpGet]
        public HttpResponseMessage ApprovedJobs()
        {
            ItemsResponse<Job> response = new ItemsResponse<Job>();
            response.Items = JobsService.GetJobsByStatus(true);
            return Request.CreateResponse(response);
        }
    }
}