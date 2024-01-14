using GalleryApp.Model;
using GalleryApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GalleryApp.Controllers
{
    public class UserAppController : Controller
    {
        private readonly SignInManager<UserApp> _signInManager;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUserStore<UserApp> _userStore;


        public UserAppController(SignInManager<UserApp> signInManager, UserManager<UserApp> userManager, IUserStore<UserApp> userStore)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
        }

        [HttpGet]
        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel registerForm) 
        {
            UserApp newUser = new UserApp()
            {
                UserName = registerForm.UserN,
                Status = registerForm.Status
                
            };

            await this._userManager.SetEmailAsync(newUser, registerForm.Email);

           IdentityResult result = await this._userManager.CreateAsync(newUser,registerForm.Password);

            if (result.Succeeded)
            {
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError("Register error", item.Description);
                }
                return View(registerForm);

               
            }
         
            await _signInManager.SignInAsync(newUser, false);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddToAlbum()
        {
            return View();
        }


    }
}
