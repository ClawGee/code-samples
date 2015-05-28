using Microsoft.AspNet.Identity.EntityFramework;
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
    [Authorize]
    [RoutePrefix("api/account")]
    public class AccountApiController : ApiController
    {
        [Route(""), HttpPut]
        public HttpResponseMessage AccountUpdate(UpdateAccountRequest Model)
        {
            if (!ModelState.IsValid) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string loggedInUser = UserService.GetCurrentUserId();
            //string loggedInUser = "11182858-d43b-4245-b1eb-0013a5654486";
            UserService.UpdateAccount(Model, loggedInUser);

            ItemResponse<bool> response = new ItemResponse<bool>();
            response.Item = true;
            return Request.CreateResponse(response);
        }

        [Route("password"), HttpPut]
        public HttpResponseMessage PasswordUpdate(UpdatePasswordRequest Model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            IdentityUser currentUser = UserService.GetCurrentUser();
            bool checkPassword = UserService.Signin(currentUser.Email, Model.currentPassword);

            if (checkPassword)
            {
                string loggedInUser = currentUser.Id;
                UserService.ChangePassWord(loggedInUser, Model.newPassword);

            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Current password is invalid.");

            ItemResponse<bool> response = new ItemResponse<bool>();
            response.Item = true;
            return Request.CreateResponse(response);
        }
    }
}
