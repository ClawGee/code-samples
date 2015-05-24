using Sabio.Web.Domain;
using Sabio.Web.Models.request;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.Requiter;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/recruiters")]
    public class RecruiterApiController : ApiController
    {
        [Authorize]
        [Route, HttpPost]
        public HttpResponseMessage Create(recruiterRequestModel model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                // call UserService.createUser w/ email & password

            }
            else
            {
                ItemResponse<Guid> response = new ItemResponse<Guid>();
                String currentUserId = UserService.GetCurrentUserId();

                response.Item = RecruiterBioService.Create(model, currentUserId);

                //AppEmailService.SendContactUs(model.Industry, model.Placements, model.Roles, model.Locations, model.Website, model.CompanyName, model.PhoneNumbers, model.Email);


                return Request.CreateResponse(HttpStatusCode.OK, response);

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

        [Authorize]
        [Route("{uid:guid}"), HttpPut]
        public HttpResponseMessage Update(RecruiterUpdateRequest model, Guid uid)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                // call UserService.createUser w/ email & password

            }
            else
            {
                SuccessResponse response = new SuccessResponse();

                String currentUserId = UserService.GetCurrentUserId();
                RecruiterBioService.Update(model, currentUserId);

                // AppEmailService.SendContactUs(model.Industry, model.placements, model.roles, model.Locations, model.website, model.CompanyName, model.PhoneNumbers, model.Email);


                return Request.CreateResponse(HttpStatusCode.OK, response);

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


        [Route("{uid:guid}"), HttpGet]//fix later
        public HttpResponseMessage Get(Guid uid)
        {


            ItemResponse<RecruiterBioDomainModel> response = new ItemResponse<RecruiterBioDomainModel>();
            RecruiterBioDomainModel item = RecruiterBioService.GetBios(uid);
            response.Item = item;



            return Request.CreateResponse(response);
        }


        [Authorize]
        [Route, HttpGet]//fix later
        public HttpResponseMessage GetBios()
        {
            string currentUserId = UserService.GetCurrentUserId();

            ItemResponse<RecruiterBioDomainModel> response = new ItemResponse<RecruiterBioDomainModel>();
            RecruiterBioDomainModel item = RecruiterBioService.GetBios(currentUserId);
            response.Item = item;

            //controller level steps
            //1) get current user id from userservice
            //2) create a new variable with type RecruiterBioDomainModel to hold the output 
            //3) call in service make a call to RecuriterBioService.GetBio
            //4) create a new envelope with ItemResponse<RecruiterBioDomainModel>
            //5) set response.Item = new domain model
            //6) return output model



            return Request.CreateResponse(response);
        }

         [Route("upload"), HttpPost]
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                ItemResponse<int> response = new ItemResponse<int>();

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    string uniqueName = Guid.NewGuid().ToString() + postedFile.FileName;//uniqueName is the remoteName.  Guid makes it so that names do not repeat.
                    string localPath = HttpContext.Current.Server.MapPath("~/FileTemp/" + uniqueName);

                    postedFile.SaveAs(localPath);

                    string currentUserId = UserService.GetCurrentUserId();
                    RecruiterBioDomainModel entity = RecruiterBioService.GetBios(currentUserId);
                    FileService.uploadFile(localPath, uniqueName, postedFile.ContentType);

                    UploadInsertRequestModel model = new UploadInsertRequestModel();
                    model.Filename = uniqueName;
                    model.ContentType = postedFile.ContentType;
                    model.EntityType = 3;
                    model.EntityId = entity.Id;
                    model.MediaType = 1;

                   

                    response.Item = MediaService.UploadInsert(model, currentUserId);
                    
                }
                return Request.CreateResponse(response);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

    }


}

    





