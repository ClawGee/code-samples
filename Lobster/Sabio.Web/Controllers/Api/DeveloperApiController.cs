using Sabio.Web.Domain;
using Sabio.Web.Models;
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
using System.IO;
using System.Web;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/developer")]
    public class DeveloperApiController : ApiController
    {
        [Route(""), HttpPost]
        [Authorize]
        public HttpResponseMessage Create(DeveloperProfileCreateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string currentUserId = UserService.GetCurrentUserId();
            Guid newDeveloperProfileId = DeveloperService.Insert(model, currentUserId);

            ItemResponse<Guid> response = new ItemResponse<Guid>();
            response.Item = newDeveloperProfileId;
            return Request.CreateResponse(response);
        }

        [Route(""), HttpPut]
        [Authorize]
        public HttpResponseMessage Update(DeveloperUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string currentUserId = UserService.GetCurrentUserId();
            DeveloperService.Update(model, currentUserId);

            ItemResponse<bool> response = new ItemResponse<bool>();
            response.Item = true;
            return Request.CreateResponse(response);
        }

        [Route(""), HttpGet]
        [Authorize]
        public HttpResponseMessage Read()
        {
            string currentUserId = UserService.GetCurrentUserId();
            Developer DeveloperProfile = DeveloperService.Select(currentUserId);
            ItemResponse<Developer> response = new ItemResponse<Developer>();
            response.Item = DeveloperProfile;
            return Request.CreateResponse(response);
        }

        [Route("photoupload"), HttpPost]
        public HttpResponseMessage Post()
        {
            
            string currentUserId = UserService.GetCurrentUserId();            
            int entityType = 1;
            int entityId = DeveloperService.Select(currentUserId).Id;
            DateTime TimeStamp = DateTime.Now;
            string ReformattedTimeStamp = TimeStamp.ToString("yyyyMdHms");
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var profilePhoto = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    string fileType = "";
                    switch (postedFile.ContentType)
                    {
                        case "image/jpeg":
                            fileType = ".jpg";
                            break;
                        case "image/png":
                            fileType = ".png";
                            break;
                        case "image/gif":
                            fileType = ".gif";
                            break;
                        default:
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Please upload a file of type .jpg, .png, or .gif.");
                    }
                    string filePath = HttpContext.Current.Server.MapPath("~/uploads/images/developer/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    profilePhoto.Add(filePath);
                    string NewFileName = ReformattedTimeStamp + "_" + postedFile.FileName;
                    var remotePath = "media/developer/images/" + NewFileName;
                    FileIoService.UploadFileToS3(filePath, remotePath, fileType);
                    MediaService.CreateMedia(NewFileName, entityType, currentUserId, postedFile.ContentType, entityId);                    
                        try
                        {
                            File.Delete(filePath);
                        }

                        catch (DirectoryNotFoundException dirNotFound)
                        {
                            Console.WriteLine(dirNotFound.Message);
                        }
                    ItemResponse<bool> response = new ItemResponse<bool>();
                    response.Item = true;
                }
                result = Request.CreateResponse(HttpStatusCode.Created, profilePhoto);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }
    }
}
