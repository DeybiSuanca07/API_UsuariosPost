﻿using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using CDataAccess.Interface;
using CModels.Context;
using CModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
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
        static IAmazonS3 client_;

        public DaPost(UsersContext usersContext, IAmazonS3 client, IConfiguration config)
        {
            this.usersContext_ = usersContext;
            client_ = client;
            config_ = config;
        }


        public int GetPostsCount(int UserId)
        {
            try
            {
                var result = usersContext_.Posts.Where(i => i.UserId == UserId).Count();
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<List<Posts>> GetPosts(int UserId, int pagInicial = 0, string title = "", string content = "")
        {
            try
            {
                
                var result = usersContext_.Posts.Where(i => i.UserId == UserId).ToList();
                if (title == "" && content != "")
                {
                    if (pagInicial == 0)
                    {
                        result = result.Take(10).Where(t => t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 10)
                    {
                        result = result.Skip(10).Take(10).Where(t => t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 20)
                    {
                        result = result.Skip(20).Take(10).Where(t => t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 30)
                    {
                        result = result.Skip(30).Take(10).Where(t => t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 40)
                    {
                        result = result.Skip(40).Take(10).Where(t => t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 50)
                    {
                        result = result.Skip(50).Take(10).Where(t => t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 60)
                    {
                        result = result.Skip(60).Take(10).Where(t => t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 70)
                    {
                        result = result.Skip(70).Take(10).Where(t => t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 80)
                    {
                        result = result.Skip(80).Take(10).Where(t => t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 90)
                    {
                        result = result.Skip(100).Take(10).Where(t => t.Content.Contains(content)).ToList();
                    }
                }
                else if (title != "" && content == "")
                {
                    if (pagInicial == 0)
                    {
                        result = result.Take(10).Where(t => t.Title.Contains(title)).ToList();
                    }
                    else if (pagInicial == 10)
                    {
                        result = result.Skip(10).Take(10).Where(t => t.Title.Contains(title) || t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 20)
                    {
                        result = result.Skip(20).Take(10).Where(t => t.Title.Contains(title) || t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 30)
                    {
                        result = result.Skip(30).Take(10).Where(t => t.Title.Contains(title) || t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 40)
                    {
                        result = result.Skip(40).Take(10).Where(t => t.Title.Contains(title) || t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 50)
                    {
                        result = result.Skip(50).Take(10).Where(t => t.Title.Contains(title) || t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 60)
                    {
                        result = result.Skip(60).Take(10).Where(t => t.Title.Contains(title) || t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 70)
                    {
                        result = result.Skip(70).Take(10).Where(t => t.Title.Contains(title) || t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 80)
                    {
                        result = result.Skip(80).Take(10).Where(t => t.Title.Contains(title) || t.Content.Contains(content)).ToList();
                    }
                    else if (pagInicial == 90)
                    {
                        result = result.Skip(100).Take(10).Where(t => t.Title.Contains(title) || t.Content.Contains(content)).ToList();
                    }
                }
                else if (title == "" && content == "")
                {
                    if (pagInicial == 0)
                    {
                        result = result.Take(10).ToList();
                    }
                    else if (pagInicial == 10)
                    {
                        result = result.Skip(10).Take(10).ToList();
                    }
                    else if (pagInicial == 20)
                    {
                        result = result.Skip(20).Take(10).ToList();
                    }
                    else if (pagInicial == 30)
                    {
                        result = result.Skip(30).Take(10).ToList();
                    }
                    else if (pagInicial == 40)
                    {
                        result = result.Skip(40).Take(10).ToList();
                    }
                    else if (pagInicial == 50)
                    {
                        result = result.Skip(50).Take(10).ToList();
                    }
                    else if (pagInicial == 60)
                    {
                        result = result.Skip(60).Take(10).ToList();
                    }
                    else if (pagInicial == 70)
                    {
                        result = result.Skip(70).Take(10).ToList();
                    }
                    else if (pagInicial == 80)
                    {
                        result = result.Skip(80).Take(10).ToList();
                    }
                    else if (pagInicial == 90)
                    {
                        result = result.Skip(100).Take(10).ToList();
                    }
                }

                if (result.Count > 0)
                {
                    string folderUser = Path.Combine("temp", UserId.ToString());
                    List<string> files = Directory.GetFiles(folderUser, "*").ToList();
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> CreatePost(string title, string content, IFormFile img, int UserId)
        {
            try
            {
                string folderRoot = "temp";
                string nameImg = "";
                string routeImg = "";
                int validateFolder = 0;
                while (validateFolder == 0)
                {
                    if (!Directory.Exists(folderRoot))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(folderRoot);
                        DirectoryInfo dInfo = new DirectoryInfo(folderRoot);
                        DirectorySecurity dSecurity = dInfo.GetAccessControl();
                        dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                        dInfo.SetAccessControl(dSecurity);
                        validateFolder = 0;
                    }
                    else
                    {
                        string folderUser = Path.Combine(folderRoot, UserId.ToString());

                        int validateFolder2 = 0;
                        while (validateFolder2 == 0)
                        {
                            if (!Directory.Exists(folderUser))
                            {
                                DirectoryInfo di = Directory.CreateDirectory(folderUser);
                                DirectoryInfo dInfo = new DirectoryInfo(folderUser);
                                DirectorySecurity dSecurity = dInfo.GetAccessControl();
                                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                                dInfo.SetAccessControl(dSecurity);
                                validateFolder2 = 0;
                            }
                            else
                            {
                                validateFolder2 = 1;
                            }
                        }

                        int countPosts = usersContext_.Posts.Where(i => i.UserId == UserId).Count();
                        nameImg = "id" + countPosts + "-" + img.FileName;
                        routeImg = Path.Combine(folderRoot, UserId.ToString(), nameImg);
                        using (var stream = new FileStream(routeImg, FileMode.Create, FileAccess.ReadWrite))
                        {
                            img.CopyTo(stream);

                        }
                        validateFolder = 1;
                    }
                }

                byte[] imgArray = File.ReadAllBytes(routeImg);
                string base64 = Convert.ToBase64String(imgArray);
                string extImg = Path.GetExtension(routeImg);

                Posts post = new Posts();
                post.Title = title;
                post.Content = content;
                post.CreationDate = DateTime.Now;
                post.UserId = UserId;
                post.Img = routeImg;
                post.BytesImg = base64;
                post.ExtImg = extImg;

                string bucketName = "bucketuserid" + UserId;

                var putRequest = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = nameImg,
                    InputStream = img.OpenReadStream(),
                    ContentType = img.ContentType
                };

                var result = await client_.PutObjectAsync(putRequest);

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

        public async Task<bool> DeletePost(int Id)
        {
            try
            {
                var Post = usersContext_.Posts.Where(i => i.IdPost == Id).FirstOrDefault();
                string bucketName = "bucketuserid" + Post.UserId;
                var nombreDec = Post.Img.Split('\\');
                var Key = nombreDec[nombreDec.Length - 1];

                var request = new DeleteObjectRequest()
                {
                    BucketName = bucketName,
                    Key = Key
                };

                var response = await client_.DeleteObjectAsync(request);

                usersContext_.Remove(Post);
                int result = usersContext_.SaveChanges();
                if (result > 0)
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
