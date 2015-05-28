using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Models.Requests;
using Sabio.Web.Services;

namespace Sabio.Web.Services
{
    public class JobsService : BaseService
    {
        // SQL Insert: JobsService
        public static Guid InsertJobs(JobsAddRequest model, String CurrentUserId)
        {
            int EntityId = 0;
            int AppUserId = UserService.ConvertGuid(CurrentUserId);
            int EntityType = UserService.GetFullUserById(CurrentUserId).UserType;
            switch (EntityType)
            {
                case 1:
                    throw new Exception("Developers are not allowed to create jobs.");

                case 2:
                    Recruiter recruiterentity = RecruiterService.Select(CurrentUserId);
                    if (recruiterentity != null)
                    {
                        EntityId = recruiterentity.Id;
                    }

                    break;

                case 3:
                    Employer employerentity = EmployerService.SelectEmployerByUserId(CurrentUserId);
                    if (employerentity != null)
                    {
                        EntityId = employerentity.EmployerId;
                    }
                    break;

            }

            Guid uid = Guid.Empty;//000-0000-0000-0000

            //int EntityId = EmployerService.

            DataProvider.ExecuteNonQuery(GetConnection,
                "dbo.AppJobs_Insert",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Title", model.Title);
                    paramCollection.AddWithValue("@Description", model.Description);
                    paramCollection.AddWithValue("@Qualifications", model.Qualifications);
                    paramCollection.AddWithValue("@Contacts", model.Contacts);
                    paramCollection.AddWithValue("@PrimaryEmail", model.PrimaryEmail);
                    paramCollection.AddWithValue("@Url", model.Url);
                    paramCollection.AddWithValue("@Rate", model.Rate);
                    paramCollection.AddWithValue("@Terms", model.Terms);
                    paramCollection.AddWithValue("@EntityType", EntityType);
                    paramCollection.AddWithValue("@EntityId", EntityId);
                    paramCollection.AddWithValue("@AppUserId", AppUserId); //no longer hardcoded

                    SqlParameter p = new SqlParameter("@Uid", System.Data.SqlDbType.UniqueIdentifier);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);

                }, returnParameters: delegate(SqlParameterCollection param)
                {
                    Guid.TryParse(param["@Uid"].Value.ToString(), out uid);
                }
               );
            return uid;
        }
        // SQL Update: JobsService
        public static void UpdateJobs(JobsAddRequest model, Guid jobGuid)
        {
            //Guid uid = Guid.Empty;//000-0000-0000-0000
            //int AppUserId = UserService.ConvertGuid(CurrentUserId);

            DataProvider.ExecuteNonQuery(
                GetConnection,
                "dbo.AppJobs_Update",
                inputParamMapper: delegate(SqlParameterCollection paramCollection
                )
                {
                    paramCollection.AddWithValue("@Uid", jobGuid);
                    paramCollection.AddWithValue("@Title", model.Title);
                    paramCollection.AddWithValue("@Description", model.Description);
                    paramCollection.AddWithValue("@Qualifications", model.Qualifications);
                    paramCollection.AddWithValue("@Contacts", model.Contacts);
                    paramCollection.AddWithValue("@PrimaryEmail", model.PrimaryEmail);
                    paramCollection.AddWithValue("@Url", model.Url);
                    //paramCollection.AddWithValue("@AppUserId", AppUserId);
                    paramCollection.AddWithValue("@Rate", model.Rate);
                    //paramCollection.AddWithValue("@EntityType", model.EntityType);
                    paramCollection.AddWithValue("@Terms", model.Terms);
                    //paramCollection.AddWithValue("@EntityId", model.EntityId);

                }, returnParameters: delegate(SqlParameterCollection param)
                {
                    // no return params for this function
                    // Guid.TryParse(param["@Uid"].Value.ToString(), out uid);
                }
               );
        }
        // SQL GetJobByUid: JobsService
        public static Job GetJobByUid(Guid jobGuid)
        {

            Job selectedJob = new Job();
            DataProvider.ExecuteCmd(
                GetConnection,
                "dbo.AppJobs_SelectByUid",
                inputParamMapper: delegate(SqlParameterCollection paramCollection
                )
                {
                    paramCollection.AddWithValue("@Uid", jobGuid);
                },
                map: delegate(IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    selectedJob.Uid = reader.GetGuid(startingIndex++);
                    selectedJob.Title = reader.GetSafeString(startingIndex++);
                    selectedJob.Description = reader.GetSafeString(startingIndex++);
                    selectedJob.Qualifications = reader.GetSafeString(startingIndex++);
                    selectedJob.Contacts = reader.GetSafeString(startingIndex++);
                    selectedJob.PrimaryEmail = reader.GetSafeString(startingIndex++);
                    selectedJob.Url = reader.GetSafeString(startingIndex++);
                    selectedJob.Rate = reader.GetSafeString(startingIndex++);
                    //selectedJob.EntityId = reader.GetSafeInt32(startingIndex++);
                    //selectedJob.EntityType = reader.GetSafeInt32(startingIndex++);
                    selectedJob.Terms = reader.GetSafeString(startingIndex++);
                    //selectedJob.Id = reader.GetSafeInt16(startingIndex++);

                    ////SqlParameter p = new SqlParameter("@Uid", System.Data.SqlDbType.UniqueIdentifier);
                    //p.Direction = System.Data.ParameterDirection.Output;
                    //paramCollection.Add(p);
                }
           );
            return selectedJob;
        }
        //public static Job GetJobsByAppUserId(int AppUserId)
        public static List<Job> GetJobsByAppUserId(String CurrentUserId)
        {

            int AppUserId = UserService.ConvertGuid(CurrentUserId);
            FullUser User = UserService.GetFullUserById(CurrentUserId);
            List<Job> listOfJobs = null; // new List<Job>();
            DataProvider.ExecuteCmd(
                GetConnection,
                "dbo.AppJobs_SelectByAppUserId",
                inputParamMapper: delegate(SqlParameterCollection paramCollection
                )
                {
                    paramCollection.AddWithValue("@AppUserId", AppUserId);
                },
                map: delegate(IDataReader reader, short set)
                {
                    Job selectedJob = new Job();
                    int startingIndex = 0;
                    selectedJob.Uid = reader.GetGuid(startingIndex++);
                    selectedJob.Title = reader.GetSafeString(startingIndex++);
                    selectedJob.Description = reader.GetSafeString(startingIndex++);
                    selectedJob.Qualifications = reader.GetSafeString(startingIndex++);
                    selectedJob.Contacts = reader.GetSafeString(startingIndex++);
                    selectedJob.PrimaryEmail = reader.GetSafeString(startingIndex++);
                    selectedJob.Url = reader.GetSafeString(startingIndex++);
                    selectedJob.Rate = reader.GetSafeString(startingIndex++);
                    //selectedJob.EntityId = reader.GetSafeInt32(startingIndex++);
                    //selectedJob.EntityType = reader.GetSafeInt32(startingIndex++);
                    selectedJob.Terms = reader.GetSafeString(startingIndex++);
                    //decorator pattern
                    if (User.UserType == 3)
                    {
                        selectedJob.EmployerUser = EmployerService.SelectEmployerByUserId(CurrentUserId);
                        //include employer info
                    }

                    if (listOfJobs == null)
                    {
                        listOfJobs = new List<Job>();
                    }
                    listOfJobs.Add(selectedJob);
                }
           );
            return listOfJobs;
        }


        //List of jobs by STATUS


        public static List<Job> GetJobsByStatus(Boolean Status)
        {

            List<Job> listOfJobs = null;

            DataProvider.ExecuteCmd(
                GetConnection,
                "dbo.AppJobs_SelectByStatus",
                inputParamMapper: delegate(SqlParameterCollection paramCollection
                )
                {
                    paramCollection.AddWithValue("@Approved", (Status== true) ? 1 : 0);  //Ternary operation FTW !!
                },
                map: delegate(IDataReader reader, short set)
                {

                    Job selectedJob = new Job();
                    int startingIndex = 0;

                    selectedJob.Uid = reader.GetGuid(startingIndex++);
                    selectedJob.Title = reader.GetSafeString(startingIndex++);
                    selectedJob.Description = reader.GetSafeString(startingIndex++);
                    selectedJob.Qualifications = reader.GetSafeString(startingIndex++);
                    selectedJob.Contacts = reader.GetSafeString(startingIndex++);
                    selectedJob.PrimaryEmail = reader.GetSafeString(startingIndex++);
                    selectedJob.Url = reader.GetSafeString(startingIndex++);
                    selectedJob.Rate = reader.GetSafeString(startingIndex++);
                    selectedJob.Terms = reader.GetSafeString(startingIndex++);
                    selectedJob.Approved = reader.GetSafeBool(startingIndex++);


                    if (listOfJobs == null)
                    {
                        listOfJobs = new List<Job>();
                    }
                    listOfJobs.Add(selectedJob);
                }
           );
            return listOfJobs;
        }
    }
}
