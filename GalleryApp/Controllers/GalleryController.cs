﻿using GalleryApp.Data;
using GalleryApp.Model;
using GalleryApp.Model.MongoDbModel;
using GalleryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

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
        public async Task<IActionResult> All()
        {
            var view = await _dbContext.Galleries.Select(x => new AllViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                GalleryImage = x.GalleryImages.Select(y=>new ImageViewModel() 
                {
                   FileName = y.FileName,
                   Extensions = y.Ext
                }).FirstOrDefault()
             
            }).ToListAsync();

           

            return View(view);
        }

        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddViewModel view,IFormFile fileData) 
        {
            // create object for Gallery without Image!!! (for SQL)
            Gallery gallery = new Gallery() 
            {
                   Title = view.Title,
                   Description = view.Description
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


           return RedirectToAction("Index", "Home");
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
