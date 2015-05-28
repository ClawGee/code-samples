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
    [RoutePrefix("api/Tag")]
    public class TagApiController : ApiController
    {
        [Route(""), HttpGet]
        [Authorize]
        public HttpResponseMessage Read()
        {
            Tag model = new Tag();
            string currentUserId = UserService.GetCurrentUserId();
            int entityType = 3;
            int entityId = EmployerService.SelectEmployerByUserId(currentUserId).EmployerId;
            ItemResponse<List<Tag>> response = new ItemResponse<List<Tag>>();
            response.Item = TagService.GetTagList(currentUserId, entityId, entityType);
            return Request.CreateResponse(response);
        }

        [Route("all"), HttpGet]
        public HttpResponseMessage ReadAll()
        {
            ItemsResponse<Tag> response = new ItemsResponse<Tag>();
            response.Items = TagService.GetTagList();
            return Request.CreateResponse(response);
        }



    }
}