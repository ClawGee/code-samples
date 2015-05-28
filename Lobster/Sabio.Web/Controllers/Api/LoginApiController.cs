using Sabio.Web.Models;
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
    [RoutePrefix("api/login")]
    public class LoginApiController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage LoginValidationPost(LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            bool signInSuccess = UserService.Signin(model.Email, model.Password);
            ItemResponse<bool> response = new ItemResponse<bool>();
            
            response.Item = signInSuccess;
            
            return Request.CreateResponse(response);
        }
    }
}
