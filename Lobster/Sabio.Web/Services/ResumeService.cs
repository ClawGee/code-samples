using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Models.Requests;
using Sabio.Web.Domain.Tests;

namespace Sabio.Web.Services
{
    public class ResumeService : BaseService
    {

        public static Guid InsertResume(ResumeRequest model, string CurrentUserId)
        {
            int LoggedInUser = UserService.ConvertGuid(CurrentUserId);
            Guid Uid = Guid.Empty;//000-0000-0000-0000

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppDeveloperResumes_Insert"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", LoggedInUser);
                   paramCollection.AddWithValue("@Inst1Name", model.Inst1Name);
                   paramCollection.AddWithValue("@Inst1Location", model.Inst1Location);
                   paramCollection.AddWithValue("@Inst1DateFrom", model.Inst1DateFrom);
                   paramCollection.AddWithValue("@Inst1DateTo", model.Inst1DateTo);
                   paramCollection.AddWithValue("@Inst1Degree", model.Inst1Degree);
                   paramCollection.AddWithValue("@Inst1DegreeMajor", model.Inst1DegreeMajor);
                   paramCollection.AddWithValue("@Inst1DegreeMinor", model.Inst1DegreeMinor);
                   paramCollection.AddWithValue("@Inst1DegreeGpa", model.Inst1DegreeGpa);
                   paramCollection.AddWithValue("@Inst2Name", model.Inst2Name);
                   paramCollection.AddWithValue("@Inst2Location", model.Inst2Location);
                   paramCollection.AddWithValue("@Inst2DateFrom", model.Inst2DateFrom);
                   paramCollection.AddWithValue("@Inst2DateTo", model.Inst2DateTo);
                   paramCollection.AddWithValue("@Inst2Degree", model.Inst2Degree);
                   paramCollection.AddWithValue("@Inst2DegreeMajor", model.Inst2DegreeMajor);
                   paramCollection.AddWithValue("@Inst2DegreeMinor", model.Inst2DegreeMinor);
                   paramCollection.AddWithValue("@Inst2DegreeGpa", model.Inst2DegreeGpa);
                   paramCollection.AddWithValue("@Inst3Name", model.Inst3Name);
                   paramCollection.AddWithValue("@Inst3Location", model.Inst3Location);
                   paramCollection.AddWithValue("@Inst3DateFrom", model.Inst3DateFrom);
                   paramCollection.AddWithValue("@Inst3DateTo", model.Inst3DateTo);
                   paramCollection.AddWithValue("@Inst3Degree", model.Inst3Degree);
                   paramCollection.AddWithValue("@Inst3DegreeMajor", model.Inst3DegreeMajor);
                   paramCollection.AddWithValue("@Inst3DegreeMinor", model.Inst3DegreeMinor);
                   paramCollection.AddWithValue("@Inst3DegreeGpa", model.Inst3DegreeGpa);
                   paramCollection.AddWithValue("@Job1EmployerName", model.Job1EmployerName);
                   paramCollection.AddWithValue("@Job1Department", model.Job1Department);
                   paramCollection.AddWithValue("@Job1EmployerLocation", model.Job1EmployerLocation);
                   paramCollection.AddWithValue("@Job1EmployerDescr", model.Job1EmployerDescr);
                   paramCollection.AddWithValue("@Job1Position", model.Job1Position);
                   paramCollection.AddWithValue("@Job1DateFrom", model.Job1DateFrom);
                   paramCollection.AddWithValue("@Job1DateTo", model.Job1DateTo);
                   paramCollection.AddWithValue("@Job1Description", model.Job1Description);
                   paramCollection.AddWithValue("@Job2EmployerName", model.Job2EmployerName);
                   paramCollection.AddWithValue("@Job2Department", model.Job2Department);
                   paramCollection.AddWithValue("@Job2EmployerLocation", model.Job2EmployerLocation);
                   paramCollection.AddWithValue("@Job2EmployerDescr", model.Job2EmployerDescr);
                   paramCollection.AddWithValue("@Job2Position", model.Job2Position);
                   paramCollection.AddWithValue("@Job2DateFrom", model.Job2DateFrom);
                   paramCollection.AddWithValue("@Job2DateTo", model.Job2DateTo);
                   paramCollection.AddWithValue("@Job2Description", model.Job2Description);
                   paramCollection.AddWithValue("@Job3EmployerName", model.Job3EmployerName);
                   paramCollection.AddWithValue("@Job3Department", model.Job3Department);
                   paramCollection.AddWithValue("@Job3EmployerLocation", model.Job3EmployerLocation);
                   paramCollection.AddWithValue("@Job3EmployerDescr", model.Job3EmployerDescr);
                   paramCollection.AddWithValue("@Job3Position", model.Job3Position);
                   paramCollection.AddWithValue("@Job3DateFrom", model.Job3DateFrom);
                   paramCollection.AddWithValue("@Job3DateTo", model.Job3DateTo);
                   paramCollection.AddWithValue("@Job3Description", model.Job3Description);
                   paramCollection.AddWithValue("@ObjectiveStmnt", model.ObjectiveStmnt);
                   paramCollection.AddWithValue("@VolunteerExperience", model.VolunteerExperience);
                   paramCollection.AddWithValue("@Hobbies", model.Hobbies);
                   paramCollection.AddWithValue("@FrontEndSkills", model.FrontEndSkills);
                   paramCollection.AddWithValue("@BackEndSkills", model.BackEndSkills);
                   paramCollection.AddWithValue("@AdditionalSkills", model.AdditionalSkills);
                   paramCollection.AddWithValue("@Certifications", model.Certifications);

