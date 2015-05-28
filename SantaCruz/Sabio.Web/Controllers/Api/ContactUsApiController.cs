using Sabio.Web.ContactUs;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
     [RoutePrefix("api/ContactUs")]
    public class ContactUsApiController : ApiController
    {

         [Route("SendMsg"), HttpPost]
        public HttpResponseMessage EchoAccountValidation(ContactUsRequestModel model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                // call UserService.createUser w/ email & password

            }
            else
            {
                int NewId = ContactUsService.Create(model);
                AppEmailService.SendContactUs(model.Email, model.Name, model.Subject, model.Message); 

                SuccessResponse sresponce = new SuccessResponse();
                return Request.CreateResponse(HttpStatusCode.OK, sresponce);

            }
            
            //bool success = UserService.Signin(model.Email, model.LoginPassword);
            //if (success)
            //{
            //    SuccessResponse sresponce = new SuccessResponse();
            //    return Request.CreateResponse(HttpStatusCode.OK ,sresponce);
            //}
            //else {
            //    ErrorResponse sresponce = new ErrorResponse("invalid login");
            //    return Request.CreateResponse(HttpStatusCode.BadRequest, sresponce);
            
            //}

            
        }
    }
}
