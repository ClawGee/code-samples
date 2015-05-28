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
    [RoutePrefix("api/public")]
    public class PublicApiController : ApiController
    {
        [Route("recruiter/{recruiterGuid:guid}"), HttpGet]
        public HttpResponseMessage GetRecruiter(Guid recruiterGuid)
        {
            Recruiter model = new Recruiter();
            string recruiterId = recruiterGuid.ToString();
            ItemResponse<Recruiter> response = new ItemResponse<Recruiter>();
            response.Item = RecruiterService.Select(recruiterId);
            return Request.CreateResponse(response);
        }

        [Route("employer/{employerGuid:guid}"), HttpGet]
        public HttpResponseMessage ReadEmployer(Guid employerGuid)
        {
            Employer model = new Employer();
            string employerId = employerGuid.ToString();

            ItemResponse<Employer> response = new ItemResponse<Employer>();

            response.Item = EmployerService.SelectEmployerById(employerId);
            return Request.CreateResponse(response);
        }

        [Route("job/{jobGuid:guid}"), HttpGet]
        public HttpResponseMessage GetJob(Guid jobGuid)
        {
            Job model = new Job();
            //string currentUserId = jobGuid.ToString();
            ItemResponse<Job> response = new ItemResponse<Job>();
            response.Item = JobsService.GetJobByUid(jobGuid);
            return Request.CreateResponse(response);
        }

        [Route("developer/{developerGuid:guid}"), HttpGet]
        public HttpResponseMessage Read(Guid developerGuid)
        {
            Developer model = new Developer();
            string developerId = developerGuid.ToString();
            ItemResponse<Developer> response = new ItemResponse<Developer>();
            response.Item = DeveloperService.Select(developerId);
            return Request.CreateResponse(response);
        }

        [Route("developer/projects/{developerGuid:guid}"), HttpGet]
        public HttpResponseMessage ListProjects(Guid developerGuid)
        {
            string developerId = developerGuid.ToString();
            List<Projects> newProjects = ProjectsService.List(developerId);

            ItemsResponse<Projects> response = new ItemsResponse<Projects>();

            response.Items = newProjects;

            return Request.CreateResponse(response);
        }

        [Route("projects/{projectGuid:guid}"), HttpGet]
        public HttpResponseMessage GetProject(Guid projectGuid)
        {
            Projects model = new Projects();
            ItemResponse<Projects> response = new ItemResponse<Projects>();
            response.Item = ProjectsService.Get(projectGuid);
            return Request.CreateResponse(response);
        }

    }
}