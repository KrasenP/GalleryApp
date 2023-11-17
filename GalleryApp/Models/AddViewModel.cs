using System.ComponentModel.DataAnnotations;

namespace GalleryApp.Models
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        
    }
}