                   SqlParameter p = new SqlParameter("@Uid", System.Data.SqlDbType.UniqueIdentifier);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   Guid.TryParse(param["@Uid"].Value.ToString(), out Uid);
               }
               );
            return Uid;
        }

        public static void UpdateResume(ResumeRequest model, string CurrentUserId)
        {
            int LoggedInUser = UserService.ConvertGuid(CurrentUserId);

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppDeveloperResumes_Update"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", LoggedInUser);
                   paramCollection.AddWithValue("@Inst1Name", model.Inst1Name);
                   paramCollection.AddWithValue("@Inst1Location", model.Inst1Location);
                   paramCollection.AddWithValue("@Inst1DateFrom", model.Inst1DateFrom);
                   paramCollection.AddWithValue("@Inst1DateTo", model.Inst1DateTo);
                   paramCollection.AddWithValue("@Inst1Degree", model.Inst1Degree);
                   paramCollection.AddWithValue("@Inst1DegreeMajor", model.Inst1DegreeMajor);
                   paramCollection.AddWithValue("@Inst1DegreeMinor", model.Inst1DegreeMinor);
                   paramCollection.AddWithValue("@Inst1DegreeGpa", model.Inst1DegreeGpa);
                   paramCollection.AddWithValue("@Inst2Name", model.Inst2Name);
                   paramCollection.AddWithValue("@Inst2Location", model.Inst2Location);
                   paramCollection.AddWithValue("@Inst2DateFrom", model.Inst2DateFrom);
                   paramCollection.AddWithValue("@Inst2DateTo", model.Inst2DateTo);
                   paramCollection.AddWithValue("@Inst2Degree", model.Inst2Degree);
                   paramCollection.AddWithValue("@Inst2DegreeMajor", model.Inst2DegreeMajor);
                   paramCollection.AddWithValue("@Inst2DegreeMinor", model.Inst2DegreeMinor);
                   paramCollection.AddWithValue("@Inst2DegreeGpa", model.Inst2DegreeGpa);
                   paramCollection.AddWithValue("@Inst3Name", model.Inst3Name);
                   paramCollection.AddWithValue("@Inst3Location", model.Inst3Location);
                   paramCollection.AddWithValue("@Inst3DateFrom", model.Inst3DateFrom);
                   paramCollection.AddWithValue("@Inst3DateTo", model.Inst3DateTo);
                   paramCollection.AddWithValue("@Inst3Degree", model.Inst3Degree);
                   paramCollection.AddWithValue("@Inst3DegreeMajor", model.Inst3DegreeMajor);
                   paramCollection.AddWithValue("@Inst3DegreeMinor", model.Inst3DegreeMinor);
                   paramCollection.AddWithValue("@Inst3DegreeGpa", model.Inst3DegreeGpa);
                   paramCollection.AddWithValue("@Job1EmployerName", model.Job1EmployerName);
                   paramCollection.AddWithValue("@Job1Department", model.Job1Department);
                   paramCollection.AddWithValue("@Job1EmployerLocation", model.Job1EmployerLocation);
                   paramCollection.AddWithValue("@Job1EmployerDescr", model.Job1EmployerDescr);
                   paramCollection.AddWithValue("@Job1Position", model.Job1Position);
                   paramCollection.AddWithValue("@Job1DateFrom", model.Job1DateFrom);
                   paramCollection.AddWithValue("@Job1DateTo", model.Job1DateTo);
                   paramCollection.AddWithValue("@Job1Description", model.Job1Description);
                   paramCollection.AddWithValue("@Job2EmployerName", model.Job2EmployerName);
                   paramCollection.AddWithValue("@Job2Department", model.Job2Department);
                   paramCollection.AddWithValue("@Job2EmployerLocation", model.Job2EmployerLocation);
                   paramCollection.AddWithValue("@Job2EmployerDescr", model.Job2EmployerDescr);
                   paramCollection.AddWithValue("@Job2Position", model.Job2Position);
                   paramCollection.AddWithValue("@Job2DateFrom", model.Job2DateFrom);
                   paramCollection.AddWithValue("@Job2DateTo", model.Job2DateTo);
                   paramCollection.AddWithValue("@Job2Description", model.Job2Description);
                   paramCollection.AddWithValue("@Job3EmployerName", model.Job3EmployerName);
                   paramCollection.AddWithValue("@Job3Department", model.Job3Department);
                   paramCollection.AddWithValue("@Job3EmployerLocation", model.Job3EmployerLocation);
                   paramCollection.AddWithValue("@Job3EmployerDescr", model.Job3EmployerDescr);
                   paramCollection.AddWithValue("@Job3Position", model.Job3Position);
                   paramCollection.AddWithValue("@Job3DateFrom", model.Job3DateFrom);
                   paramCollection.AddWithValue("@Job3DateTo", model.Job3DateTo);
                   paramCollection.AddWithValue("@Job3Description", model.Job3Description);
                   paramCollection.AddWithValue("@ObjectiveStmnt", model.ObjectiveStmnt);
                   paramCollection.AddWithValue("@VolunteerExperience", model.VolunteerExperience);
                   paramCollection.AddWithValue("@Hobbies", model.Hobbies);
                   paramCollection.AddWithValue("@FrontEndSkills", model.FrontEndSkills);
                   paramCollection.AddWithValue("@BackEndSkills", model.BackEndSkills);
                   paramCollection.AddWithValue("@AdditionalSkills", model.AdditionalSkills);
                   paramCollection.AddWithValue("@Certifications", model.Certifications);

               }, returnParameters: delegate(SqlParameterCollection param)
               {

               }
               );
        }

        public static Resume GetResume(string CurrentUserId)
        {
            int LoggedInUser = UserService.ConvertGuid(CurrentUserId);
            Resume ResumeRow = null;

            DataProvider.ExecuteCmd(GetConnection, "AppDeveloperResumes_Select",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserId", LoggedInUser);
                }
               , map: delegate(IDataReader reader, short set)
               {
                   ResumeRow = new Resume();
                   int startingIndex = 0; //startingOrdinal

                   ResumeRow.Inst1Name = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst1Location = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst1DateFrom = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst1DateTo = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst1Degree = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst1DegreeMajor = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst1DegreeMinor = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst1DegreeGpa = reader.GetSafeDecimal(startingIndex++);

                   ResumeRow.Inst2Name = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst2Location = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst2DateFrom = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst2DateTo = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst2Degree = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst2DegreeMajor = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst2DegreeMinor = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst2DegreeGpa = reader.GetSafeDecimal(startingIndex++);

                   ResumeRow.Inst3Name = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst3Location = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst3DateFrom = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst3DateTo = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst3Degree = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst3DegreeMajor = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst3DegreeMinor = reader.GetSafeString(startingIndex++);
                   ResumeRow.Inst3DegreeGpa = reader.GetSafeDecimal(startingIndex++);

                   ResumeRow.Job1EmployerName = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job1Department = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job1EmployerLocation = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job1EmployerDescr = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job1Position = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job1DateFrom = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job1DateTo = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job1Description = reader.GetSafeString(startingIndex++);

                   ResumeRow.Job2EmployerName = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job2Department = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job2EmployerLocation = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job2EmployerDescr = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job2Position = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job2DateFrom = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job2DateTo = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job2Description = reader.GetSafeString(startingIndex++);

                   ResumeRow.Job3EmployerName = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job3Department = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job3EmployerLocation = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job3EmployerDescr = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job3Position = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job3DateFrom = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job3DateTo = reader.GetSafeString(startingIndex++);
                   ResumeRow.Job3Description = reader.GetSafeString(startingIndex++);

                   ResumeRow.ObjectiveStmnt = reader.GetSafeString(startingIndex++);
                   ResumeRow.VolunteerExperience = reader.GetSafeString(startingIndex++);
                   ResumeRow.Hobbies = reader.GetSafeString(startingIndex++);
                   ResumeRow.FrontEndSkills = reader.GetSafeString(startingIndex++);
                   ResumeRow.BackEndSkills = reader.GetSafeString(startingIndex++);
                   ResumeRow.AdditionalSkills = reader.GetSafeString(startingIndex++);
                   ResumeRow.Certifications = reader.GetSafeString(startingIndex++);
                   ResumeRow.UId = reader.GetGuid(startingIndex++);
               }
            );
            return ResumeRow;
        }

        public static FullResume GetFullResume(string CurrentUserId)
        {
            int LoggedInUser = UserService.ConvertGuid(CurrentUserId);
            FullResume FullResumeRow = null;

            DataProvider.ExecuteCmd(GetConnection, "AppDeveloperResumes_FullResumeSelect",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserId", LoggedInUser);
                }
               , map: delegate(IDataReader reader, short set)
               {
                   FullResumeRow = new FullResume();
                   int startingIndex = 0; //startingOrdinal

                   FullResumeRow.ResumeData = new Resume();
                   FullResumeRow.ResumeData.Id = reader.GetSafeInt32(startingIndex++);
                   FullResumeRow.ResumeData.Inst1Name = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst1Location = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst1DateFrom = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst1DateTo = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst1Degree = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst1DegreeMajor = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst1DegreeMinor = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst1DegreeGpa = reader.GetSafeDecimal(startingIndex++);

                   FullResumeRow.ResumeData.Inst2Name = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst2Location = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst2DateFrom = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst2DateTo = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst2Degree = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst2DegreeMajor = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst2DegreeMinor = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst2DegreeGpa = reader.GetSafeDecimal(startingIndex++);
                   
                   FullResumeRow.ResumeData.Inst3Name = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst3Location = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst3DateFrom = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst3DateTo = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst3Degree = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst3DegreeMajor = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst3DegreeMinor = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Inst3DegreeGpa = reader.GetSafeDecimal(startingIndex++);

                   FullResumeRow.ResumeData.Job1EmployerName = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job1Department = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job1EmployerLocation = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job1EmployerDescr = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job1Position = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job1DateFrom = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job1DateTo = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job1Description = reader.GetSafeString(startingIndex++);

                   FullResumeRow.ResumeData.Job2EmployerName = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job2Department = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job2EmployerLocation = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job2EmployerDescr = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job2Position = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job2DateFrom = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job2DateTo = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job2Description = reader.GetSafeString(startingIndex++);

                   FullResumeRow.ResumeData.Job3EmployerName = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job3Department = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job3EmployerLocation = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job3EmployerDescr = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job3Position = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job3DateFrom = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job3DateTo = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Job3Description = reader.GetSafeString(startingIndex++);

                   FullResumeRow.ResumeData.ObjectiveStmnt = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.VolunteerExperience = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Hobbies = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.FrontEndSkills = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.BackEndSkills = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.AdditionalSkills = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.Certifications = reader.GetSafeString(startingIndex++);
                   FullResumeRow.ResumeData.UId = reader.GetGuid(startingIndex++);

                   FullResumeRow.UserData = new FullUser();
                   FullResumeRow.UserData.Email = reader.GetSafeString(startingIndex++);
                   FullResumeRow.UserData.PhoneNumber = reader.GetSafeString(startingIndex++);
                   FullResumeRow.UserData.LastName = reader.GetSafeString(startingIndex++);
                   FullResumeRow.UserData.FirstName = reader.GetSafeString(startingIndex++);
                   FullResumeRow.UserData.UserType = reader.GetSafeInt32(startingIndex++);
                   int UserMediaId = reader.GetSafeInt32(startingIndex++);
                   if (UserMediaId > 0)
                   {
                       FullResumeRow.UserData.Avatar = MediaService.SelectMediaByMediaId(UserMediaId);
                   }
                   FullResumeRow.AddressData = new Address();
                   FullResumeRow.AddressData.AddressLine1 = reader.GetSafeString(startingIndex++);
                   FullResumeRow.AddressData.AddressLine2 = reader.GetSafeString(startingIndex++);
                   FullResumeRow.AddressData.City = reader.GetSafeString(startingIndex++);
                   FullResumeRow.AddressData.State = reader.GetSafeInt32(startingIndex++);
                   FullResumeRow.AddressData.Zip = reader.GetSafeInt32(startingIndex++);
               }
            );
            return FullResumeRow;
        }
    }
}