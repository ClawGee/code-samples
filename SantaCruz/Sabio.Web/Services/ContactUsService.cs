using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Models.Requests;
using Sabio.Web.Services;

namespace Sabio.Web.ContactUs
{
    public class ContactUsService : BaseService
    {

        public static int Create(ContactUsRequestModel model)
        {
            //wiki documented - service layer - search "data provider"
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppContactUs_Insert"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                 


                   paramCollection.AddWithValue("@Name", model.Name);
                   paramCollection.AddWithValue("@Email", model.Email);
                   paramCollection.AddWithValue("@Subject", model.Subject);
                   paramCollection.AddWithValue("@Message", model.Message);
                 


                   SqlParameter p = new SqlParameter("@id", System.Data.SqlDbType.Int);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   int.TryParse(param["@id"].Value.ToString(), out id);
                   

               }
               );


            return id;
        }

      

    }


}
