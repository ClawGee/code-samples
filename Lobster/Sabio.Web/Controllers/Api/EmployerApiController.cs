using Sabio.Web.Models.Requests;
using Sabio.Web.Domain;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sabio.Web.Models.Responses;
using System.Web;
using System.Diagnostics;
using System.IO;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/Employer")]
    public class EmployerApiController : ApiController
    {
        [Route("create"), HttpPost]
        [Authorize]
        public HttpResponseMessage Create(EmployerRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            string currentUserId = UserService.GetCurrentUserId();
            ItemResponse<Guid> response = new ItemResponse<Guid>();
            response.Item = EmployerService.InsertEmployer(model, currentUserId);


            return Request.CreateResponse(response);

        }


        [Route("upload"), HttpPost]
        public HttpResponseMessage Post()
        {
            string currentUserId = UserService.GetCurrentUserId();
            int entityType = 3;
            int entityID = EmployerService.SelectEmployerByUserId(currentUserId).EmployerId;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var fullPath = "";
                foreach (string file in httpRequest.Files)
                {
                    HttpPostedFile postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/uploads/images/employer/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    string remotePath = "media/employer/images/" + postedFile.FileName;
                    FileIoService.UploadFileToS3(filePath, remotePath, postedFile.ContentType);
                    MediaService.CreateMedia(postedFile.FileName, entityType, currentUserId, postedFile.ContentType, entityID);
                    fullPath = FileIoService.FileHelperGet(entityType, postedFile.FileName);
                    File.Delete(filePath);
                }
                ItemResponse<string> response = new ItemResponse<string>();
                response.Item = fullPath;
                return Request.CreateResponse(response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }



        [Route(""), HttpGet]
        [Authorize]
        public HttpResponseMessage Read()
        {
            Employer model = new Employer();
            string currentUserId = UserService.GetCurrentUserId();
            
            ItemResponse<Employer> response = new ItemResponse<Employer>();
            response.Item = EmployerService.SelectEmployerByUserId(currentUserId);
            return Request.CreateResponse(response);
        }


        [Route("tag/{tagName}"), HttpGet]
        public HttpResponseMessage GetTag(string tagName, [FromUri] SearchRequest request)        
        { 
            List<EmployerSearchResults> EmployersByTagList = EmployerService.GetEmployersByTag(tagName);

            ItemResponse<PagedList<EmployerSearchResults>> response = new ItemResponse<PagedList<EmployerSearchResults>>();

            PagedList<EmployerSearchResults> pagedlist = new PagedList<EmployerSearchResults>(EmployersByTagList, request.CurrentPage,request.ItemsPerPage);

            response.Item = pagedlist;

            return Request.CreateResponse(response);
        }


        [Route("image"), HttpGet]
        [Authorize]
        public HttpResponseMessage ReadImage()
        {
            Media model = new Media();
            string currentUserId = UserService.GetCurrentUserId();
            int entityType = 3;
            int entityId = EmployerService.SelectEmployerByUserId(currentUserId).EmployerId;
            ItemResponse<Media> response = new ItemResponse<Media>();
            response.Item = MediaService.SelectMedia(currentUserId, entityType, entityId);
            return Request.CreateResponse(response);
        }


        [Route(""), HttpPut]
        [Authorize]
        public HttpResponseMessage Update(EmployerRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            string currentUserId = UserService.GetCurrentUserId();
            ItemResponse<Guid> response = new ItemResponse<Guid>();
            response.Item = EmployerService.UpdateEmployer(model, currentUserId);
            return Request.CreateResponse(response);

        }

    }
}

