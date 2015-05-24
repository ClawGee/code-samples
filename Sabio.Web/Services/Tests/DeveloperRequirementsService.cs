using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using Sabio.Data;

namespace Sabio.Web.Services
{
    public class DeveloperRequirementsService : BaseService
    {

        //guid uniquely identifies JobSeekerInserts everytime this is called
        public static Guid DeveloperInsert(DeveloperRequirementsAddRequest model)
        {
            //basically declaring the variable with nothing in it. 
            Guid uid = Guid.Empty;


            DataProvider.ExecuteNonQuery(GetConnection, "dbo.JobSeekerRequirements_Create",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                //wtf do i do with my primary key id and global UID
                
                paramCollection.AddWithValue("@WantedJobTitleKeywords", model.WantedJobTitleKeywords);
                paramCollection.AddWithValue("@WantedLocation", model.WantedLocation);
                paramCollection.AddWithValue("@WorkType", model.WorkType);
                paramCollection.AddWithValue("@Skills", model.Skills);
                paramCollection.AddWithValue("@CompanyType", model.CompanyType);
                paramCollection.AddWithValue("@SalaryRange", model.SalaryRange);
                paramCollection.AddWithValue("@JobDistance", model.JobDistance);
                paramCollection.AddWithValue("@ZipCode", model.ZipCode);
                paramCollection.AddWithValue("@Languages", model.Languages);
                paramCollection.AddWithValue("@AppUserID", "1");

                SqlParameter p = new SqlParameter("@UId", System.Data.SqlDbType.UniqueIdentifier);
                p.Direction = System.Data.ParameterDirection.Output;

                paramCollection.Add(p);

            }, returnParameters: delegate(SqlParameterCollection param)
            
            {
                //tryparse, out is a .net method inside of the Guid class
                Guid.TryParse(param["@UId"].Value.ToString(), out uid);
            });

            return uid;

        }

    }
}