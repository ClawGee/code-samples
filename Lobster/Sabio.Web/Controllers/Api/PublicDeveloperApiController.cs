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
using System.IO;
using System.Web;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/public")]
    public class PublicDeveloperApiController : ApiController
    {
        [Route("developer/{developerGuid:guid}"), HttpGet]
        public HttpResponseMessage Read(Guid developerGuid)
        {
            string UserRequestedId = developerGuid.ToString();
            Developer newDeveloperProfileId = DeveloperService.Select(UserRequestedId);

            ItemResponse<Developer> response = new ItemResponse<Developer>();
            response.Item = newDeveloperProfileId;
            return Request.CreateResponse(response);
        }
    }
}