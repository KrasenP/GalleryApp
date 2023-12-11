namespace GalleryApp.Models
{
    public class DetailsViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<ImageViewModel> Images { get; set; }
    }
}
