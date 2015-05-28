using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System.Data.SqlClient;
using System.Data;
using Sabio.Data;
using Sabio.Web.Services;

namespace Sabio.Web.Services
{
    public class ForgotPasswordService : BaseService
    {
        //this class just runs the storeproc and returns a token
        public static Guid InsertTokenUsingEmail(ForgotPasswordRequestModel model)
        {

            Guid token = Guid.Empty;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.ForgotPasswordToken_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Email", model.Email);

                    SqlParameter p = new SqlParameter("@ForgotPasswordToken", System.Data.SqlDbType.UniqueIdentifier);
                    p.Direction = System.Data.ParameterDirection.Output;
                    
                    paramCollection.Add(p);

                },

                returnParameters: delegate (SqlParameterCollection param)
                {
                    Guid.TryParse(param["@ForgotPasswordToken"].Value.ToString(), out token);
                }
            );

            return token;
                
        }

        public static ForgotPasswordUserDomainModel FindUserUsingTokenSelect(string token)
        {

            ForgotPasswordUserDomainModel p = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.ForgotPasswordUser_Select",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ForgotPasswordToken", token);
                },
                map: delegate(IDataReader reader, short set)
                {
                    p = new ForgotPasswordUserDomainModel();

                    int startingIndex = 0;
                    p.Email = reader.GetSafeString(startingIndex++);
                    p.FirstName = reader.GetSafeString(startingIndex++);


                }
                
                );

            return p;
        }


    }

}