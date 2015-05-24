using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Sabio.Data;
using Sabio.Web.Models.Requests;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.Projects;
using Sabio.Web.Models.Responses;

namespace Sabio.Web.Services
{

    public class ProjectService : BaseService
    {

        //guid uniquely identifies ProjectCreate everytime this is called
        public static Guid CreateProject(ProjectCreateRequestModel model, string currentUserId)
        {

            int appuserid = UserService.GetAppUserId(currentUserId);
            Guid uid = Guid.Empty;

            //Still need to add Radio buttons to make them work
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppUserProject_Create",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Name", model.name);
                paramCollection.AddWithValue("@Url", model.url);
                paramCollection.AddWithValue("@Image", model.image);
                paramCollection.AddWithValue("@Description", model.description);
                paramCollection.AddWithValue("@Launchdate", model.launchdate);
                paramCollection.AddWithValue("@AppUserID", appuserid);

                SqlParameter p = new SqlParameter("@UId", System.Data.SqlDbType.UniqueIdentifier);
                p.Direction = System.Data.ParameterDirection.Output;

                paramCollection.Add(p);
            }
            , returnParameters: delegate(SqlParameterCollection param)
            {
                //(tryparse, out) is a .net method inside of the Guid class
                Guid.TryParse(param["@UId"].Value.ToString(), out uid);
            });

            return uid;

        }


        // GET by Uid Function

        public static UserProjectDomainModel Get(Guid Uid)
        {
            UserProjectDomainModel project = null;
           

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppUserProject_SelectbyUid"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {

                   paramCollection.AddWithValue("@Uid", Uid);

               }
               , map: delegate(IDataReader reader, short set)
               {
                   project = new UserProjectDomainModel();
                   int startingIndex = 0; //startingOrdinal

                   project.Name = reader.GetSafeString(startingIndex++);
                   project.Url = reader.GetSafeString(startingIndex++);
                   project.Image = reader.GetSafeString(startingIndex++);
                   project.Description = reader.GetSafeString(startingIndex++);
                   project.Launchdate = reader.GetDateTime(startingIndex++);
                   project.Technologies = reader.GetSafeString(startingIndex++);
                   project.Uid = reader.GetGuid(startingIndex++);
                   project.Id = reader.GetSafeInt32(startingIndex++);
               
               }
               );

            return project;
        }
        

        // Select Function  by AppUserId 

        public static List<UserProjectDomainModel> ProjectsList(string currentUserId)
        {
            int appuserid = UserService.GetAppUserId(currentUserId);
            List<UserProjectDomainModel> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppUserProject_Select"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", appuserid);

               }, map: delegate(IDataReader reader, short set)
               {
                   UserProjectDomainModel p = new UserProjectDomainModel();
                   int startingIndex = 0; //startingOrdinal

                   p.Name = reader.GetSafeString(startingIndex++);
                   p.Url = reader.GetSafeString(startingIndex++);
                   p.Image = reader.GetSafeString(startingIndex++);
                   p.Description = reader.GetSafeString(startingIndex++);
                   p.Launchdate = reader.GetDateTime(startingIndex++);
                   p.Technologies = reader.GetSafeString(startingIndex++);
                   p.Uid = reader.GetGuid(startingIndex++);


                   if (list == null)
                   {
                       list = new List<UserProjectDomainModel>();
                   }

                   list.Add(p);
               }
               );

            return list;
        }

        public static void UpdateProject(Guid uid, ProjectUpdateRequestModel model)
        {

            //Still need to add Radio buttons to make it work
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppUserProject_Update",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Name", model.name);
                    paramCollection.AddWithValue("@Url", model.url);
                    paramCollection.AddWithValue("@Image", model.image);
                    paramCollection.AddWithValue("@Description", model.description);
                    paramCollection.AddWithValue("@Launchdate", model.launchdate);
                    paramCollection.AddWithValue("@UId", uid);
                }
            , returnParameters: delegate(SqlParameterCollection param)
            {

            });
        }

        //* GET Function

    }
}




