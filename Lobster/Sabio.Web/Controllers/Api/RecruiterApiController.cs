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
using System.Web;
using System.IO;

namespace Sabio.Web.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/recruiter")]
    public class RecruiterApiController : ApiController
    {
        //[Route("Validate"), HttpPost]
        //public HttpResponseMessage Validate(UpdateRecruiterRequest Model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //    return Request.CreateResponse(Model);
        //}

        [Route(""), HttpPost]
        public HttpResponseMessage Create(UpdateRecruiterRequest Model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string loggedInUser = UserService.GetCurrentUserId();
            Guid newRecruiterId = RecruiterService.Insert(Model, loggedInUser);
            //return Request.CreateResponse(newRecruiterId);
            ItemResponse<Guid> response = new ItemResponse<Guid>();
            response.Item = newRecruiterId;
            return Request.CreateResponse(response);
        }

        [Route(""), HttpPut]
        public HttpResponseMessage Update(UpdateRecruiterRequest Model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string loggedInUser = UserService.GetCurrentUserId();
            RecruiterService.Update(Model, loggedInUser);
        
            ItemResponse<bool> response = new ItemResponse<bool>();
            response.Item = true;
            return Request.CreateResponse(response);
        }

        [Route(""), HttpGet]
        public HttpResponseMessage Read()
        {
            Recruiter model = new Recruiter();
            string loggedInUser = UserService.GetCurrentUserId();

            ItemResponse<Recruiter> response = new ItemResponse<Recruiter>();
            response.Item = RecruiterService.Select(loggedInUser);
            return Request.CreateResponse(response);
        }


        //Upload for Files
        [Route("files"), HttpPost]
        public HttpResponseMessage Files()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/uploads/files/recruiter/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

        //Upload for Photos
        [Route("images"), HttpPost]
        public HttpResponseMessage Images()
        {
            long ticks = UtilityService.GetTimeStamp();
            string timeStamp = ticks.ToString();

            string currentUserId = UserService.GetCurrentUserId();
            int entityType = 2;
            int entityID = RecruiterService.Select(currentUserId).Id;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var fullPath = "";
                foreach (string file in httpRequest.Files)
                {
                    HttpPostedFile postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/uploads/images/recruiter/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    string timeStampPhoto = timeStamp + "_" + postedFile.FileName;
                    string remotePath = "media/recruiter/images/" + timeStampPhoto;
                    FileIoService.UploadFileToS3(filePath, remotePath, postedFile.ContentType);
                    MediaService.CreateMedia(timeStampPhoto, entityType, currentUserId, postedFile.ContentType, entityID);
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
    }
}