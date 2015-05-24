using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{

    [RoutePrefix("api/users/addresses")]
    public class UsersAddressAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage Create(UserAddressModelRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<Guid> response = new ItemResponse<Guid>();
            string currentUserId = UserService.GetCurrentUserId();
            Guid newId = UserAddressService.InsertAppUsersAddress(model, currentUserId);

            response.Item = newId;

            return Request.CreateResponse(response);
        }

        [Route("{addressGuid:guid}"), HttpPut]
        public HttpResponseMessage Edit(UserAddressUpdateRequest model, Guid addressGuid)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            //Below is for consistency's sake/professional way. 
            ItemResponse<bool> response = new ItemResponse<bool>();
            UserAddressService.UpdateAppUsersAddress(model, addressGuid);

            response.Item = true;

            return Request.CreateResponse(response);
        }



        [Route, HttpGet]
        [Authorize]
        public HttpResponseMessage ListUsersAddresses()
        {
            string currentUserId = UserService.GetCurrentUserId();
            ItemsResponse<UserAddress> response = new ItemsResponse<UserAddress>();
            response.Items = UserAddressService.SelectUserAddresses(currentUserId);
            return Request.CreateResponse(response);
        }


        //ANGULAR create new endpoint to return one single address by addressGuid
        [Route("{addressGuid:guid}"), HttpGet]
        [Authorize]
        public HttpResponseMessage GetAddress(Guid addressGuid)
        {
            UserAddress model = UserAddressService.SelectSingleUserAddress(addressGuid);
            ////use same domain model but return only single address
            ItemResponse<UserAddress> response = new ItemResponse<UserAddress>();
            response.Item = UserAddressService.SelectSingleUserAddress(addressGuid);
            return Request.CreateResponse(response);
        }

        ////create new endpoint to return one single address by addressGuid
        //[Route("{addressGuid:guid}"), HttpGet]
        //[Authorize]
        //public HttpResponseMessage GetAddress(Guid addressGuid)
        //{
        //    UserAddress model = UserAddressService.SelectSingleUserAddress(addressGuid);
        //    ////use same domain model but return only single address
        //    ItemResponse<UserAddress> response = new ItemResponse<UserAddress>();
        //    response.Item = UserAddressService.SelectSingleUserAddress(addressGuid);
        //    return Request.CreateResponse(response);
        //}



    }
}