using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
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
    [RoutePrefix("api/developer")]
    public class DeveloperApiController : ApiController
    {
        //Using the [Authorize] Attribute - This filter checks whether the user is authenticated.
        [Authorize]
        [Route("create"), HttpPost]
        public HttpResponseMessage EchoDeveloperCreate(DeveloperRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string currentUserId = UserService.GetCurrentUserId();


            ItemResponse<Guid> response = new ItemResponse<Guid>();
            Guid newId = DeveloperService.DeveloperInsert(model, currentUserId);

            response.Item = newId;

            return Request.CreateResponse(response);

        }


        [Authorize]
        [Route("upload"), HttpPost]
        public HttpResponseMessage PostProfilePic()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {

                    HttpPostedFile postedFile = httpRequest.Files[file];
                    string uniqueName = Guid.NewGuid().ToString() + postedFile.FileName;//uniqueName is the remoteName.  Guid makes it so that names do not repeat.
                    string localPath = HttpContext.Current.Server.MapPath("~/FileTemp/" + uniqueName);

                    postedFile.SaveAs(localPath);
                    docfiles.Add(localPath);

                    string currentUserId = UserService.GetCurrentUserId();

                    Developer entity = DeveloperService.DeveloperSelect(currentUserId);
                    FileService.uploadFile(localPath, uniqueName, postedFile.ContentType);


                    UploadInsertRequestModel model = new UploadInsertRequestModel();
                    model.Filename = uniqueName;
                    model.ContentType = postedFile.ContentType;
                    model.EntityType = 1;
                    model.EntityId = entity.Id;
                    model.MediaType = 1;

                    MediaService.UploadInsert(model, currentUserId);

                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }


        [Authorize]
        [Route("view"), HttpGet]
        public HttpResponseMessage EchoDeveloperSelect()
        {

            string currentUserId = UserService.GetCurrentUserId();
            ItemResponse<Developer> response = new ItemResponse<Developer>();

            Developer item = DeveloperService.DeveloperSelect(currentUserId);

            response.Item = item;

            return Request.CreateResponse(response);
        }


    }
}
