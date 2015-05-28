using Sabio.Web.Models.Requests;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api.Support
{
    [RoutePrefix("api/support")]
    public class SupportApiController : ApiController
    {
        
        [Route(""), HttpPost]
        public HttpResponseMessage Create(ContactUsRequest Model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            Guid NewId = SupportService.InsertContactUs(Model);

            AppEmailService.SendContactUs(Model);
            AppEmailService.SendContactUsToAdmin(Model);
            return Request.CreateResponse(Model);
        }


    }
}