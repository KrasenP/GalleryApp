using GalleryApp.Data;
using GalleryApp.Model;
using GalleryApp.Model.MongoDbModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace GalleryApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<GalleryAppDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<UserApp>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<GalleryAppDbContext>();
            builder.Services.AddControllersWithViews();

            // Add MongoDb services 
            var connectionStringMongo = builder.Configuration.GetConnectionString("MongoDb");
            var mongoClient = new MongoClient(connectionStringMongo);
            builder.Services.AddSingleton<IMongoDatabase>(mongoClient.GetDatabase("GalleryApp"));

         

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}