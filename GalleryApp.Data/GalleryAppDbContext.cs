using GalleryApp.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GalleryApp.Data
{
    public class GalleryAppDbContext : IdentityDbContext<UserApp>
    {
        public GalleryAppDbContext(DbContextOptions<GalleryAppDbContext> options):base(options)    
        {

        }

        public DbSet<Gallery> Galleries { get; set; }

        public DbSet<GalleryImages> GalleryImages { get; set; }

        public DbSet<Categories> Categories { get; set; }

        public DbSet<UserApp> UsersApp { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);  
        }


    }
}