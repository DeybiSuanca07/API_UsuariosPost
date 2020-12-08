using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using CDataAccess.Interface;
using CModels.Context;
using CModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CDataAccess.DataAccess
{
    public class DaPost : IPost
    {
        private UsersContext usersContext_;
        private readonly IConfiguration config_;
        private readonly IAmazonS3 amazonS3;

        public DaPost(UsersContext usersContext, IAmazonS3 amazonS3, IConfiguration config)
        {
            this.usersContext_ = usersContext;
            this.amazonS3 = amazonS3;
            config_ = config;
        }

        public async Task<bool> CreatePost(string title, string content, IFormFile img, int UserId)
        {
            try
            {
                var routeImg = "";
                int validateFolder = 1;
                while (validateFolder == 1)
                {
                    if (!Directory.Exists("temp"))
                    {

                        DirectoryInfo di = Directory.CreateDirectory("temp");
                        DirectoryInfo dInfo = new DirectoryInfo("temp");
                        DirectorySecurity dSecurity = dInfo.GetAccessControl();
                        dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                        dInfo.SetAccessControl(dSecurity);
                        validateFolder = 1;
                    }
                    else
                    {
                        routeImg = Path.Combine("temp", img.FileName);
                        using (var stream = new FileStream(routeImg, FileMode.Create, FileAccess.ReadWrite))
                        {
                            img.CopyTo(stream);
                        }
                        validateFolder = 2;

                    }
                }
                Posts post = new Posts();
                post.Title = title;
                post.Content = content;
                post.CreationDate = DateTime.Now;
                post.UserId = UserId;
                post.Img = routeImg;

                //string bucketName = config_.GetValue<string>("BucketS3");

                var putRequest = new PutObjectRequest()
                {
                    BucketName = config_.GetValue<string>("BucketS3"),
                    Key = img.FileName,
                    InputStream = img.OpenReadStream(),
                    ContentType = img.ContentType
                };

                var result = await this.amazonS3.PutObjectAsync(putRequest);

                usersContext_.Posts.Add(post);
                int response = usersContext_.SaveChanges();
                if (response > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
