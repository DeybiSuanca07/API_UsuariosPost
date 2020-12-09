using API_UsuariosPosts.Models;
using CModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDataAccess.Interface
{
    public interface IUser
    {
        int ValidateExistence(string email);
        Task<int> CreateUser(UsersData user);
    }
}
