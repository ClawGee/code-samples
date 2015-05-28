using Sabio.Data;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services
{
    public class MediaService : BaseService
    {
         
        public static int UploadInsert(UploadInsertRequestModel model, string currentUserId)
        {

            int appUserId = UserService.GetAppUserId(currentUserId);
            int mediaId = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppMedia_Insert",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@filename", model.Filename);
                paramCollection.AddWithValue("@entitytype", model.EntityType);
                paramCollection.AddWithValue("@entityid", model.EntityId);
                paramCollection.AddWithValue("@mediatype", model.MediaType);
                paramCollection.AddWithValue("@contenttype", model.ContentType);
                paramCollection.AddWithValue("@AppUserID", appUserId);

                SqlParameter p = new SqlParameter("@mediaId", System.Data.SqlDbType.Int);
                p.Direction = System.Data.ParameterDirection.Output;

                paramCollection.Add(p);
            }
            , returnParameters: delegate(SqlParameterCollection param)
            {

                mediaId = int.Parse(param["@mediaId"].Value.ToString());

            });
            
            return mediaId;

        }
    }
}