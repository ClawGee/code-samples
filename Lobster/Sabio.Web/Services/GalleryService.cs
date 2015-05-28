using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Models.Requests;
using Sabio.Web.Domain;

namespace Sabio.Web.Services
{
    public class GalleryService : BaseService
    {
        public static int Insert(GalleryRequest model, string loggedInUser, EntityIdentifiers entityIdentity)
        {
            int id = 0;

            int currentUserId = UserService.ConvertGuid(loggedInUser);

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppGalleries_Insert"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

                   paramCollection.AddWithValue("@Title", model.title);
                   paramCollection.AddWithValue("@Description", model.description);
                   paramCollection.AddWithValue("@IsPublic", model.isPublic);
                   paramCollection.AddWithValue("@EntityType", entityIdentity.EntityType);
                   paramCollection.AddWithValue("@EntityId", entityIdentity.EntityId);
                   paramCollection.AddWithValue("@AppUserId", currentUserId);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   int.TryParse(param["@Id"].Value.ToString(), out id);
               }
               );


            return id;
        }

        public static void Update(GalleryRequest model, string loggedInUser)
        {
            int currentUserId = UserService.ConvertGuid(loggedInUser);

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppGalleries_Update"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Title", model.title);
                   paramCollection.AddWithValue("@Description", model.description);
                   paramCollection.AddWithValue("@IsPublic", model.isPublic);
                   paramCollection.AddWithValue("@AppUserId", currentUserId);

               }, returnParameters: delegate(SqlParameterCollection param)
               {

               }
               );
        }

        public static Gallery GetGallerybyId(int galleryId)
        {
            Gallery row = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppGalleries_SelectbyGalleryId"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", galleryId);
                }
                , map: delegate(IDataReader reader, short set)
                {
                    row = new Gallery();
                    int startingIndex = 0;
              
                    row.Title = reader.GetSafeString(startingIndex++);
                    row.Description = reader.GetSafeString(startingIndex++);
                    row.CreatedDate = reader.GetSafeDateTime(startingIndex++);
                    row.IsPublic = reader.GetSafeBool(startingIndex++);
                    row.EntityType = reader.GetSafeInt32(startingIndex++);
                    row.EntityId = reader.GetSafeInt32(startingIndex++);
                    row.Deleted = reader.GetSafeBool(startingIndex++);
                   
                }
                );
            return row;
        }

        public static List<Media> GetGalleryMedias(int galleryId)
        {
            List<Media> galleryList = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppGalleriesMedia_Select",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@GalleryId", galleryId);
                }
               , map: delegate(IDataReader reader, short set)
               {
                   Media row = new Media();
                   int startingIndex = 0; //startingOrdinal

                   row.Id = reader.GetSafeInt32(startingIndex++);
                   row.Uid = reader.GetSafeGuid(startingIndex++);
                   row.FileName = reader.GetSafeString(startingIndex++);
                   row.CreateDate = reader.GetSafeDateTime(startingIndex++);
                   row.ContentType = reader.GetSafeString(startingIndex++);
                   row.Deleted = reader.GetSafeBool(startingIndex++);
                   row.EntityType = reader.GetSafeInt32(startingIndex++);
                   row.EntityId = reader.GetSafeInt32(startingIndex++);
                   row.MediaType = reader.GetSafeInt32(startingIndex++);
                   if (row.FileName != null)
                   {
                       row.FullUrl = FileIoService.GetGalleryMediaURL(row.FileName);
                   }

                   if (galleryList == null)
                   {
                       galleryList = new List<Media>();
                   }

                   galleryList.Add(row);
               }
               );
            return galleryList;
        }

        public static List<Gallery> Select(string loggedInUser)
        {
            List<Gallery> galleryList = null;

            int currentUserId = UserService.ConvertGuid(loggedInUser);
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppGalleries_Select"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AppUserId", currentUserId);
                }
                , map: delegate(IDataReader reader, short set)
                {
                    Gallery row = new Gallery();
                    int startingIndex = 0;
                    row.Id = reader.GetInt32(startingIndex++);
                    row.Title = reader.GetSafeString(startingIndex++);
                    row.Description = reader.GetSafeString(startingIndex++);
                    row.CreatedDate = reader.GetSafeDateTime(startingIndex++);
                    row.IsPublic = reader.GetSafeBool(startingIndex++);
                    row.EntityType = reader.GetSafeInt32(startingIndex++);
                    row.EntityId = reader.GetSafeInt32(startingIndex++);
                    row.Deleted = reader.GetSafeBool(startingIndex++);

                    if (galleryList == null)
                    {
                        galleryList = new List<Gallery>();
                    }

                    galleryList.Add(row);
                }
                );
            return galleryList;
        }
    }
}