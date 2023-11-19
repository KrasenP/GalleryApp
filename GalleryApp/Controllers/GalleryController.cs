using GalleryApp.Data;
using GalleryApp.Model;
using GalleryApp.Model.MongoDbModel;
using GalleryApp.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace GalleryApp.Controllers
{
    public class GalleryController : Controller
    {
        private GalleryAppDbContext _dbContext;
        private IMongoCollection<GalleryImagesFiles> mongoCollection;
       
        public GalleryController(GalleryAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult All()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddViewModel view,IFormFile fileData) 
        {
            // create object for Gallery without Image!!! for SQL
            Gallery gallery = new Gallery() 
            {
                   Title = view.Title,
                   Description = view.Description
            };


            await _dbContext.Galleries.AddAsync(gallery);
            await _dbContext.SaveChangesAsync();

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(fileData.FileName)}";
            var extension = Path.GetExtension(fileData.FileName).TrimStart('.');
            // create object for images
            GalleryImages galleryImages = new GalleryImages()
            {
                FileName = fileName,
                Ext = extension,
                GalleryId = gallery.Id
            };

            using (MemoryStream stream = new MemoryStream()) 
            {
                await fileData.CopyToAsync(stream);

                GalleryImagesFiles galleryImagesFiles = new GalleryImagesFiles()
                {
                    FileData = stream.ToArray(),
                    FileName = fileName,
                    GalleryId = gallery.Id.ToString(),
                    Extension = extension
                };
             
                await mongoCollection.InsertOneAsync(galleryImagesFiles);
            }

            await _dbContext.GalleryImages.AddAsync(galleryImages);



           return RedirectToAction("Index", "Home");
        }
    }
}
