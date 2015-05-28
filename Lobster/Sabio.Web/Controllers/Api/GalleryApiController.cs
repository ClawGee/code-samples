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
    //[Authorize]
    [RoutePrefix("api/gallery")]
    public class GalleryApiController : ApiController
    {
        [Route(""), HttpPost]
        public HttpResponseMessage Create(GalleryRequest Model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string loggedInUser = UserService.GetCurrentUserId();
            EntityIdentifiers entityIdentity = UserService.GetEntity(loggedInUser);
            int newGalleryId = GalleryService.Insert(Model, loggedInUser, entityIdentity);

            //new instance of GalleryIdentifier domain object, properties from EntityIdentifier(type, id) & Guid newGalleryId
            GalleryIdentifier galleryIdent = new GalleryIdentifier();
            galleryIdent.EntityType = entityIdentity.EntityType;
            galleryIdent.EntityId = entityIdentity.EntityId;

            galleryIdent.Id = newGalleryId;
            ItemResponse<GalleryIdentifier> response = new ItemResponse<GalleryIdentifier>(); //return <GalleryIdentifier>
            response.Item = galleryIdent;
            return Request.CreateResponse(response);
        }

        [Route(""), HttpPut]
        public HttpResponseMessage Update(GalleryRequest Model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string loggedInUser = UserService.GetCurrentUserId();
            GalleryService.Update(Model, loggedInUser);

            ItemResponse<bool> response = new ItemResponse<bool>();
            response.Item = true;
            return Request.CreateResponse(response);
        }

        [Route("image/{mediaId:int}"), HttpDelete]
        public HttpResponseMessage DeleteImage(int mediaId)
        {
            MediaService.DeleteImage(mediaId);

            ItemResponse<bool> response = new ItemResponse<bool>();
            response.Item = true;
            return Request.CreateResponse(response);
        }

        [Route("manage/{galleryId:int}"), HttpGet]
        public HttpResponseMessage GetSingleGallery(int galleryId)
        {
            Gallery model = new Gallery();
            ItemResponse<Gallery> response = new ItemResponse<Gallery>();
            response.Item = GalleryService.GetGallerybyId(galleryId);
            return Request.CreateResponse(response);
        }
        
        [Route(""), HttpGet]
        public HttpResponseMessage Read()
        {
            string loggedInUser = UserService.GetCurrentUserId();

            ItemsResponse<Gallery> response = new ItemsResponse<Gallery>();
            response.Items = GalleryService.Select(loggedInUser);
            return Request.CreateResponse(response);
        }

        [Route("images/{galleryId:int}"), HttpGet]
        public HttpResponseMessage GetGalleryMedias(int galleryId)
        {
            ItemsResponse<Media> response = new ItemsResponse<Media>();
            response.Items = GalleryService.GetGalleryMedias(galleryId);
            return Request.CreateResponse(response);
        }

        [Route("images/{galleryId:int}"), HttpPost]
        public HttpResponseMessage Images(int galleryId)
        {
            long ticks = UtilityService.GetTimeStamp();
            string timeStamp = ticks.ToString();

            string currentUserId = UserService.GetCurrentUserId();
            Gallery currentGallery = GalleryService.GetGallerybyId(galleryId);
            int entityType = currentGallery.EntityType;
            int entityID = currentGallery.EntityId;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var fullPath = "";
                foreach (string file in httpRequest.Files)
                {
                    HttpPostedFile postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/uploads/images/gallery/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    string timeStampPhoto = timeStamp + "_" + postedFile.FileName;
                    string remotePath = "media/gallery/images/" + timeStampPhoto;
                    FileIoService.UploadFileToS3(filePath, remotePath, postedFile.ContentType);
                    MediaService.CreateGalleryMedia(timeStampPhoto, entityType, currentUserId, postedFile.ContentType, entityID, galleryId);
                    fullPath = FileIoService.GetGalleryMediaURL(postedFile.FileName);
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
