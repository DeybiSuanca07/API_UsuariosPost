using API_UsuariosPosts.Models;
using CModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDataAccess.Interface
{
    public interface IUser
    {
        int ValidateExistence(string email);
        int CreateUser(UsersData user);
    }
}
