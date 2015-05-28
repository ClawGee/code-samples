using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using Sabio.Data;
using Sabio.Web.Domain;

namespace Sabio.Web.Services
{
    public class JobsService : BaseService
    {

        public static Guid JobCreate(JobsCreateRequestModel model, string currentUserId)
        {
            //basically declaring empty variable 
            Guid uid = Guid.Empty;

            //SC* new code UserService.GetAppUserId here
            int appUserId = 0;
            appUserId = UserService.GetAppUserId(currentUserId);

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppJobs_Insert",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@AppUserId", appUserId); //put new AppUserId var here
                paramCollection.AddWithValue("@JobTitle", model.JobTitle);
                paramCollection.AddWithValue("@JobDescription", model.JobDescription);
                paramCollection.AddWithValue("@SalaryFillIn", model.SalaryFillIn);
                paramCollection.AddWithValue("@SalaryDropDown", model.SalaryDropDown);
                paramCollection.AddWithValue("@JobLevelRadio", model.JobLevelRadio);
                paramCollection.AddWithValue("@JobSkillsCheckBox", model.JobSkillsCheckBox);
                paramCollection.AddWithValue("@locationStateDropDown", model.locationStateDropDown);
                paramCollection.AddWithValue("@locationCityFillIn", model.locationCityFillIn);

                SqlParameter p = new SqlParameter("@UId", System.Data.SqlDbType.UniqueIdentifier);
                p.Direction = System.Data.ParameterDirection.Output;

                paramCollection.Add(p);

            }, returnParameters: delegate(SqlParameterCollection param)
            {
                //tryparse, out is a .NET method inside of the GUID class
                Guid.TryParse(param["@UId"].Value.ToString(), out uid);
            });

            return uid;

        }


        public static JobsDomainModel Select(Guid uid)
        {

            JobsDomainModel job = null;


            DataProvider.ExecuteCmd(GetConnection, "dbo.AppJobs_SelectByUid",
            inputParamMapper: delegate(SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@UId", uid);
            }
               , map: delegate(IDataReader reader, short set)
               {

                   job = new JobsDomainModel();
                   //p.Title do something here... not sure what yet


                   //TestEmployee p = new TestEmployee();
                   int startingIndex = 0; //startingOrdinal
                   job.JobTitle = reader.GetSafeString(startingIndex++);
                   job.JobDescription = reader.GetSafeString(startingIndex++);
                   job.SalaryFillIn = reader.GetSafeInt32(startingIndex++);
                   job.SalaryDropDown = reader.GetSafeInt32(startingIndex++);
                   job.JobLevelRadio = reader.GetSafeInt32(startingIndex++);
                   job.JobSkillsCheckBox = reader.GetSafeString(startingIndex++);
                   job.LocationStateDropDown = reader.GetSafeString(startingIndex++);
                   job.LocationCityFillIn = reader.GetSafeString(startingIndex++);

               }
               );


            return job;
        }


        public static List<JobsDomainModel> Select(int appUserId)
        {

            List<JobsDomainModel> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppJobs_SelectByAppUserId",
            inputParamMapper: delegate(SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@AppUserId", appUserId);
            }
               , map: delegate(IDataReader reader, short set)
               {

                   JobsDomainModel job = null;
                   job = new JobsDomainModel();

                   int startingIndex = 0; //startingOrdinal
                   job.JobTitle = reader.GetSafeString(startingIndex++);
                   job.JobDescription = reader.GetSafeString(startingIndex++);
                   job.SalaryFillIn = reader.GetSafeInt32(startingIndex++);
                   job.SalaryDropDown = reader.GetSafeInt32(startingIndex++);
                   job.JobLevelRadio = reader.GetSafeInt32(startingIndex++);
                   job.JobSkillsCheckBox = reader.GetSafeString(startingIndex++);
                   job.LocationStateDropDown = reader.GetSafeString(startingIndex++);
                   job.LocationCityFillIn = reader.GetSafeString(startingIndex++);


                   if (list == null)
                   {
                       list = new List<JobsDomainModel>();
                   }

                   list.Add(job);
               }
               );

            return list;
        }

    }
}