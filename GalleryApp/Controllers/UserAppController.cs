using GalleryApp.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GalleryApp.Controllers
{
    public class UserAppController : Controller
    {
        private readonly SignInManager<UserApp> _signInManager;
        private readonly UserManager<UserApp> _userManager;

        public IActionResult Register() 
        {
            return View();
        }

        public IActionResult AddToAlbum()
        {
            return View();
        }


    }
}
