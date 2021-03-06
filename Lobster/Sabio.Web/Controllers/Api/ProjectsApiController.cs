﻿using Sabio.Web.Domain;
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
    
    [RoutePrefix("api/projects")]
    [Authorize]
    public class ProjectsApiController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddProject(ProjectsCreateRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string currentUserId = UserService.GetCurrentUserId();
            ItemResponse<Guid> response = new ItemResponse<Guid>(); //creating a suitcase to put my info inside to send data to client side
            response.Item = ProjectsService.CreateProject(model, currentUserId);

            return Request.CreateResponse(response);
        }

        [Route("{uid:guid}"), HttpPut]
        public HttpResponseMessage UpdateProject(ProjectsUpdateRequestModel model, Guid uid)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            ProjectsService.UpdateProject(uid, model);

            return Request.CreateResponse(response);
        }

        [Route("{projectGuid:guid}"), HttpGet]
        public HttpResponseMessage GetProjectByGuid(Guid projectGuid)
        {

            Projects model = ProjectsService.Get(projectGuid);

            ItemResponse<Projects> response = new ItemResponse<Projects>();

            response.Item = model;

            return Request.CreateResponse(response);
        }

        [Route(""), HttpGet]
        public HttpResponseMessage GetProjects()
        {
            string currentUserId = UserService.GetCurrentUserId();
            ItemsResponse<Projects> response = new ItemsResponse<Projects>();
            response.Items = ProjectsService.List(currentUserId);
            return Request.CreateResponse(response);
        }

    }


}
