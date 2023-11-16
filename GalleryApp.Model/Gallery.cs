using System.ComponentModel.DataAnnotations;

namespace GalleryApp.Model
{
    public class Gallery
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public List<GalleryImages> GalleryImages { get; set; }

    }
}