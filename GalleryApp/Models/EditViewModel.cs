using System.ComponentModel.DataAnnotations;

namespace GalleryApp.Models
{
    public class EditViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public  List<CategoryViewModel> Categories { get; set; }
    }
}
