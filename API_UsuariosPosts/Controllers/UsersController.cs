using API_UsuariosPosts.Models;
using CDataAccess.Interface;
using CModels.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_UsuariosPosts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        public Response response;
        public IUser user_;

        public UsersController(IUser daUser)
        {
            response = new Response();
            user_ = daUser;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        [Route("createUser")]
        public ActionResult<Response> CreateUser(UsersData user)
        {
            if (user != null)
            {
                int result = user_.CreateUser(user);
                if (result == 1)
                {
                    response.Message = "Usuario creado satisfactoriamente";
                    response.Status = true;
                    response.Object = user;
                }
                else if (result == 2)
                {
                    response.Message = "El correo que ingresaste ya existe en el sistema";
                    response.Status = false;
                    response.Object = user;
                }
                else if (result == 0)
                {
                    response.Message = "El usuario no se pude crear, intenta nuevamente";
                    response.Status = false;
                    response.Object = user;
                }
            }
            else
            {
                response.Message = "No se encontraron datos";
                response.Status = false;
                response.Object = null;
            }
            return response;
        }
    }
}
