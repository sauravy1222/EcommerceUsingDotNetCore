using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EcommerceUsingDotNetCore.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Remote(action:"CheckExistingId",controller:"Auth")]
        public string?Email { get; set; }
        public string?UserName { get; set; }
        public string?Password { get; set; }

        public string ?Role { get; set; }

    }
}
