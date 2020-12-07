using API_UsuariosPosts.Models;
using CDataAccess.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;

namespace API_UsuariosPosts.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : Controller
    {
        private Response response;
        public IPost post_;
        public PostsController(IPost daPost)
        {
            response = new Response();
            post_ = daPost;
        }

        [HttpGet]
        public ActionResult<int> Get()
        {
            return 1;
        }

        [HttpPost]
        [Route("createPost")]
        public async Task<ActionResult<Response>> CreatePost()
        {
            string title = Request.Form["title"][0];
            string content = Request.Form["content"][0];
            IFormFile img = Request.Form.Files[0];
            bool result = await post_.CreatePost(title, content, img);
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
            return response;
        }

    }
}
