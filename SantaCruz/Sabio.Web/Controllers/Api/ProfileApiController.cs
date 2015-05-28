using Sabio.Web.Domain;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using Sabio.Web.Models.Requests;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/profile")]
    public class ProfileApiController : ApiController
    
    {

        [Route("developer/{currentUserId}"), HttpGet]
        public HttpResponseMessage GetProfile(string currentUserId)
       
        {

            ItemResponse<Developer> response = new ItemResponse<Developer>();

            Developer item = DeveloperService.DeveloperSelect(currentUserId);

            response.Item = item;

            return Request.CreateResponse(response);

        }


    }
}

