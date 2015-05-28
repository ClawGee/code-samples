using System.Data;
using System.Data.SqlClient;
using Sabio.Data;
using System;


namespace Sabio.Web.Services
{
    public class GetAppUserIdService : BaseService
    {
        public static int ConvertGuid(string guid)
        {
            int userId = 0;
            DataProvider.ExecuteCmd(GetConnection, "dbo.AspNetUserId_Select"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
	                paramCollection.AddWithValue("@Uid", guid);
               }
               , map: delegate(IDataReader reader, short set)
               {
                   userId = reader.GetSafeInt32(0);
               }
               );
            return userId;
        }
    }
}
