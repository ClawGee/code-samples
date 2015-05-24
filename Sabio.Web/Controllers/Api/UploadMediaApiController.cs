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
    [Authorize]
    [RoutePrefix("api/uploadmedia")]
    public class UploadMediaApiController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage Insert(UploadInsertRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemResponse<int> response = new ItemResponse<int>();
            string currentUserId = UserService.GetCurrentUserId();
            response.Item = MediaService.UploadInsert(model, currentUserId);

            return Request.CreateResponse(response);
        }
    }
}