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

    public class ProjectsService : BaseService
    {

        //guid uniquely identifies ProjectCreate everytime this is called
        public static Guid CreateProject(ProjectsCreateRequestModel model, string CurrentUserId)
        {
            int LoggedInUser = UserService.ConvertGuid(CurrentUserId);
            Guid uid = Guid.Empty;

            //Still need to add Radio buttons to make them work
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppDeveloperProjects_Update_V2",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Name", model.name);
                    paramCollection.AddWithValue("@Url", model.url);
                    paramCollection.AddWithValue("@Image", model.image);
                    paramCollection.AddWithValue("@Description", model.description);
                    paramCollection.AddWithValue("@Launchdate", model.launchdate);
                    paramCollection.AddWithValue("@AppUserID", LoggedInUser);

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

        public static Projects Get(Guid Uid)
        {
            Projects project = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppDeveloperProjects_SelectbyUid_V2"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {

                   paramCollection.AddWithValue("@Uid", Uid);

               }
               , map: delegate(IDataReader reader, short set)
               {
                   project = MapProjects(reader);
               }
               );

            return project;
        }

        private static Projects MapProjects(IDataReader reader)
        {
            Projects project = new Projects();
            int startingIndex = 0;

            project.Name = reader.GetSafeString(startingIndex++);
            project.Url = reader.GetSafeString(startingIndex++);
            project.Image = reader.GetSafeString(startingIndex++);
            project.Description = reader.GetSafeString(startingIndex++);
            project.Launchdate = reader.GetDateTime(startingIndex++);
            project.Uid = reader.GetGuid(startingIndex++);
            project.Id = reader.GetSafeInt32(startingIndex++);

            string techString = reader.GetSafeString(startingIndex++);

            if (techString != null && techString.Length > 0)
            {
                project.setTechnologiesString(techString);
            }
            return project;
        }


        // Select Function  by AppUserId 

        public static List<Projects> List(string CurrentUserId)
        {
            int IdCurrentUser = UserService.ConvertGuid(CurrentUserId);
            List<Projects> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppDeveloperProjects_SelectV2"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", IdCurrentUser);

               }, map: delegate(IDataReader reader, short set)
               {
                  Projects p = MapProjects(reader); 
                  
                   if (list == null)
                   {
                       list = new List<Projects>();
                   }

                   list.Add(p);
               }
               );

            return list;
        }

        public static void UpdateProject(Guid uid, ProjectsUpdateRequestModel model)
        {

            //Still need to add Radio buttons to make it work
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppDeveloperProjects_Update_V2",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Name", model.name);
                    paramCollection.AddWithValue("@Url", model.url);
                    paramCollection.AddWithValue("@Image", model.image);
                    paramCollection.AddWithValue("@Description", model.description);
                    paramCollection.AddWithValue("@Launchdate", model.launchdate);
                    paramCollection.AddWithValue("@UId", uid);

                    SqlParameter p = new SqlParameter("@Technologies", System.Data.SqlDbType.Structured);

                    if (model.technologies != null && model.technologies.Any())
                    {
                        p.Value = new Sabio.Data.IntIdTable(model.technologies);
                    }

                    paramCollection.Add(p);
                }
            , returnParameters: delegate(SqlParameterCollection param)
            {

            });
        }

        //* GET Function

    }
}