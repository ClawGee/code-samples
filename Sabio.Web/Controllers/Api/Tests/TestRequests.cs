using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api.Tests
{


    [RoutePrefix("api/testcenter")]
    public class TestRequests : ApiController
    {

        #region - TestService Calls -
        [Route("EchoPerson"), HttpPost]
        public HttpResponseMessage EchoPerson(TestPersonAddRequest model)
        {
            /*
             *There is no datavalidation in this method. this is simply here for you
             *to be able to echo data back to yourself with no validation
             
             */

            return Request.CreateResponse(model);
        }

        [Route("EchoPerson/validate"), HttpPost]
        public HttpResponseMessage EchoPersonValidation(TestPersonAddRequest model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            
            return Request.CreateResponse(model);
        }

        //[Route("people"), HttpPost]
        //public HttpResponseMessage AddPerson(TestPersonAddRequest model)
        //{
        //    // if the Model does not pass validation, there will be an Error response returned with errors
        //    if(!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }


        //    ItemResponse<Guid> response = new ItemResponse<Guid>();

        //    response.Item = TestService.InsertTest(model);

        //    return Request.CreateResponse(response);
        //}


        [Route("people"), HttpGet]
        public HttpResponseMessage GetPeople(bool demoError = true)
        {
            ItemsResponse<TestPerson> response = new ItemsResponse<TestPerson>();

            response.Items = TestService.Get(demoError);

            return Request.CreateResponse(response);
        }

        [Route("people/noerrors"), HttpGet]
        public HttpResponseMessage GetPeople()
        {
            ItemsResponse<TestPerson> response = new ItemsResponse<TestPerson>();

            response.Items = TestService.Get(false);

            return Request.CreateResponse(response);
        } 
        #endregion
    
    }

}
