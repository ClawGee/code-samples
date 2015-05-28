using Sabio.Web.Domain;
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
    [RoutePrefix("api/geography")]
    public class GeographyApiController : ApiController
    {
        [Route("states"), HttpGet]
        public HttpResponseMessage Read()
        {
            ItemsResponse<State> GeographyResponse = new ItemsResponse<State>();
            GeographyResponse.Items = AddressService.GetAmericanStates();
            return Request.CreateResponse(GeographyResponse);
        }

    }
}
