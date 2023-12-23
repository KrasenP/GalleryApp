using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryApp.Model
{
    public class UserApp:IdentityUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

       public List<Gallery> Galleries { get; set; }
    }
}
