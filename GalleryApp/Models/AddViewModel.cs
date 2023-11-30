using Amazon.Util.Internal;
using GalleryApp.Model;
using System.ComponentModel.DataAnnotations;

namespace GalleryApp.Models
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public List<CategoryViewModel> Categories { get; set; }

    }
}
