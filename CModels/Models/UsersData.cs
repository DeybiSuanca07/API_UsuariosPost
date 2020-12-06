using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CModels.Models
{
    public partial class UsersData
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [NotMapped]
        public string Token { get; set; }

        public int IdUserData { get; set; }
    }
}
