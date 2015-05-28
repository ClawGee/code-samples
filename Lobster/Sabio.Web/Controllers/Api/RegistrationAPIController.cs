using Sabio.Web.Domain;
using Sabio.Web.Exceptions;
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
    [RoutePrefix("api/register")]

    public class RegistrationAPIController : ApiController
    {

        [Route(""), HttpPost]
        public HttpResponseMessage RegisterPost(RegisterRequest Model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ApplicationUser NewUser = null;
           
            try
            {
                NewUser = (ApplicationUser)UserService.CreateUser(Model.Email, Model.Password);
                UserService.UpdateUser(Model, NewUser.Id);
                AppEmailService.SendRegistrationConfirmationEmail(Model, NewUser.Id);// Gene's code for sending Registratation email confirmation
            }
            catch (IdentityResultException ire)
            {
                // here: looking for specific identityresultexception error, 400 error
                ErrorResponse error = new ErrorResponse(ire.Result.Errors);
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
                //return request.createresponse(httpstatuscode.badrequest, model);
                //return request.createresponse(httpstatuscode.badrequest, "error 400: user is already taken!!");
            }
            catch (Exception ex)
            {
                //here: catching any general server exception, internal server error 500
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
           
            ItemResponse<ApplicationUser> response = new ItemResponse<ApplicationUser>();
            response.Item = NewUser;
            return Request.CreateResponse(Model);
        }
        [Route("confirmed/{confirmationGuid:guid}"), HttpGet]
        public HttpResponseMessage RegisterGet(Guid confirmationGuid)
        {
            UserTokenCheck registrant = UserService.GetGuidRegistrationMatchToken(confirmationGuid);
            ItemResponse<UserTokenCheck> response = new ItemResponse<UserTokenCheck>();//the envelope. between the <> is the type of value returned to user.
            response.Item = registrant;//letter inside the envelope
            return Request.CreateResponse(response);//mail the letter aka passes the response.
        }
        [Route("emailconfirmed/{confirmationGuid:guid}"), HttpPut]  //set route to call value
        public HttpResponseMessage ChangeRegistrationEmailConfirmed(Guid confirmationGuid)//pass guid
        {
            UserService.ChangeRegistrationEmailConfirmed(confirmationGuid); //call service. no equals neccessary in beween
            return Request.CreateResponse(true);
        }


    }
}


//public static void Main()
//{
//    try 
//    {


//    }
//    catch (identityresultexception ire)
//    {
//    // here: looking for specific identityresultexception error, 400 error
//    errorresponse error = new errorresponse(ire.result.errors);
//    return request.createresponse(httpstatuscode.badrequest, error);
//    //return request.createresponse(httpstatuscode.badrequest, model);
//    //return request.createresponse(httpstatuscode.badrequest, "error 400: user is already taken!!");
//    }
//    catch (exception ex)
//    {
//    //here: catching any general server exception, internal server error 500
//    return request.createerrorresponse(httpstatuscode.internalservererror, ex);
//    }
//}
