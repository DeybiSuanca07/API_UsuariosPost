using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CModels.Models
{
    public partial class Posts
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public string Img { get; set; }
        [Key]
        public int IdPost { get; set; }

    }
}
