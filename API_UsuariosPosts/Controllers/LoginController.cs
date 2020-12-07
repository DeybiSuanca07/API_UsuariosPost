using API_UsuariosPosts.Models;
using CDataAccess.Interface;
using CModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace API_UsuariosPosts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration config_;
        private readonly IUser user_;
        private readonly ILogin login_;
        private Response response;


        public LoginController(IConfiguration config, IUser daUser, ILogin daLogin)
        {
            response = new Response();
            config_ = config;
            user_ = daUser;
            login_ = daLogin;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<Response> Login(UsersData user)
        {
            try
            {
                if (user != null)
                {
                    int result = user_.ValidateExistence(user.Email);
                    if (result == 1)
                    {
                        if (login_.Login(user))
                        {
                            response.Message = "Usuario correctamente validado";
                            response.MessageId = 1;
                            response.Status = true;
                            response.Object = user;
                        }
                        else
                        {
                            response.Message = "Nombre de usuario, email y/o contraseña incorrectos";
                            response.MessageId = 2;
                            response.Status = false;
                            response.Object = user;
                        }
                    }
                    else if (result == 2)
                    {
                        response.Message = "El usuario no existe";
                        response.MessageId = 3;
                        response.Status = false;
                        response.Object = null;
                    }
                }
                else
                {
                    response.Message = "No se encontraron datos";
                    response.MessageId = 0;
                    response.Status = false;
                    response.Object = null;
                }
                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
