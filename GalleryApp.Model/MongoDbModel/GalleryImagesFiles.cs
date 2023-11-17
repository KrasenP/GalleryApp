using MongoDB.Bson;
using MongoDB.Driver;


namespace GalleryApp.Model.MongoDbModel
{
    public class GalleryImagesFiles
    {
        public ObjectId Id { get; set; }
        public string FileName { get; set; }
        public string GalleryId { get; set; }
        public string Extension { get; set; }


    }
}
