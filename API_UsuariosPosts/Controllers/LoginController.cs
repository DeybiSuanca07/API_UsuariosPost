using API_UsuariosPosts.Models;
using CDataAccess.Interface;
using CModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_UsuariosPosts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
                            response.Status = true;
                            response.Object = user;
                        }
                        else
                        {
                            response.Message = "Usuario y/o contraseña incorrectos";
                            response.Status = false;
                            response.Object = user;
                        }
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
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
