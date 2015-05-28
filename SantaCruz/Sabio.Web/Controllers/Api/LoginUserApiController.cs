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
    [RoutePrefix("api/LoginUser")]
    public class LoginUserApiController : ApiController
    {
        [Route("validate"), HttpPost]
        public HttpResponseMessage EchoAccountValidation(LoginUserRequestModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                

            }


            bool success = UserService.Signin(model.Email, model.LoginPassword);
            if (success)
            {
                SuccessResponse sresponce = new SuccessResponse();
                return Request.CreateResponse(HttpStatusCode.OK, sresponce);
            }
            else
            {
                ErrorResponse sresponce = new ErrorResponse("invalid login");
                return Request.CreateResponse(HttpStatusCode.BadRequest, sresponce);

            }


        }
    }
}
