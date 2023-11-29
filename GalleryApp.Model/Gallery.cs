using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey(nameof(Categories))]
        public int? CategoriesId { get; set; }
        public Categories? Categories { get; set; }

        public List<GalleryImages> GalleryImages { get; set; }

    }
}