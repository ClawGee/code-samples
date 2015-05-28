using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Sabio.Web.Domain;
using Sabio.Web.Services;
using Sabio.Data;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Models.Requests;

namespace Sabio.Web.Services
{
    public class EventsService : BaseService
    {


        public static Guid Insert(EventsInsertRequest model, string currentUserId)// 
        {
            int appUserId = UserService.ConvertGuid(currentUserId);
            Guid uid = Guid.Empty;//000-0000-0000-0000 

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppEvents_Insert"

               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", appUserId);
                   paramCollection.AddWithValue("@StartDate", model.StartDate);
                   paramCollection.AddWithValue("@EndDate", model.EndDate);
                   paramCollection.AddWithValue("@StartTime ", model.StartTime);
                   paramCollection.AddWithValue("@EndTime", model.EndTime);
                   paramCollection.AddWithValue("@AllDay", model.AllDay);
                   paramCollection.AddWithValue("@Title", model.Title);
                   paramCollection.AddWithValue("@Description", model.Description);

                   SqlParameter p = new SqlParameter("@Uid", System.Data.SqlDbType.UniqueIdentifier);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);
               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   Guid.TryParse(param["@Uid"].Value.ToString(), out uid);
               }


               );
            //post to address table in DB:
            //instanciate a new AddressCreateRequest Model
            AddressCreateRequest eventAddress = new AddressCreateRequest();
            eventAddress.AddressLine1 = model.AddressLine1;
            eventAddress.AddressLine2 = model.AddressLine2;
            eventAddress.City = model.City;
            eventAddress.State = model.State;
            eventAddress.Zip = model.Zip;
            eventAddress.Lat = model.Lat;
            eventAddress.Lng = model.Lng;

            //take 'eventAddress' model and take to call AddressService.InsertAddress Service:
            Guid EventsAddressGuidResponse = AddressService.InsertAddress(eventAddress, currentUserId);
            EventsService.InsertEventAddress(uid, EventsAddressGuidResponse);

            return uid;
        }
        //another function to call the mapper proc:
        public static void InsertEventAddress(Guid uid, Guid EventsAddressGuidResponse)
        {
            DataProvider.ExecuteNonQuery(
                GetConnection
                , "dbo.AppEventsAddresses_Insert"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                    {
                        paramCollection.AddWithValue("@eventGuid", uid);
                        paramCollection.AddWithValue("@addressGuid", EventsAddressGuidResponse);

                    }, returnParameters: delegate(SqlParameterCollection param)
                {
                    //no output/void.
                }
            );

        }
 //*********Start of Update/Insert Event**************
        public static void Update(EventsUpdateRequest model, Guid uid, string currentUserId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppEvents_Update"

               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@StartDate", model.StartDate);
                   paramCollection.AddWithValue("@EndDate", model.EndDate);
                   paramCollection.AddWithValue("@StartTime", model.StartTime);
                   paramCollection.AddWithValue("@EndTime", model.EndTime);
                   paramCollection.AddWithValue("@AllDay", model.AllDay);
                   paramCollection.AddWithValue("@Title", model.Title);
                   paramCollection.AddWithValue("@Description", model.Description);
                   paramCollection.AddWithValue("@Uid", uid);
               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   //This does not need to return anything.
               }
               );
            //post to address table in DB:
            ////instanciate a new addresscreaterequest Model
            AddressCreateRequest eventAddress = new AddressCreateRequest();
            eventAddress.AddressLine1 = model.AddressLine1;
            eventAddress.AddressLine2 = model.AddressLine2;
            eventAddress.City = model.City;
            eventAddress.State = model.State;
            eventAddress.Zip = model.Zip;
            eventAddress.Lat = model.Lat;
            eventAddress.Lng = model.Lng;
            Guid addressGuid = model.AddressGuid;
            
            //take 'eventAddress' model and take to call AddressService.InsertAddress Service:
            AddressService.UpdateAddress(eventAddress, addressGuid, currentUserId);
           
        }
//*********End of Update/Insert Event**************


        public static Event GetEventByUid(Guid uid)
        {
            Event selectedEvent = new Event();
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppEvents_SelectByUid",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Uid", uid);
                },
                map: delegate(IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    selectedEvent.Uid = reader.GetGuid(startingIndex++);
                    selectedEvent.StartDate = reader.GetString(startingIndex++);
                    selectedEvent.EndDate = reader.GetString(startingIndex++);
                    selectedEvent.StartTime = reader.GetString(startingIndex++);
                    selectedEvent.EndTime = reader.GetString(startingIndex++);
                    selectedEvent.AllDay = reader.GetString(startingIndex++);
                    selectedEvent.Title = reader.GetString(startingIndex++);
                    selectedEvent.Description = reader.GetSafeString(startingIndex++);
                    //bring in/use address domain model in current event model, and intialize it below:
                    selectedEvent.EventAddress = new Address();
                    selectedEvent.EventAddress.AddressLine1 = reader.GetSafeString(startingIndex++);
                    selectedEvent.EventAddress.AddressLine2 = reader.GetSafeString(startingIndex++);
                    selectedEvent.EventAddress.City = reader.GetSafeString(startingIndex++);
                    //selectedEvent.EventAddress.State = reader.GetSafeInt16(startingIndex++);
                    selectedEvent.EventAddress.Zip = reader.GetSafeInt32(startingIndex++);
                    selectedEvent.EventAddress.Lat = reader.GetDecimal(startingIndex++);
                    selectedEvent.EventAddress.Lng = reader.GetDecimal(startingIndex++);
                    selectedEvent.EventAddress.Uid = reader.GetGuid(startingIndex++);
                }
           );
            return selectedEvent;
        }
        public static List<Event> GetEventsByAppUserId(String CurrentUserId)
        {
            int AppUserId = UserService.ConvertGuid(CurrentUserId);
            FullUser User = UserService.GetFullUserById(CurrentUserId);
            List<Event> listOfEvents = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppEvents_SelectByAppUserId",
            inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AppUserId", AppUserId);
                },
                map: delegate(IDataReader reader, short set)
                {
                    Event selectedEvent = new Event();
                    int startingIndex = 0;
                    selectedEvent.Uid = reader.GetGuid(startingIndex++);
                    selectedEvent.StartDate = reader.GetSafeString(startingIndex++);
                    selectedEvent.EndDate = reader.GetSafeString(startingIndex++);
                    selectedEvent.StartTime = reader.GetSafeString(startingIndex++);
                    selectedEvent.EndTime = reader.GetSafeString(startingIndex++);
                    selectedEvent.AllDay = reader.GetSafeString(startingIndex++);
                    selectedEvent.Title = reader.GetSafeString(startingIndex++);
                    selectedEvent.Description = reader.GetSafeString(startingIndex++);
                    //selectedEvent.Latitude = reader.GetSafeString(startingIndex++);
                    //selectedEvent.Longitude = reader.GetSafeString(startingIndex++);

                    if (listOfEvents == null)
                    {
                        listOfEvents = new List<Event>();
                    }

                    listOfEvents.Add(selectedEvent);
                }
            );
            return listOfEvents;
        }
    }
}