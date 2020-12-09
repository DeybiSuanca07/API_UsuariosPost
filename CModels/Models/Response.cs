using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_UsuariosPosts.Models
{
    public class Response
    {
        public string Message { get; set; }
        public int MessageId { get; set; }
        public int Count { get; set; }
        public int CountPage { get; set; }
        public bool Status { get; set; }
        public object Object { get; set; }
    }
}
