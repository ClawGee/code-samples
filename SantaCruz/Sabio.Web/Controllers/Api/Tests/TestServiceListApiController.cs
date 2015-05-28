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
    [RoutePrefix("api/testlist")]
    public class TestServiceListApiController : ApiController
    {
        [Route("people"), HttpGet]
        public  HttpResponseMessage GetList()

        {
            ItemsResponse<TestPerson> returnedList = new ItemsResponse<TestPerson>();

            //List<TestPerson> returnedList = TestService.Get(false);

            return Request.CreateResponse(returnedList);
        }
    }
  
}

