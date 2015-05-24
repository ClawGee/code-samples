using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.Projects;
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
    [Authorize]
    [RoutePrefix("api/projects")]
    public class ProjectApiController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddProject(ProjectCreateRequestModel model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemResponse<Guid> response = new ItemResponse<Guid>(); //creating a suitcase to put my info inside to send data to client side
            string currentUserId = UserService.GetCurrentUserId();
            response.Item = ProjectService.CreateProject(model, currentUserId);

            return Request.CreateResponse(response);
        }

        [Route("{uid:guid}"), HttpPut]
        public HttpResponseMessage UpdateProject(ProjectUpdateRequestModel model, Guid uid)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            ProjectService.UpdateProject(uid, model);

            return Request.CreateResponse(response);
        }

        [Route("{uid:guid}"), HttpGet]
        public HttpResponseMessage GetProject(Guid Uid)
        {

            UserProjectDomainModel model = ProjectService.Get(Uid);

            ItemResponse<UserProjectDomainModel> response = new ItemResponse<UserProjectDomainModel>();

            response.Item = model;

            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetProjects()
        {
            string currentUserId = UserService.GetCurrentUserId();
            ItemsResponse<UserProjectDomainModel> response = new ItemsResponse<UserProjectDomainModel>();
            response.Items = ProjectService.ProjectsList(currentUserId);
            return Request.CreateResponse(response);
        }

        [Route("upload/{projectUid:guid}"), HttpPost]
        public HttpResponseMessage Post(Guid projectUid)
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                ItemResponse<int> response = new ItemResponse<int>();

                UserProjectDomainModel entity= ProjectService.Get(projectUid);
              
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    string uniqueName = Guid.NewGuid().ToString() + postedFile.FileName;//uniqueName is the remoteName.  Guid makes it so that names do not repeat.
                    string localPath = HttpContext.Current.Server.MapPath("~/FileTemp/" + uniqueName);

                    postedFile.SaveAs(localPath);

                    FileService.uploadFile(localPath, uniqueName, postedFile.ContentType);
                    

                    UploadInsertRequestModel model = new UploadInsertRequestModel();
                    model.Filename = uniqueName;
                    model.ContentType = postedFile.ContentType;
                    model.EntityType = 1;
                    model.EntityId = entity.Id;
                    model.MediaType = 4;

                    string currentUserId = UserService.GetCurrentUserId();

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

