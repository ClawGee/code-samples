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

namespace Sabio.Web.Services
{
    public class SupportService : BaseService
    {
        // This is a Method within a Service Class. This service class must inherit from the BaseService Class that you have in your project.
        public static Guid InsertContactUs(ContactUsRequest model)
        {
            Guid uid = Guid.Empty;//000-0000-0000-0000 

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppContactUs_Insert"
              
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Name", model.Name);
                   paramCollection.AddWithValue("@Email", model.Email);
                   paramCollection.AddWithValue("@PhoneNumber", model.PhoneNumber);
                   paramCollection.AddWithValue("@Subject", model.Subject);
                   paramCollection.AddWithValue("@Message", model.Message);

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
    }
}
