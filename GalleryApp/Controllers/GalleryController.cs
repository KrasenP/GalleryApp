using Microsoft.AspNetCore.Mvc;

namespace GalleryApp.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult All()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add() 
        {


            return RedirectToAction("Index","Home");
        }
    }
}
