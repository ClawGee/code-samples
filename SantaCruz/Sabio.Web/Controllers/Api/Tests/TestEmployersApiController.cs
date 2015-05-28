using Sabio.Web.Models.Requests.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api.Tests
{
    [RoutePrefix("api/test/employers")]
    public class TestEmployersApiController : ApiController
    {
        [Route("validate"), HttpPost]
        public HttpResponseMessage EchoEmployersValidation (TestEmployerRequest model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            return Request.CreateResponse(model);
        }

    }
}
