using CModels.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDataAccess.Interface
{
    public interface IPost
    {
        Task<bool> CreatePost(string title, string content, IFormFile img, int UserId);
    }
}
