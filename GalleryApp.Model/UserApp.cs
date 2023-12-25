
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GalleryApp.Model
{
    public class UserApp:IdentityUser
    {
        public UserApp()
        {
            Galleries = new List<Gallery>();
        }

        public List<Gallery> Galleries { get; set; }
    }
}
