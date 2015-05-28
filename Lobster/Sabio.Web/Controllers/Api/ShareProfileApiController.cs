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
    [RoutePrefix("api/shareprofile")]
    public class ShareProfileApiController : ApiController
    {
        [Route("{profileGuid:guid}"), HttpPost]
        public HttpResponseMessage Insert(ShareProfileRequest model, Guid profileGuid)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            string userRequestedId = profileGuid.ToString();
            AppEmailService.SendShareProfile(model, userRequestedId);

            ItemResponse<bool> response = new ItemResponse<bool>();
            response.Item = true;
            return Request.CreateResponse(response);
        }
    }
}