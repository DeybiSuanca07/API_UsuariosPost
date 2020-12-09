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
        Task<List<Posts>> GetPosts(int UserId,int pagInicial = 0, int pagFinal = 0);
        Task<bool> CreatePost(string title, string content, IFormFile img, int UserId);
        Task<bool> DeletePost(int UserId);
    }
}
