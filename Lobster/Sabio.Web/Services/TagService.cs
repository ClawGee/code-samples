using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Web.Models.Requests;
using Sabio.Data;
using System.Diagnostics;


namespace Sabio.Web.Services
{
    public class TagService : BaseService
    {
        public static void Upsert(string[] tags, int entityId, int entityType)
        {
            InsertTags(tags);
            DeleteEntityTags(entityId, entityType);
            InsertEntityTags(tags, entityId, entityType);

        }
        public static void InsertTags(string[] tags)
        {

            if (tags != null)
            {
                for (int i = 0; i < tags.Length; i++)
                {
                    string tag = UtilityService.GenerateSlug(tags[i]);
                    Debug.WriteLine(tags[i]);
                    DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppTag_Insert"
                       , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                       {
                           paramCollection.AddWithValue("@Name", tag);

                       }, returnParameters: delegate(SqlParameterCollection param)
                       {

                       }
                       );
                }
            }
        }


        public static void DeleteEntityTags(int entityId, int entityType)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppEntityTag_Delete"
                  , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                  {
                      paramCollection.AddWithValue("@EntityId", entityId);
                      paramCollection.AddWithValue("@EntityType", entityType);

                  }, returnParameters: delegate(SqlParameterCollection param)
                  {
                  }
                  );
        }

        public static void InsertEntityTags(string[] tags, int entityId, int entityType)
        {
            if (tags != null)
            {
                for (int i = 0; i < tags.Length; i++)
                {
                    string tag = UtilityService.GenerateSlug(tags[i]);
                    DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppEntityTag_Insert"
                       , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                       {
                           paramCollection.AddWithValue("@Name", tag);
                           paramCollection.AddWithValue("@EntityId", entityId);
                           paramCollection.AddWithValue("@EntityType", entityType);

                       }, returnParameters: delegate(SqlParameterCollection param)
                       {
                       }
                       );
                }
            }
        }

        public static List<Tag> GetTagList(string user, int EntityId, int EntityType)
        {
            List<Tag> list = new List<Tag>();
            int AppUserId = UserService.ConvertGuid(user);
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppEntityTag_Select"
           , inputParamMapper: delegate(SqlParameterCollection paramCollection)
           {
               paramCollection.AddWithValue("@EntityId", EntityId);
               paramCollection.AddWithValue("@EntityType", EntityType);
           }
           , map: delegate(IDataReader reader, short set)
           {
               Tag t = MapTag(reader);
               list.Add(t);
           }
           );
            return list;
        }


        public static List<Tag> GetTagList()
        {
            List<Tag> list = new List<Tag>();
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppTag_SelectAll"
           , inputParamMapper: delegate(SqlParameterCollection paramCollection)
           {
           }
           , map: delegate(IDataReader reader, short set)
           {
               Tag t = MapTag(reader);
               list.Add(t);
           }
           );
            return list;
        }



        internal static Tag MapTag(IDataReader reader)
        {
            Tag t = new Tag();
            int startingIndex = 0;
            t.Name = reader.GetSafeString(startingIndex++);
            return t;
        }
    }
}
