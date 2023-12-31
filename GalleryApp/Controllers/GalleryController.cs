﻿using GalleryApp.Data;
using GalleryApp.Model;
using GalleryApp.Model.MongoDbModel;
using GalleryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using static GalleryApp.Extensions.ClaimsExt; 

namespace GalleryApp.Controllers
{
    public class GalleryController : Controller
    {
        private GalleryAppDbContext _dbContext;
        private IMongoCollection<GalleryImagesFiles> _mongoCollection;
       
        public GalleryController(GalleryAppDbContext dbContext,IMongoDatabase mongoDatabase)
        {
            _dbContext = dbContext;
            _mongoCollection = mongoDatabase.GetCollection<GalleryImagesFiles>("GalleryApp");
        }

        [HttpPost]
        public async Task<IActionResult> DelatePicture(string fileName,string Id) 
        {

            var galleryPicture = await _dbContext.GalleryImages.Where(x => x.FileName == fileName).FirstOrDefaultAsync();

            if (galleryPicture == null) 
            {
                return Content("fail");
            }

             _dbContext.GalleryImages.Remove(galleryPicture);

            await _dbContext.SaveChangesAsync();

            return Content("success");
        }

        public async Task<IActionResult> Details(Guid id) 
        {
            var getGallery = await _dbContext.Galleries.Where(x => x.Id == id)
                  .Select(y => new DetailsViewModel()
                  {
                      
                      Title=y.Title,
                      Description=y.Description,
                      Images=y.GalleryImages.Select(z=>new ImageViewModel() 
                      {
                          FileName=z.FileName,
                          Extensions = z.Ext
                          
                      }).ToList()

                  }).FirstOrDefaultAsync();
               

            return View(getGallery);
        }

        public async Task<IActionResult> Edit(string id) 
        {
            var categories = await _dbContext.Categories.Select(x=>new CategoryViewModel() 
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            var getGalleryForEdit = await _dbContext.Galleries.Where(x => x.Id.ToString() == id).Select(y => new EditViewModel()
            {
                Id = y.Id,
                Title = y.Title,
                Description = y.Description,
                Categories = categories
                
            }).FirstOrDefaultAsync();

            return View(getGalleryForEdit);
        }
     
        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel editViewModel)
        {
            Gallery gallery = await _dbContext.Galleries.Where(x => x.Id == editViewModel.Id).FirstOrDefaultAsync();

            if (gallery==null)
            {
                return NotFound();
            }

            gallery.Title = editViewModel.Title;
            gallery.Description = editViewModel.Description;
            gallery.CategoriesId = editViewModel.CategoryId;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> All()
        {
            var view = await _dbContext.Galleries.Select(x => new AllViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Category = x.Categories.Name,
                GalleryImage = x.GalleryImages.Select(y=>new ImageViewModel() 
                {
                   FileName = y.FileName,
                   Extensions = y.Ext
                }).FirstOrDefault()
             
            }).ToListAsync();

           

            return View(view);
        }

        [HttpGet]
        public async Task<IActionResult> Add() 
        {
            var ctagories = await _dbContext.Categories.Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,                
            }).ToListAsync();

            AddViewModel view = new AddViewModel();
        
            view.Categories = ctagories;
          

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddViewModel view,IFormFile fileData) 
        {
            var getUserId = this.User.GetCurrentUser();
            // create object for Gallery without Image!!! (for SQL)
            Gallery gallery = new Gallery()
            {
                Title = view.Title,
                Description = view.Description,
                CategoriesId = view.CategoryId,
                AppUserId = getUserId
                  
            };


            await _dbContext.Galleries.AddAsync(gallery);
            await _dbContext.SaveChangesAsync();

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(fileData.FileName)}";
            var extension = Path.GetExtension(fileData.FileName).TrimStart('.');

            // create object for image (for SQL)
            GalleryImages galleryImages = new GalleryImages()
            {
                FileName = fileName,
                Ext = extension,
                GalleryId = gallery.Id
            };

            // open stream to add file data in MongoDB 
            using (MemoryStream stream = new MemoryStream()) 
            {
                await fileData.CopyToAsync(stream);

               // create object(object from type (model for MongoDb)) for mongodb
                GalleryImagesFiles galleryImagesFiles = new GalleryImagesFiles()
                {
                    FileData = stream.ToArray(),
                    FileName = fileName,
                    GalleryId = gallery.Id.ToString(),
                    Extension = extension
                };
             
                await _mongoCollection.InsertOneAsync(galleryImagesFiles);
            }

            await _dbContext.GalleryImages.AddAsync(galleryImages);
            await _dbContext.SaveChangesAsync();


           return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> GetImage(string fileName) 
        {
            var imageFile = await _mongoCollection.Find(x => x.FileName == fileName).FirstOrDefaultAsync();

            if (imageFile == null || imageFile.FileData == null)
            {
                return NotFound();
            }

            return File(imageFile.FileData,$"image/{imageFile.Extension}");
        }

        
    }
}
