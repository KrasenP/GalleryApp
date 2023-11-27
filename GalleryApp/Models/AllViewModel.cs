using System.Reflection.Metadata.Ecma335;

namespace GalleryApp.Models
{
    public class AllViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public ImageViewModel GalleryImage { get; set; }
    }
}
