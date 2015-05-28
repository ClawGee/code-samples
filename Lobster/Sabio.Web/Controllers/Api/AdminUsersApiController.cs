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
    [RoutePrefix("api/admin/user")]
    public class AdminUsersApiController : ApiController
    {
        [Route("{userId}"), HttpPut]
        
        public HttpResponseMessage Update(AdminRequest model, string userId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
           
            UserService.AdminUpdate(model, userId);

            SuccessResponse response = new SuccessResponse();
            return Request.CreateResponse(response);
        }
    }
}

