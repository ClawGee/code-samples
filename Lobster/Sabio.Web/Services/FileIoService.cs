using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Models.Requests;
using Sabio.Web.Services;
using System.Configuration;
using Sabio.Web.Models.Responses;
using System.Net;

namespace Sabio.Web.Services
{

    public class FileIoService : BaseService
    {

        private static string _AccessKey = 
        private static string _SecretKey = 
        private static string _Prefix = 
        private static string _BucketName =
        private static string _BaseUrl = 

        public static void UploadFileToS3(string localFileName, string remotePath, string fileType)
        {
            var client = Amazon.AWSClientFactory.CreateAmazonS3Client(_AccessKey, _SecretKey, RegionEndpoint.USEast1);
            var uploadRequest = new TransferUtilityUploadRequest
            {
                FilePath = localFileName,
                BucketName = _BucketName,
                CannedACL = S3CannedACL.PublicRead,
                Key = _Prefix + remotePath,
                ContentType = fileType
            };

            var fileTransferUtility = new TransferUtility(client);
            fileTransferUtility.Upload(uploadRequest);
        }

        //public static GetFileFromS3(string remotePath, string, fileName);
        public static string FileHelperGet(int entityType, string postedFile)
        {

            string mediaPath = "";
            switch (entityType)
            {
                case 4:
                    mediaPath = "media/user/images/";
                    break;
                case 1:
                    mediaPath = "media/developer/images/";
                    break;
                case 2:
                    mediaPath = "media/recruiter/images/";
                    break;
                case 3:
                    mediaPath = "media/employer/images/";
                    break;
                default: throw new Exception("Entity type you passed is not valid.  Options are: User, Developer, Recruiter, or Employer");
            }
            string FullPath = _BaseUrl + _Prefix + mediaPath + postedFile;
            return FullPath;
        }

        public static string GetGalleryMediaURL(string postedFile)
        {
            string mediaPath = "media/gallery/images/";
            string FullPath = _BaseUrl + _Prefix + mediaPath + postedFile;
            return FullPath;
        }
    }
}
