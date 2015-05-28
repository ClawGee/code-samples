using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/address")]

    public class AddressApiController : ApiController
    {
        [Authorize]
        [Route(""), HttpPost]
        public HttpResponseMessage Create(AddressCreateRequest Model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            string LoggedInUser = UserService.GetCurrentUserId();
            Guid NewAddressId = AddressService.InsertAddress(Model, LoggedInUser);

            ItemResponse<Guid> AddressResponse = new ItemResponse<Guid>();
            AddressResponse.Item = NewAddressId;
            return Request.CreateResponse(AddressResponse);
        }
        [Authorize]
        [Route("{addressGuid:guid}"), HttpPut]
        public HttpResponseMessage Update(AddressCreateRequest Model, Guid AddressGuid)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            string LoggedInUser = UserService.GetCurrentUserId();
            AddressService.UpdateAddress(Model, AddressGuid, LoggedInUser);
            ItemResponse<Guid> AddressResponse = new ItemResponse<Guid>();
            AddressResponse.Item = AddressGuid;
            return Request.CreateResponse(AddressResponse);
        }

        [Authorize]
        [Route(""), HttpGet]
        public HttpResponseMessage Get()
        {
            string LoggedInUser = UserService.GetCurrentUserId();
            Address CurrentUserAddress = AddressService.GetAddressBlock(LoggedInUser);
            ItemResponse<Address> AddressResponse = new ItemResponse<Address>();
            AddressResponse.Item = CurrentUserAddress;
            return Request.CreateResponse(AddressResponse);
        }

        [Authorize]
        [Route("RadiusSearch"), HttpGet]
        public HttpResponseMessage GetRadius([FromUri]AddressRadiusRequest model)
        {
            List<FullUser> list = AddressService.GetAddressRadiusBlock(model);
            Debug.WriteLine(list);
            ItemsResponse<FullUser> response = new ItemsResponse<FullUser>();
            response.Items = list;
            return Request.CreateResponse(response);
        }
    }
}
