using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api.Tests
{   [RoutePrefix("api/testsantacruz")]
    public class TestSantaCruzAPIController : ApiController
    {
        [Route("one"), HttpPost]
        public HttpResponseMessage AlternativeMethod(TestSantaCruzRequest model)
        {

            //this is practically the same code. it accomplishes the same thing but it is a bit cleaner and does not require an else.
            // the difference is in the "!" being present
            if (!ModelState.IsValid)
            {
                // the code would execute inside this part of the if statement when the data being sent over is not valid.
                // it is a convention to send back the ModelState property of the Controller in this situation as it will have your error message
                // that you defined in your DataAnnotation attributes

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }


            // Do something with the emp(not shown).
            // for testing you may be at step where you simply echo back the model you received.

            return Request.CreateResponse(HttpStatusCode.OK, model);

        }
    }
}
