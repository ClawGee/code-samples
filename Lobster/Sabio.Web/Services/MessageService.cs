using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Models.Requests;
using Sabio.Web.Domain.Tests;

namespace Sabio.Web.Services
{
    public class MessageService : BaseService
    {

        public static ConversationMessagePair InsertMessage(MessageCreateRequest model, string CurrentUserId)
        {
            int loggedInUser = UserService.ConvertGuid(CurrentUserId);
            int recipientUser = UserService.ConvertGuid(model.RecipientId);
            ConversationMessagePair outputPair = null;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsers_InitializeConversation"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", loggedInUser);
                   paramCollection.AddWithValue("@RecipientId", recipientUser);
                   paramCollection.AddWithValue("@Subject", model.Subject);
                   paramCollection.AddWithValue("@Body", model.Body);

                   SqlParameter p1 = new SqlParameter("@ConversationId", System.Data.SqlDbType.Int);
                   p1.Direction = System.Data.ParameterDirection.Output;
                   paramCollection.Add(p1);

                   SqlParameter p2 = new SqlParameter("@MessageId", System.Data.SqlDbType.Int);
                   p2.Direction = System.Data.ParameterDirection.Output;
                   paramCollection.Add(p2);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   outputPair = new ConversationMessagePair();
                   outputPair.ConversationId = Convert.ToInt32(param["@ConversationId"].Value);
                   outputPair.MessageId = Convert.ToInt32(param["@MessageId"].Value);
               }
            );
            return outputPair;
        }

        public static Message InsertReply(ReplyCreateRequest model, string CurrentUserId)
        {
            int loggedInUser = UserService.ConvertGuid(CurrentUserId);
            Message outputId = new Message();

            DataProvider.ExecuteNonQuery(GetConnection, "AspNetUsers_InitializeReply"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", loggedInUser);
                   paramCollection.AddWithValue("@Subject", model.Subject);
                   paramCollection.AddWithValue("@Body", model.Body);
                   paramCollection.AddWithValue("@ConversationId", model.ConversationId);

                   SqlParameter p1 = new SqlParameter("@MessageId", System.Data.SqlDbType.Int);
                   p1.Direction = System.Data.ParameterDirection.Output;
                   paramCollection.Add(p1);
               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   outputId.MessageId = Convert.ToInt32(param["@MessageId"].Value);
               }
             );
            return outputId;
        }
    

        public static List<Message> GetConversationThread(int ConversationId)
        {
            List<Message> messageList = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AspNetUsers_ConversationSelect",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ConversationID", ConversationId);
               }
               , map: delegate(IDataReader reader, short set)
               {                
                    Message MessageRow = new Message();
                    int startingIndex = 0; //startingOrdinal

                    MessageRow.MessageId = reader.GetSafeInt32(startingIndex++);
                    MessageRow.ConversationId = reader.GetSafeInt32(startingIndex++);
                    MessageRow.CreateDate = reader.GetSafeDateTime(startingIndex++, DateTimeKind.Utc);
                    MessageRow.Subject = reader.GetSafeString(startingIndex++);
                    MessageRow.Body = reader.GetSafeString(startingIndex++);
                    MessageRow.Deleted = reader.GetSafeBool(startingIndex++);
                    MessageRow.AppUserId = reader.GetSafeInt32(startingIndex++);
                    MessageRow.RecipientId = reader.GetSafeInt32(startingIndex++);
                    if (messageList == null)
                        {
                            messageList = new List<Message>();
                        }
                    messageList.Add(MessageRow);
                }
            );
            return messageList;
        }

        public static List<Conversation> GetConversationsByUserId(int AppUserId)
        {
            List<Conversation> conversationList = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AspNetUsers_UserConversationsSelect",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AppUserId", AppUserId);
                }
               , map: delegate(IDataReader reader, short set)
               {
                   Conversation ConversationRow = new Conversation();
                   int startingIndex = 0; //startingOrdinal

                   ConversationRow.Id = reader.GetSafeInt32(startingIndex++);
                   //ConversationRow.Subject = reader.GetSafeString(startingIndex++);
                   ConversationRow.RecipientId = reader.GetSafeInt32(startingIndex++);
                   ConversationRow.CreateDate = reader.GetSafeDateTime(startingIndex++, DateTimeKind.Utc);
                   ConversationRow.AppUserId = reader.GetSafeInt32(startingIndex++);

                   if (conversationList == null)
                   {
                       conversationList = new List<Conversation>();
                   }

                   conversationList.Add(ConversationRow);
               }
            );
            return conversationList;
        }

        public static List<Conversation> GetConversationsByRecipientId(int RecipientId)
        {
            List<Conversation> conversationList = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AspNetUsers_RecipientConversationsSelect",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@RecipientId", RecipientId);
                }
               , map: delegate(IDataReader reader, short set)
               {
                   Conversation ConversationRow = new Conversation();
                   int startingIndex = 0; //startingOrdinal

                   ConversationRow.Id = reader.GetSafeInt32(startingIndex++);
                   //ConversationRow.Subject = reader.GetSafeString(startingIndex++);
                   ConversationRow.RecipientId = reader.GetSafeInt32(startingIndex++);
                   ConversationRow.CreateDate = reader.GetSafeDateTime(startingIndex++, DateTimeKind.Utc);
                   ConversationRow.AppUserId = reader.GetSafeInt32(startingIndex++);

                   if (conversationList == null)
                   {
                       conversationList = new List<Conversation>();
                   }

                   conversationList.Add(ConversationRow);
               }
            );
            return conversationList;
        }

        public static ConvoMetaData GetConvoMetaDataById(int ConversationId)
        {
            ConvoMetaData ConvoMetaDataRow = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AspNetUsers_ConversationMetaDataSelect",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ConversationId", ConversationId);
                }
               , map: delegate(IDataReader reader, short set)
               {
                   ConvoMetaDataRow = new ConvoMetaData();
                   int startingIndex = 0; //startingOrdinal

                   ConvoMetaDataRow.ConversationId = reader.GetSafeInt32(startingIndex++);
                   ConvoMetaDataRow.CreateDate = reader.GetSafeDateTime(startingIndex++, DateTimeKind.Utc);
                   ConvoMetaDataRow.AppUserId = reader.GetSafeInt32(startingIndex++);
                   ConvoMetaDataRow.RecipientId = reader.GetSafeInt32(startingIndex++);
                   ConvoMetaDataRow.Recipient = new FullUser();
                   ConvoMetaDataRow.Recipient.Email = reader.GetSafeString(startingIndex++);
                   ConvoMetaDataRow.Recipient.PhoneNumber = reader.GetSafeString(startingIndex++);
                   ConvoMetaDataRow.Recipient.UserName = reader.GetSafeString(startingIndex++);
                   ConvoMetaDataRow.Recipient.LastName = reader.GetSafeString(startingIndex++);
                   ConvoMetaDataRow.Recipient.FirstName = reader.GetSafeString(startingIndex++);
                   ConvoMetaDataRow.Recipient.UserType = reader.GetSafeInt32(startingIndex++);
                   int RecipientMediaId = reader.GetSafeInt32(startingIndex++);
                   if (RecipientMediaId > 0 ) 
                   {
                       ConvoMetaDataRow.Recipient.Avatar = MediaService.SelectMediaByMediaId(RecipientMediaId);
                   }
                   ConvoMetaDataRow.Sender= new FullUser();
                   ConvoMetaDataRow.Sender.Email = reader.GetSafeString(startingIndex++);
                   ConvoMetaDataRow.Sender.PhoneNumber = reader.GetSafeString(startingIndex++);
                   ConvoMetaDataRow.Sender.UserName = reader.GetSafeString(startingIndex++);
                   ConvoMetaDataRow.Sender.LastName = reader.GetSafeString(startingIndex++);
                   ConvoMetaDataRow.Sender.FirstName = reader.GetSafeString(startingIndex++);
                   ConvoMetaDataRow.Sender.UserType = reader.GetSafeInt32(startingIndex++);
                   int SenderMediaId = reader.GetSafeInt32(startingIndex++);
                   if (SenderMediaId > 0)
                   {
                       ConvoMetaDataRow.Sender.Avatar = MediaService.SelectMediaByMediaId(SenderMediaId);
                   }
                }
            );
            return ConvoMetaDataRow;
        }

    }
}