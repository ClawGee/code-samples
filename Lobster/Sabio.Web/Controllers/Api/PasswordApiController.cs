using Sabio.Web.Models.Requests;
using Sabio.Web.Domain;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sabio.Web.Models.Responses;
using System.Diagnostics;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/password")]
    public class PasswordApiController : ApiController
    {
        [Route(""), HttpPost]
        public HttpResponseMessage Read(PasswordEmailRequest email)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemResponse<bool> response = new ItemResponse<bool>();
            response.Item = PasswordService.checkEmail(email);
            return Request.CreateResponse(response);
        }

        [Route("confirm/{passwordGuid:guid}"), HttpGet]

        public HttpResponseMessage Read(Guid passwordGuid)
        {
            ItemResponse<UserTokenCheck> response = new ItemResponse<UserTokenCheck>();
            response.Item = PasswordService.checkToken(passwordGuid);
            return Request.CreateResponse(response);
        }

        [Route(""), HttpPut]

        public HttpResponseMessage Update(PasswordChangeRequest info)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemResponse<bool> response = new ItemResponse<bool>();
            response.Item = UserService.ChangePassWord(info.User, info.Password);
            PasswordService.removeToken(info.User);
            return Request.CreateResponse(response);
        }
    }

}