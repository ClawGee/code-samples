using Sabio.Web.Domain;
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
    [RoutePrefix("api/admin")]
    public class AdminApiController : ApiController
    {
        //page get, will need to [authorize]
        [Route("getusers"), HttpGet]
        public HttpResponseMessage EchoAspNetUsers()
        {
            ItemsResponse<AdminUsersDomainModel> response = new ItemsResponse<AdminUsersDomainModel>();
            List<AdminUsersDomainModel> items = AdminService.GetAspNetUsers();
            response.Items = items;
            return Request.CreateResponse(response);
        }

        //editmodal
        [Route("getappuser/{AspNetUsersId:guid}"), HttpGet]
        public HttpResponseMessage EchoAspNetUser(string AspNetUsersId)
        {
            ItemResponse<AdminUsersDomainModel> response = new ItemResponse<AdminUsersDomainModel>();
            AdminUsersDomainModel item = AdminService.GetAspNetUser(AspNetUsersId);
            response.Item = item;
            return Request.CreateResponse(response);
        }

        //tab1
        [Route("updateuser"), HttpPut]
        public HttpResponseMessage UpdateUser(AdminUpdateUserRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                ItemResponse<bool> response = new ItemResponse<bool>();
                AdminService.PutAspNetUser(model);
                response.Item = true;
                return Request.CreateResponse(response);
            }
        }

        //tab2. AppUserId should be int, but useraddressservice is written with string instead
        [Route("getaddresses/{AspNetUsersId:guid}"), HttpGet]
        public HttpResponseMessage EchoAddresses(Guid AspNetUsersId)
        {
            string currentAspNetUsersId = AspNetUsersId.ToString();
            ItemsResponse<UserAddress> response = new ItemsResponse<UserAddress>();
            response.Items = UserAddressService.SelectUserAddresses(currentAspNetUsersId);
            return Request.CreateResponse(response);
        }

        [Route("createaddress/{AspNetUsersId:guid}"), HttpPost]
        public HttpResponseMessage CreateAddress(UserAddressModelRequest model, Guid AspNetUsersId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemResponse<Guid> response = new ItemResponse<Guid>();
            string currentAspNetUsersId = AspNetUsersId.ToString();
            Guid newAppUserAddressId = UserAddressService.InsertAppUsersAddress(model, currentAspNetUsersId);
            response.Item = newAppUserAddressId;

            return Request.CreateResponse(response);
        }

        [Route("updateaddress/{AddressGuid:guid}"), HttpPut]
        public HttpResponseMessage UpdateAddress(string AddressGuid)
        {
            return null;
        }

        [Route("deleteaddress/{AddressGuid:guid}"), HttpPut]
        public HttpResponseMessage DeleteAddress(Guid AddressGuid)
        {
            string currentAddressGuid = AddressGuid.ToString();
            SuccessResponse response = new SuccessResponse();
            AdminService.DeleteAddress(currentAddressGuid);

            return Request.CreateResponse(response);
        }
        //tab3.
        [Route("getexperiences/{AspNetUsersId:guid}"), HttpGet]
        public HttpResponseMessage EchoExperiences(Guid AspNetUsersId)
        {
            string currentAspNetUsersId = AspNetUsersId.ToString();
            ItemsResponse<ExperienceDomainModel> response = new ItemsResponse<ExperienceDomainModel>();
            response.Items = ExperienceService.GetExperiencesByUserGuid(currentAspNetUsersId);
            return Request.CreateResponse(response);
        }

        //tab6
        [Route("updatepassword"), HttpPut]
        public HttpResponseMessage UpdatePassword(AdminUpdatePasswordRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                ItemResponse<bool> response = new ItemResponse<bool>();

                //bool updateSuccess = UserService.ChangePassWord(model.AspNetUsersId, model.Password);
                //response.Item = updateSuccess;
                response.Item = UserService.ChangePassWord(model.AspNetUsersId, model.Password);
                return Request.CreateResponse(response);
            }
        }
    }
}
