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
        public HttpResponseMessage GetUsStates()
        {

          List<StateDomainModel> newUsStates = GeoStateService.GetUsStates();

            ItemsResponse<StateDomainModel> response = new ItemsResponse<StateDomainModel>();

            response.Items = newUsStates;

            return Request.CreateResponse(response);

        }


    }
}

