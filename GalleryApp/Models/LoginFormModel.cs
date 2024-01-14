using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GalleryApp.Models
{
    public class LoginFormModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
