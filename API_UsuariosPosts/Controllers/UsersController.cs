using API_UsuariosPosts.Models;
using CDataAccess.Interface;
using CModels.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API_UsuariosPosts.Controllers
{
    [Route("api/[controller]/[action]")]
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

        [HttpPost]
        public async Task<ActionResult<Response>> CreateUser(UsersData user)
        {
            try
            {
                if (user != null)
                {
                    int result = await user_.CreateUser(user);
                    if (result == 1)
                    {
                        response.Message = "Usuario creado satisfactoriamente";
                        response.MessageId = 1;
                        response.Status = true;
                        response.Object = user;
                    }
                    else if (result == 2)
                    {
                        response.Message = "El correo que ingresaste ya existe en el sistema";
                        response.MessageId = 2;
                        response.Status = false;
                        response.Object = user;
                    }
                    else if (result == 0)
                    {
                        response.Message = "El usuario no se pude crear, intenta nuevamente";
                        response.MessageId = 0;
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
