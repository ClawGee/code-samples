using Sabio.Web.Models.Requests.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api.Tests
{
    [RoutePrefix("api/tests/testjobseeker")]
    public class TestJobSeekerApiController : ApiController
    {

        [Route("validate"), HttpPost]
        public HttpResponseMessage EchoJobSeekerRequirementsValidationTestRequest(TestJobSeekerAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            return Request.CreateResponse(model);
        }

    }
}
