using CModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDataAccess.Interface
{
    public interface ILogin
    {
        bool Login(UsersData user);
    }
}
