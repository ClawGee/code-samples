using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;




namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/user")]
    [Authorize]
    public class UsersApiController : ApiController
    {
                                                                                    
        [Route(""), HttpGet]
        public HttpResponseMessage Read()
        {
            string LoggedInUser = UserService.GetCurrentUserId();
            FullUser CurrentUserFullUserInfo = UserService.GetFullUserById(LoggedInUser);
            ItemResponse<FullUser> FullUserResponse = new ItemResponse<FullUser>();
            FullUserResponse.Item = CurrentUserFullUserInfo;
            return Request.CreateResponse(FullUserResponse);
        }




        [Route("all"), HttpGet]
        public HttpResponseMessage GetAllUsersInfo()
        {
            List<AllUsers> allUsersInfo = UserService.GetAllUsers();
            ItemsResponse<AllUsers> AllUsersResponse = new ItemsResponse<AllUsers>();
            AllUsersResponse.Items = allUsersInfo;
            return Request.CreateResponse(AllUsersResponse);
        }
        
            
        // For Pagination adminUser page 
        [Route("alluser"), HttpGet]
        public HttpResponseMessage GetAllUsersInfo([FromUri] int currentPage, int pageSize)
        {
            
            List<AllUsers> AllUserInfo = UserService.GetAllUsers();
            ItemResponse<PagedList<AllUsers>> response = new ItemResponse<PagedList<AllUsers>>();

            PagedList<AllUsers> pagedlist = new PagedList<AllUsers>(AllUserInfo, currentPage, pageSize);

            response.Item = pagedlist;
            return Request.CreateResponse(response);
        }
       


        [Route("photo"), HttpPost]
        public HttpResponseMessage Post()
        {
            long ticks = UtilityService.GetTimeStamp();
            string timeStamp = ticks.ToString();
                
            string currentUserId = UserService.GetCurrentUserId();
            HttpResponseMessage result = null;
            int entityType = 4;

            var httpRequest = HttpContext.Current.Request;
            int entityID = UserService.ConvertGuid(currentUserId);
            if (httpRequest.Files.Count > 0)
            {
                var userProfilePhoto = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    HttpPostedFile hpostedFile = httpRequest.Files[file] as HttpPostedFile;
                    if (hpostedFile.ContentLength == 0)
                        continue;
                    string localSavedFilePath = Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        Path.GetFileName(hpostedFile.FileName));
                    string fileType = "";
                    switch (hpostedFile.ContentType)
                    {
                        case "image/jpeg":
                            fileType = ".jpeg";
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
                    hpostedFile.SaveAs(localSavedFilePath);

                    string timeStampedPhoto = timeStamp + "_" + hpostedFile.FileName;
                    string remotePath = "media/user/images/" + timeStampedPhoto;
                    FileIoService.UploadFileToS3(localSavedFilePath, remotePath, fileType);

                    MediaService.CreateMedia(timeStampedPhoto, entityType, currentUserId, hpostedFile.ContentType, entityID);

                        try
                        {
                            File.Delete(localSavedFilePath);
                        }

                        catch (DirectoryNotFoundException dirNotFound)
                        {
                            Console.WriteLine(dirNotFound.Message);
                        }
                    
                    ItemResponse<bool> response = new ItemResponse<bool>();
                    response.Item = true;
                }
                result = Request.CreateResponse(HttpStatusCode.Created, userProfilePhoto);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }
    }
}