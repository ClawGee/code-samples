using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Web.Services;
using System.Data;
using Sabio.Data;

namespace Sabio.Web.Services
{
    public class DeveloperService : BaseService
    {

        //guid uniquely identifies DeveloperInserts everytime this is called
        public static Guid DeveloperInsert(DeveloperRequestModel model, string currentUserGuid)
        {

            int currentAppUserId = UserService.GetAppUserId(currentUserGuid);
            
            //first declare variable with nothing in it.
            Guid uid = Guid.Empty;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppDeveloper_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    //wtf do i do with my primary id and global UID
                    paramCollection.AddWithValue("@AppUserId", currentAppUserId);
                    paramCollection.AddWithValue("@EducationLevel", model.EducationLevel);
                    paramCollection.AddWithValue("@YearsOfExperience", model.YearsOfExperience);
                    paramCollection.AddWithValue("@FrontEnd", model.FrontEnd);
                    paramCollection.AddWithValue("@BackEnd", model.BackEnd);
                    paramCollection.AddWithValue("@Network", model.Network);
                    paramCollection.AddWithValue("@Certifications", model.Certifications);

                    SqlParameter p = new SqlParameter("@UId", System.Data.SqlDbType.UniqueIdentifier);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);

                },
                returnParameters: delegate (SqlParameterCollection param)
                {
                    Guid.TryParse(param["@UId"].Value.ToString(), out uid);
                });

            return uid;
        }

        public static Developer DeveloperSelect(string currentUserGuid)
        {
            //get the current user id of the person who has logged in. 
            int currentAppUserId = UserService.GetAppUserId(currentUserGuid);

            // List<DeveloperDomainModel> list = null;
            Developer p = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppDeveloper_Select",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AppUserId", currentAppUserId);
                }
                ,
                map: delegate(IDataReader reader, short set)
                {   
                    /*
                    if (list == null)
                    {
                        list = new List<DeveloperDomainModel>();
                    }
                    */

                    p = new Developer();

                    int startingIndex = 0;
                    p.UId = reader.GetGuid(startingIndex++);
                    p.EducationLevel = reader.GetInt32(startingIndex++);
                    p.YearsOfExperience = reader.GetInt32(startingIndex++);
                    p.FrontEnd = reader.GetBoolean(startingIndex++);
                    p.BackEnd = reader.GetBoolean(startingIndex++);
                    p.Network = reader.GetBoolean(startingIndex++);
                    p.Certifications = reader.GetSafeString(startingIndex++);
                    p.Id = reader.GetInt32(startingIndex++);

                    //list.Add(p);

                }

                );

            return p;

        }

    }
}