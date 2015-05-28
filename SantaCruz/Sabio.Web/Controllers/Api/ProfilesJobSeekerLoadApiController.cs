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

    [RoutePrefix ("api/profile-old")]
    public class ProfilesJobSeekerLoadApiController : ApiController
    {
        [Route ("candidate"), HttpGet]
        public HttpResponseMessage GetProfileInfo(object model)
        {   

            string other_looks_like_a_guid = UserService.GetCurrentUserId();

            //List<ExperienceDomainModel> newExperience = ExperienceService.GetExperiencesByUserGuid(currentUserId);

            ItemsResponse<int> response = new ItemsResponse<int>();


            //response.Items = newExperience;


            return Request.CreateResponse(response);
        }

        //get appuserid using userservice.getcurrentuserID
        //store that in a variable
        //call a service
        //pass in user guid 
    }
}
