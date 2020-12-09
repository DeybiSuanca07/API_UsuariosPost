using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using API_UsuariosPosts.Models;
using CDataAccess.Interface;
using CModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace API_UsuariosPosts.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController : Controller
    {
        private Response response;
        public IPost post_;
        private readonly IConfiguration config_;
        private List<Claim> lst;
        static IAmazonS3 client_;
        public class Search
        {
            public string title { get; set; }
            public string content { get; set; }
        }

        public PostsController(IPost daPost, IConfiguration config, IAmazonS3 client)
        {
            response = new Response();
            post_ = daPost;
            config_ = config;
            client_ = client;
        }

        [HttpPost]
        public async Task<ActionResult<Response>> GetPosts()
        {
            try
            {
                string title = "";
                string content = "";
                int pag = Convert.ToInt32(Request.Form["pag"][0]);
                if (Request.Form.Count == 2)
                {
                    dynamic lst = JsonConvert.DeserializeObject(Request.Form["search"][0]);
                    title = ((Newtonsoft.Json.Linq.JValue)((Newtonsoft.Json.Linq.JProperty)((Newtonsoft.Json.Linq.JContainer)lst).First).Value).Value.ToString();
                    content = ((Newtonsoft.Json.Linq.JValue)((Newtonsoft.Json.Linq.JProperty)((Newtonsoft.Json.Linq.JContainer)lst).Last).Value).Value.ToString();
                }

                lst = User.Claims.ToList();
                int UserId = Convert.ToInt32(lst[0].Value);
                List<Posts> posts = new List<Posts>();
                if (pag == 0)
                {
                    if (title != "" || content != "")
                    {
                        posts = await post_.GetPosts(UserId, pag, title, content);
                    } 
                    else
                    {
                        posts = await post_.GetPosts(UserId);
                    }
                }
                else
                {
                    posts = await post_.GetPosts(UserId, pag);
                }
                int countPost = post_.GetPostsCount(UserId);
                int countPage = 0;
                if (posts != null)
                {
                   countPage = posts.Count;
                }
             
                if (posts != null)
                {
                    response.Message = "Posts del usuario";
                    response.MessageId = 0;
                    response.Count = countPost;
                    response.CountPage = countPage;
                    response.Status = true;
                    response.Object = posts;
                }
                else
                {
                    response.Message = "El usuario no tiene posts creados";
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                response.Object = null;
            }
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<Response>> CreatePost()
        {
            try
            {
                string title = Request.Form["title"][0];
                string content = Request.Form["content"][0];
                IFormFile img = Request.Form.Files[0];
                lst = User.Claims.ToList();
                int UserId = Convert.ToInt32(lst[0].Value);
                bool result = await post_.CreatePost(title, content, img, UserId);
                if (result)
                {
                    response.Message = "Post creado correctamente";
                    response.Status = true;
                    response.Object = null;
                }
                else
                {
                    response.Message = "El post no se pudo crear correctamente";
                    response.Status = false;
                    response.Object = null;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                response.Object = null;
            }
            return response;
        }

        [HttpDelete]
        public async Task<ActionResult<Response>> DeletePost()
        {
            try
            {
                int id = Convert.ToInt32(Request.Form["id"][0]);
                bool result = await post_.DeletePost(id);
                if (result)
                {
                    response.Message = "Post eliminado";
                    response.MessageId = 2;
                    response.Status = true;
                    response.Object = null;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                response.Object = null;
            }
            return response;
        }
    }
}
