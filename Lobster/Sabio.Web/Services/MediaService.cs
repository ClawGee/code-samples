using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Web.Models.Requests;
using Sabio.Data;


namespace Sabio.Web.Services
{
    public class MediaService : BaseService
    {
        public static Guid CreateMedia(string fileName, int entityType, string user, string fileType, int entityId)
        {
            Guid uid = Guid.Empty;//000-0000-0000-0000
            int appUserId = UserService.ConvertGuid(user);
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppMedia_AddDelete"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@FileName", fileName);
                   paramCollection.AddWithValue("@AppUserId", appUserId);
                   paramCollection.AddWithValue("@EntityType", entityType);
                   paramCollection.AddWithValue("@MediaType", fileType);
                   paramCollection.AddWithValue("@EntityId", entityId);
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

        public static Media SelectMedia(string user,int entityType,int entityId)
        {
            Media m = new Media();
            var AppUserId = UserService.ConvertGuid(user);
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppMedia_Select"
           , inputParamMapper: delegate(SqlParameterCollection paramCollection)
           {
               paramCollection.AddWithValue("@AppUserId", AppUserId);
               paramCollection.AddWithValue("@EntityType", entityType);
               paramCollection.AddWithValue("@EntityId", entityId);
           }
           , map: delegate(IDataReader reader, short set)
           {
               int startingIndex = 0;
               m.FileName = reader.GetString(startingIndex++);
               m.CreateDate = reader.GetSafeDateTime(startingIndex++);

           }
           );
            return m;
        }

        public static Media SelectMediaByMediaId(int MediaId)
        {
            Media userPhotoInfo = new Media();
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppMedia_SelectByMediaId"
           , inputParamMapper: delegate(SqlParameterCollection paramCollection)
           {
               paramCollection.AddWithValue("@MediaId", MediaId);
           }
           , map: delegate(IDataReader reader, short set)
           {
               int startingIndex = 0;
               userPhotoInfo.FileName = reader.GetString(startingIndex++);
               userPhotoInfo.ContentType = reader.GetString(startingIndex++);
               userPhotoInfo.EntityType = reader.GetInt32(startingIndex++);
               userPhotoInfo.CreateDate = reader.GetSafeDateTime(startingIndex++);
               userPhotoInfo.Deleted = reader.GetSafeBool(startingIndex++);
               userPhotoInfo.MediaType = reader.GetSafeInt32(startingIndex++);
               if ((userPhotoInfo.FileName != null) && (userPhotoInfo.EntityType > 0))
               {
                   userPhotoInfo.FullUrl = FileIoService.FileHelperGet(userPhotoInfo.EntityType, userPhotoInfo.FileName);
               }
               else
               {
                   userPhotoInfo = null;
               }
           }
           );
            return userPhotoInfo;
        }

        public static Guid CreateGalleryMedia(string fileName, int entityType, string user, string fileType, int entityId, int galleryId)
        {
            Guid uid = Guid.Empty;//000-0000-0000-0000
            int appUserId = UserService.ConvertGuid(user);
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppMedia_InsertGalleryImage"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@FileName", fileName);
                   paramCollection.AddWithValue("@AppUserId", appUserId);
                   paramCollection.AddWithValue("@EntityType", entityType);
                   paramCollection.AddWithValue("@ContentType", fileType);
                   paramCollection.AddWithValue("@EntityId", entityId);
                   paramCollection.AddWithValue("@GalleryId", galleryId);
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

        public static void DeleteImage(int mediaId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "AppMedia_DeleteMediaById"
                  , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                  {
                      paramCollection.AddWithValue("@MediaId", mediaId);

                  }, returnParameters: delegate(SqlParameterCollection param)
                  {
                  }
                  );
        }
    }
}
