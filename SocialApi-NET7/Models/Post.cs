using SocialApi_NET7.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SocialAPI.Models
{
    public class Post : BaseEntity
    {
        public int AuthorId { get; set; }
        
        [Required]
        public User? Author { get; set; }
    }
}