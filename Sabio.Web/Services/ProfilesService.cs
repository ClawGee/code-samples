using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sabio.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using Sabio.Web.Exceptions;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Sabio.Web.Models.Requests;
using System.Data.SqlClient;

namespace Sabio.Web.Services
{
    public class ProfilesService : BaseService
    {
        //int appUserId = UserService.GetAppUserId(currentUserId);

       
    //public static List<ExperienceDomainModel> GetExperiencesByUserGuid(string currentUserId);
    //public static DeveloperDomainModel DeveloperSelect(string currentUserGuid);

    
        // * 
        // * DataProvider.ExecuteQuery( store proc for experience)
        // * DAtaProvider.ExecuteQuery(User Address)
        // * DataProvider.ExecuteQuery(Projects)
        // * DataProvider.ExecuteQuery(JobSeeker Skills)
        // * DataProvider.ExecuteQuery(any registration info you need from AspNetUser)
        // *
     
        //public static object GetSignUpInfo(int AppUserId)
        //{
         
            
        //    object signUpInfo = {};
        //    //function to execute query because i want the whole row of info
        //    DataProvider.ExecuteQuery(GetConnection, "dbo.            ", inputParamMapper: delegate(SqlParameterCollection paramCollection)
        //    {
            
        //        //need to state output parameters
        //        SqlParameter p = new SqlParameter("@             ", System.Data.SqlDbType.Int);
        //        p.Direction = System.Data.ParameterDirection.Output;

        //    },

        //    returnParameters: delegate(SqlParameterCollection param)
        //    {

        //        //need a way to output a whole object
        //        object.TryParse(signUpInfo, out             );

        //    });
            
        //    return signUpInfo;



        //}
        
    }
}