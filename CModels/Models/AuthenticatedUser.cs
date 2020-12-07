using System;
using System.Collections.Generic;
using System.Text;

namespace CModels.Models
{
    public class AuthenticatedUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string TokenJWT { get; set; }
        public int IdUser { get; set; }
    }
}
